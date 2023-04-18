using Client.Core.Entities.Viewer.Models.Store.Actions;
using Client.Core.Shared.Api.HttpClient.Requests.Identity;
using Client.Core.Shared.Configs;
using Common.Helpers;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Client.Core.Entities.Viewer.Models.Store.Effects
{
    internal sealed class SignInEffect : BaseEffect<SignInAction>
    {
        #region Ctors

        public SignInEffect(BaseEffectInjects injects, ILogger<BaseEffect<SignInAction>> logger) : base(injects, logger)
        {

        }

        #endregion

        public override async Task HandleAsync(SignInAction action, IDispatcher dispatcher)
        {
            try
            {
                var request = new SignInRequest
                {
                    Body = new SignInRequestData
                    {
                        Login = action.Login,
                        Password =  action.Password,
                    },
                };

                var response = await _injects.HttpEndpointsClient.Account.SignInAsync(request);
                await _injects.LocalStorageService.SetItemAsync(LocalStorageKeys.AccessToken, response.AccessToken);
                _injects.AuthenticationStateProvider.MarkUserAsAuthenticated(action.Login);

                var claims = IdentityHelper.ParseClaimsFromJwt(response.AccessToken).ToList();

                var viewerModel = new ViewerModel
                {
                    Id = int.Parse(claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value),
                    Email = claims.First(x => x.Type == ClaimTypes.Email).Value,
                    UserName = claims.First(x => x.Type == ClaimTypes.Name).Value,
                };

                dispatcher.Dispatch(new SignInSuccessAction
                {
                    ViewerModel = viewerModel,  
                });
            }
            catch
            {

            }
        }
    }
}
