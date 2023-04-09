using Client.Core.Entities.Days.Models.Store.Actions;
using Microsoft.Extensions.Logging;

namespace Client.Core.Entities.Days.Models.Store.Effects
{
    internal sealed class UpdateDayEffect : BaseEffect<UpdateDayAction>
    {
        #region Ctors

        public UpdateDayEffect(BaseEffectInjects injects,
                               ILogger<BaseEffect<UpdateDayAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(UpdateDayAction action, IDispatcher dispatcher)
        {
            try
            {
                var targetDay = await _injects.Dal.For<Day>().Get
                    .FirstOrDefaultAsync(x => x.Id == action.Id)
                    ?? throw new Exception();

                targetDay = targetDay with
                {
                    Title = action.Day.Title,
                    Description = action.Day.Description,
                    ProteinPercentages = action.Day.ProteinPercentages,
                    FatPercentages = action.Day.FatPercentages,
                    CarbohydratePercentages = action.Day.CarbohydratePercentages,
                    ProteinMealCount = action.Day.ProteinMealCount,
                    FatMealCount = action.Day.FatMealCount,
                    CarbohydrateMealCount = action.Day.CarbohydrateMealCount,
                };

                await _injects.Dal.For<Day>().Update.UpdateAsync(targetDay);

                dispatcher.Dispatch(new UpdateDaySuccessAction
                {
                    Day = targetDay,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new UpdateDayFailureAction
                {
                    ErrorMessage = "",
                });
            }
        }
    }
}
