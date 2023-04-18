using DALQueryChain.Interfaces;
using Server.Core.Context;
using Server.Core.Context.Entities.Identity;
using Server.Core.Context.Entities.UserData;
using Server.Core.Helpers;
using Server.Core.Interfaces.Services;
using Server.Core.Models.Api.Identity.Requests;
using Server.Core.Models.Api.Identity.Responses;

namespace Server.Core.Services
{
    internal sealed class AccountService : IAccountService
    {
        #region Injects

        private readonly IDALQueryChain<ServerEatCalculatorDbContext> _dal;

        #endregion

        #region Ctors

        public AccountService(IDALQueryChain<ServerEatCalculatorDbContext> dal)
        {
            _dal = dal;
        }

        #endregion

        public async ValueTask<SignInResponse> SingInAsync(SignInRequest request, CancellationToken ctn)
        {
            var user = await _dal.For<User>()
                .Get
                .FirstOrDefaultAsync(x => x.Email == request.Data.Login || x.UserName == request.Data.Login, ctn)
                ?? throw new BadRequestException("Bad login or password");

            var checkResult = BCrypt.Net.BCrypt.Verify(request.Data.Password, user.PasswordHash);
            if (!checkResult)
                throw new BadRequestException("Bad login or password");

            return new SignInResponse
            {
                UserId = user.Id,
                AccessToken = TokenHelper.GenerateAccessToken(user),
            };
        }

        public async ValueTask<SignUpResponse> SingUpAsync(SignUpRequest request, CancellationToken ctn)
        {
            if (await _dal.For<User>().Get.AnyAsync(x => x.Email == request.Data.Email, ctn))
                throw new BadRequestException("User with this email already registered");

            if (await _dal.For<User>().Get.AnyAsync(x => x.UserName == request.Data.UserName, ctn))
                throw new BadRequestException("User with this username already registered");

            var newUser = new User
            {
                Id = 0,
                Email = request.Data.Email,
                UserName = request.Data.UserName,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Data.Password),

                UserEatData = new UserEatData
                {
                    Id = 0,
                },
            };

            var createdUser = await _dal.For<User>().Insert.InsertWithObjectAsync(newUser);

            return new SignUpResponse
            {
                UserId = createdUser.Id,
                AccessToken = TokenHelper.GenerateAccessToken(createdUser),
            };
        }
    }
}
