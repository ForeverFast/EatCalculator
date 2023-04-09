using DALQueryChain.Interfaces;
using Org.BouncyCastle.Asn1.Ocsp;
using Server.Core.Context;
using Server.Core.Context.Entities.Identity;
using Server.Core.Context.Entities.UserData;
using Server.Core.Interfaces;
using Server.Core.Interfaces.Services;
using Server.Core.Models.Api.UserData.Requests;
using Server.Core.Models.Api.UserData.Responses;

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

        public async ValueTask<CheckUpdatesResponse> CheckUpdatesAsync(CheckUpdatesRequest request, CancellationToken ctn)
        {
            var userId = request.AuthorizedUserId!.Value;
            var userData = await _dal.For<UserEatData>().Get.FirstAsync(x => x.Id == userId);

            var result = true switch
            {
                { } when !userData.LastUpdateDate.HasValue
                    => false,
                { } when userData.LastUpdateDate.Value.Equals(request.LastUpdateDate)
                    => false,
                _ => true,
            };

            return new CheckUpdatesResponse
            {
                AnyUpdates = result,
            };
        }

        public async ValueTask<LoadUserEatDataResponse> LoadUserEatDataAsync(LoadUserEatDataRequest request, CancellationToken ctn)
        {
            var userId = request.AuthorizedUserId!.Value;
            var userData = await _dal.For<UserEatData>().Get.FirstAsync(x => x.Id == userId, ctn);

            if (!userData.HasData)
                throw new NotFoundException("No available data");

            var fileData = await _fileProvider.GetFileAsync(userData.FilePath!, ctn);

            return new LoadUserEatDataResponse
            {
                Data = fileData,
                LastUpdateDate = userData.LastUpdateDate!.Value
            };
        }

        public async ValueTask<UploadUserEatDataResponse> UploadUserEatDataAsync(UploadUserEatDataRequest request, CancellationToken ctn)
        {
            var userId = request.AuthorizedUserId!.Value;
            var userData = await _dal.For<UserEatData>().Get.FirstAsync(x => x.Id == userId);

            MemoryStream memStream = new();
            await request.File.CopyToAsync(memStream, ctn);
            var fileData = memStream.ToArray();
            var updateDate = DateTime.UtcNow;

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

            return new UploadUserEatDataResponse
            {
                LastUpdateDate = updateDate,
            };
        }
    }
}
