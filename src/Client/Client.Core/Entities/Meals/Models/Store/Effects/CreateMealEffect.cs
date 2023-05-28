using Client.Core.Entities.Meals.Models.Store.Actions;
using Client.Core.Shared.Resources.Localizations;
using Microsoft.Extensions.Logging;

namespace Client.Core.Entities.Meals.Models.Store.Effects
{
    internal sealed class CreateMealEffect : BaseEffect<CreateMealAction>
    {
        #region Ctors

        public CreateMealEffect(BaseEffectInjects injects,
                               ILogger<BaseEffect<CreateMealAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(CreateMealAction action, IDispatcher dispatcher)
        {
            try
            {
                var newMeal = new Meal
                {
                    Id = 0,
                    DayId = action.Meal.DayId,
                    Title = action.Meal.Title,
                };

                var createdMeal = await _injects.Dal.Instance.For<Meal>().Insert.InsertWithObjectAsync(newMeal);

                if (action.Meal.Portions.Count > 0)
                {
                    var newPortions = action.Meal.Portions
                        .Select(portion => portion with
                        {
                            Id = 0,
                            MealId = createdMeal.Id,
                        })
                        .ToList();

                    await _injects.Dal.Instance.For<Portion>().Insert.BulkInsertAsync(newPortions);

                    createdMeal.Portions.AddRange(newPortions);
                }

                dispatcher.Dispatch(new CreateMealSuccessAction
                {
                    Meal = createdMeal,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new CreateMealFailureAction
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
