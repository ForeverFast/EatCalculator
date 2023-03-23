using EatCalculator.UI.Entities.Days.Models.Store.Actions;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor.Effects;
using Microsoft.Extensions.Logging;

namespace EatCalculator.UI.Entities.Days.Models.Store.Effects
{
    internal sealed class CreateDayEffect : BaseEffect<CreateDayAction>
    {
        #region Ctors

        public CreateDayEffect(BaseEffectInjects injects,
                               ILogger<BaseEffect<CreateDayAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(CreateDayAction action, IDispatcher dispatcher)
        {
            try
            {
                var newDay = new Day
                {
                    Id = 0,
                    Title = action.Day.Title,
                    Description = action.Day.Description,
                };

                var createdDay = await _injects.Dal.For<Day>().Insert.InsertWithObjectAsync(newDay);

                await _injects.Dal.For<Meal>().Insert.BulkInsertAsync(Enumerable.Range(1, action.Day.MealCount ?? 3).Select(x => new Meal
                {
                    Id = 0,
                    DayId = createdDay.Id,
                    Title = $"Приём пищи №{x}",
                    Order = x,
                }));

                dispatcher.Dispatch(new CreateDaySuccessAction
                {
                    Day = createdDay,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new CreateDayFailureAction
                {
                    ErrorMessage = ex.Message,
                });
            }
        }
    }
}
