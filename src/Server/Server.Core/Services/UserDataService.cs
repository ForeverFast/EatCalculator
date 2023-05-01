using DALQueryChain.Interfaces;
using Org.BouncyCastle.Asn1.Ocsp;
using Server.Core.Context;
using Server.Core.Context.Entities.Identity;
using Server.Core.Context.Entities.UserData;
using Server.Core.Interfaces;
using Server.Core.Interfaces.Services;
using Server.Core.Models.Api.UserData.Requests;
using Server.Core.Models.Api.UserData.Responses;
using System;
using System.Collections.Concurrent;

namespace Server.Core.Services
{
    internal sealed class UserDataService : IUserDataService
    {
        #region Injects

        private readonly IDALQueryChain<ServerEatCalculatorDbContext> _dal;
        private readonly IFileProvider _fileProvider;

        #endregion

        #region Ctors

        public UserDataService(IDALQueryChain<ServerEatCalculatorDbContext> dal, IFileProvider fileProvider)
        {
            _dal = dal;
            _fileProvider = fileProvider;
        }

        #endregion

        private readonly static ConcurrentDictionary<int, SemaphoreSlim> _locker = new();

        private async ValueTask<IResult<T>> Execute<T>(Func<ValueTask<IResult<T>>> func, int userId)
        {
            var locker = _locker.GetOrAdd(userId, new SemaphoreSlim(1, 1));

            try
            {
                await locker.WaitAsync();

                return await func();
            }
            finally
            {
                locker.Release();
            }
        }

        public ValueTask<IResult<CheckUpdatesResponse>> CheckUpdatesAsync(CheckUpdatesRequest request, CancellationToken ctn)
            => Execute<CheckUpdatesResponse>(async () =>
            {

                var userId = request.AuthorizedUserId!.Value;
                var userData = await _dal.For<UserEatData>().Get.FirstAsync(x => x.Id == userId);

                var result = true switch
                {
                    { } when !userData.LastUpdateDate.HasValue
                        => ServerDataState.NotFound,
                    { } when request.LastUpdateDate.HasValue && userData.LastUpdateDate.Value <= request.LastUpdateDate.Value
                        => ServerDataState.NoUpdates,
                    _ => ServerDataState.NeedUpdate,
                };

                return Result<CheckUpdatesResponse>.Success(new CheckUpdatesResponse
                {
                    ServerDataState = result,
                });

            }, request.AuthorizedUserId!.Value);

        public ValueTask<IResult<LoadUserEatDataResponse>> LoadUserEatDataAsync(LoadUserEatDataRequest request, CancellationToken ctn)
            => Execute<LoadUserEatDataResponse>(async () =>
            {
                var userId = request.AuthorizedUserId!.Value;
                var userData = await _dal.For<UserEatData>().Get.FirstAsync(x => x.Id == userId, ctn);

                if (!userData.HasData)
                    return Result<LoadUserEatDataResponse>.Fail("No available data");

                var fileData = await _fileProvider.GetFileAsync(userData.FilePath!, ctn);

                return Result<LoadUserEatDataResponse>.Success(new LoadUserEatDataResponse
                {
                    Data = fileData,
                    LastUpdateDate = userData.LastUpdateDate!.Value
                });
            }, request.AuthorizedUserId!.Value);

        public ValueTask<IResult<UploadUserEatDataResponse>> UploadUserEatDataAsync(UploadUserEatDataRequest request, CancellationToken ctn)
            => Execute<UploadUserEatDataResponse>(async () =>
            {
                var userId = request.AuthorizedUserId!.Value;
                var userData = await _dal.For<UserEatData>().Get.FirstAsync(x => x.Id == userId);

                MemoryStream memStream = new();
                await request.File.CopyToAsync(memStream, ctn);
                var fileData = memStream.ToArray();
                var updateDate = DateTime.Parse(DateTime.UtcNow.ToString("yyyy-MM-ddTHH:mm:ss"));

                if (!userData.HasData)
                {
                    var filePath = await _fileProvider.CreateFileAsync(fileData, userId, ctn: ctn);
                    userData = userData with
                    {
                        FilePath = filePath,
                    };
                }
                else
                {
                    await _fileProvider.UpdateFile(fileData, userData.FilePath!, ctn);
                }

                await _dal.For<UserEatData>().Update.UpdateAsync(userData with
                {
                    LastUpdateDate = updateDate,
                });

                return Result<UploadUserEatDataResponse>.Success(new UploadUserEatDataResponse
                {
                    LastUpdateDate = updateDate,
                });
            }, request.AuthorizedUserId!.Value);
    }
}
