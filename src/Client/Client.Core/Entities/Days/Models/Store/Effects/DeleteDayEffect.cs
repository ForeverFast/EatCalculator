using Client.Core.Entities.Days.Models.Store.Actions;
using Client.Core.Shared.Resources.Localizations;
using Microsoft.Extensions.Logging;

namespace Client.Core.Entities.Days.Models.Store.Effects
{
    internal sealed class DeleteDayEffect : BaseEffect<DeleteDayAction>
    {
        #region Ctors

        public DeleteDayEffect(BaseEffectInjects injects,
                               ILogger<BaseEffect<DeleteDayAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(DeleteDayAction action, IDispatcher dispatcher)
        {
            try
            {
                await _injects.Dal.For<Day>().Delete.DeleteAsync(x => x.Id == action.Id);

                dispatcher.Dispatch(new DeleteDaySuccessAction
                {
                    Id = action.Id
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new DeleteDayFailureAction
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
