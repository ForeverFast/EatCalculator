using Client.Core.Entities.Days.Models.Store.Actions;
using Client.Core.Shared.Resources.Localizations;
using Microsoft.Extensions.Logging;

namespace Client.Core.Entities.Days.Models.Store.Effects
{
    internal sealed class DetachDayFromDateEffect : BaseEffect<DetachDayFromDateAction>
    {
        #region Ctors

        public DetachDayFromDateEffect(BaseEffectInjects injects, ILogger<BaseEffect<DetachDayFromDateAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(DetachDayFromDateAction action, IDispatcher dispatcher)
        {
            try
            {
                await _injects.Dal.For<DayDateBind>()
                    .Delete
                    .DeleteAsync(x => x.Id == action.DayDateBindId);

                dispatcher.Dispatch(new DetachDayFromDateSuccessAction
                {
                    DayId = action.DayId,
                    DayDateBindId = action.DayDateBindId,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new DetachDayFromDateFailureAction
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

