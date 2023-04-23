using Client.Core.Entities.Viewer.Models.Store.Actions;
using Client.Core.Shared.Api.HttpClient.Requests.Identity;
using Client.Core.Shared.Configs;
using Common.Helpers;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Client.Core.Entities.Viewer.Models.Store.Effects
{
    internal sealed class SignUpEffect : BaseEffect<SignUpAction>
    {
        #region Injects

        private readonly IStringLocalizer<IdentityErrorsLocalization> _identityErrorsLocalizer;

        #endregion

        #region Ctors

        public SignUpEffect(
            BaseEffectInjects injects,
            ILogger<BaseEffect<SignUpAction>> logger,
            IStringLocalizer<IdentityErrorsLocalization> identityErrorsLocalizer) : base(injects, logger)
        {
            _identityErrorsLocalizer = identityErrorsLocalizer;
        }

        #endregion

        public override async Task HandleAsync(SignUpAction action, IDispatcher dispatcher)
        {
            try
            {
                var request = new SignUpRequest
                {
                    Body = new SignUpRequestData
                    {
                        UserName = action.UserName,
                        Email = action.Email,
                        Password = action.Password,
                    },
                };

                var response = await _injects.HttpEndpointsClient.Account.SignUpAsync(request);
                if (!response.Succeeded)
                {
                    var errorMessages = response.Messages
                        .Select(x => _identityErrorsLocalizer[x].Value)
                        .ToList();

                    if (!errorMessages.Any())
                        errorMessages.Add(_identityErrorsLocalizer[nameof(IdentityErrorsLocalization.DefaultMessage)]);

                    dispatcher.Dispatch(new SignUpFailureAction
                    {
                        Messages = errorMessages,
                    });
                    return;
                }

                await _injects.LocalStorageService.SetItemAsync(LocalStorageKeys.AccessToken, response.Data.AccessToken);
                await _injects.AuthenticationStateProvider.GetAuthenticationStateAsync();

                var claims = IdentityHelper.ParseClaimsFromJwt(response.Data.AccessToken).ToList();

                var viewerModel = new ViewerModel
                {
                    Id = int.Parse(claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value),
                    Email = claims.First(x => x.Type == ClaimTypes.Email).Value,
                    UserName = claims.First(x => x.Type == ClaimTypes.Name).Value,
                };

                dispatcher.Dispatch(new SignUpSuccessAction
                {
                    Viewer = viewerModel,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new SignUpFailureAction
                {
                    Messages = new List<string>
                    {
                        _injects.Localizer[nameof(DefaultLocalization.UnhandledException)]
                    },
                });
            }
        }
    }
}
