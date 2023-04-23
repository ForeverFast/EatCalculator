using Client.Core.Entities.Viewer.Models.Store.Actions;
using Client.Core.Shared.Configs;
using Common.Helpers;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace Client.Core.Entities.Viewer.Models.Store.Effects
{
    internal class InitializeViewerEffect : BaseEffect<InitializeViewerAction>
    {
        #region Ctors

        public InitializeViewerEffect(BaseEffectInjects injects, ILogger<BaseEffect<InitializeViewerAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(InitializeViewerAction action, IDispatcher dispatcher)
        {
            try
            {
                var authenticationState = await action.AuthenticationStateTask;

                if (!authenticationState?.User.Identity?.IsAuthenticated ?? true)
                {
                    dispatcher.Dispatch(new InitializeViewerSuccessAction
                    {
                        Viewer = null,
                    });
                    return;
                }

                var claims = authenticationState!.User!.Claims;

                var viewer = new ViewerModel
                {
                    Id = int.Parse(claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value),
                    Email = claims.First(x => x.Type == ClaimTypes.Email).Value,
                    UserName = claims.First(x => x.Type == ClaimTypes.Name).Value,
                };

                dispatcher.Dispatch(new InitializeViewerSuccessAction
                {
                    Viewer = viewer,
                });
            }
            catch (Exception ex)
            {
                dispatcher.Dispatch(new InitializeViewerFailureAction
                {
                    Messages = new List<string> { ex.Message },
                });
            }
        }
    }
}
