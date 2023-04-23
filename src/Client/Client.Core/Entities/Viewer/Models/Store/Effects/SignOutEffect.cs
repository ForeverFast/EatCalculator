using Client.Core.Entities.Viewer.Models.Store.Actions;
using Client.Core.Shared.Configs;
using Microsoft.Extensions.Logging;

namespace Client.Core.Entities.Viewer.Models.Store.Effects
{
    internal sealed class SignOutEffect : BaseEffect<SignOutAction>
    {
        #region Ctors

        public SignOutEffect(BaseEffectInjects injects, ILogger<BaseEffect<SignOutAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(SignOutAction action, IDispatcher dispatcher)
        {
            try
            {
                await _injects.LocalStorageService.RemoveItemAsync(LocalStorageKeys.AccessToken);
                await _injects.AuthenticationStateProvider.GetAuthenticationStateAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new SignOutFailureAction
                {
                    Messages = new List<string> { _injects.Localizer[nameof(DefaultLocalization.UnhandledException)] },
                });
            }
        }
    }
}
