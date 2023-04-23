using Client.Core.Entities.Days.Models.Store.Actions;
using Client.Core.Shared.Resources.Localizations;
using Microsoft.Extensions.Logging;

namespace Client.Core.Entities.Days.Models.Store.Effects
{
    internal sealed class AttachDayToDateEffect : BaseEffect<AttachDayToDateAction>
    {
        #region Ctors

        public AttachDayToDateEffect(BaseEffectInjects injects, ILogger<BaseEffect<AttachDayToDateAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(AttachDayToDateAction action, IDispatcher dispatcher)
        {
            try
            {
                var targetDay = await _injects.Dal.For<Day>()
                    .Get
                    .FirstOrDefaultAsync(x => x.Id == action.DayId)
                    ?? throw new Exception();

                var newDayDateBind = new DayDateBind
                {
                    Id = 0,
                    DayId = action.DayId,
                    Date = action.Date,
                };

                var createdDayDateBind = await _injects.Dal.For<DayDateBind>()
                    .Insert
                    .InsertWithObjectAsync(newDayDateBind);

                dispatcher.Dispatch(new AttachDayToDateSuccessAction
                {
                    DayDateBind = createdDayDateBind,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new AttachDayToDateFailureAction
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
