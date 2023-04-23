using Client.Core.Entities.Days.Models.Store.Actions;
using Client.Core.Shared.Resources.Localizations;
using Microsoft.Extensions.Logging;

namespace Client.Core.Entities.Days.Models.Store.Effects
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
                var mealCount = action.Day.MealCount ?? _injects.CalculatorService.DefaultMealCountForDay;

                var newDay = new Day
                {
                    Id = 0,
                    Title = action.Day.Title,
                    Description = action.Day.Description,

                    ProteinPercentages = _injects.CalculatorService.DefaultProteinPercentagesForDay,
                    FatPercentages = _injects.CalculatorService.DefaultFatPercentagesForDay,
                    CarbohydratePercentages = _injects.CalculatorService.DefaultCarbohydratePercentagesForDay,

                    ProteinMealCount = mealCount,
                    FatMealCount = mealCount,
                    CarbohydrateMealCount = mealCount,
                };

                var createdDay = await _injects.Dal.For<Day>().Insert.InsertWithObjectAsync(newDay);

                await _injects.Dal.For<Meal>().Insert.BulkInsertAsync(Enumerable.Range(1, mealCount).Select(x => new Meal
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
                    Messages = new List<string>
                    {
                        _injects.Localizer[nameof(DefaultLocalization.UnhandledException)]
                    },
                });
            }
        }
    }
}
