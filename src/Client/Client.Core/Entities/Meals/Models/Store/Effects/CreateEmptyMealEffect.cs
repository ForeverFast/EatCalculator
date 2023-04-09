using Client.Core.Entities.Meals.Models.Store.Actions;
using Microsoft.Extensions.Logging;

namespace Client.Core.Entities.Meals.Models.Store.Effects
{
    internal sealed class CreateEmptyMealEffect : BaseEffect<CreateEmptyMealAction>
    {
        #region Ctors

        public CreateEmptyMealEffect(BaseEffectInjects injects,
                                     ILogger<BaseEffect<CreateEmptyMealAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(CreateEmptyMealAction action, IDispatcher dispatcher)
        {
            try
            {
                var newMeal = new Meal
                {
                    Id = 0,
                    DayId = action.DayId,
                    Title = "Новый приём пищи",
                };

                var createdMeal = await _injects.Dal.For<Meal>().Insert.InsertWithObjectAsync(newMeal);

                dispatcher.Dispatch(new CreateEmptyMealSuccessAction
                {
                    Meal = createdMeal,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new CreateEmptyMealFailureAction
                {
                    ErrorMessage = ex.Message,
                });
            }
        }
    }
}
