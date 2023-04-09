using DALQueryChain.Interfaces;
using Server.Core.Context;
using Server.Core.Context.Entities.Identity;
using Server.Core.Context.Entities.UserData;
using Server.Core.Helpers;
using Server.Core.Interfaces.Services;
using Server.Core.Models.Api.Identity.Requests;
using Server.Core.Models.Api.Identity.Responses;
using System.Security.Claims;

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

            if (user.RefreshToken == null || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                user = user with
                {
                    RefreshToken = TokenHelper.GenerateRefreshToken(),
                    RefreshTokenExpiryTime = DateTime.UtcNow.AddYears(1),
                };

                await _dal.For<User>()
                    .Update
                    .UpdateAsync(user);
            }

            return new SignInResponse
            {
                UserId = user.Id,
                AccessToken = TokenHelper.GenerateAccessToken(user),
                RefreshToken = user.RefreshToken,
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
                RefreshToken = TokenHelper.GenerateRefreshToken(),
                RefreshTokenExpiryTime = DateTime.UtcNow.AddYears(1),

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
                RefreshToken = createdUser.RefreshToken!,
            };
        }

        public async ValueTask<RefreshTokenResponse> RefreshTokenAsync(RefreshTokenRequest request, CancellationToken ctn)
        {
            var principal = TokenHelper.GetPrincipalFromToken(request.Data.AccessToken);
            var userIdFromClaims = principal?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (!int.TryParse(userIdFromClaims, out var userId))
                throw new BadRequestException("Invalid token");

            var user = await _dal.For<User>()
               .Get
               .FirstOrDefaultAsync(x => x.Id == userId, ctn)
               ?? throw new BadRequestException("Invalid token");

            if (!(user.RefreshToken == request.Data.RefreshToken && user.RefreshTokenExpiryTime >= DateTime.UtcNow))
                throw new BadRequestException("Invalid refresh token");

            return new RefreshTokenResponse
            {
                AccessToken = TokenHelper.GenerateAccessToken(user),
            };
        }
    }
}
