using Client.Core.Entities.Meals.Models.Store.Actions;
using Client.Core.Shared.Resources.Localizations;
using DALQueryChain.EntityFramework.Extensions;
using Microsoft.Extensions.Logging;

namespace Client.Core.Entities.Meals.Models.Store.Effects
{
    internal sealed class UpdateMealEffect : BaseEffect<UpdateMealAction>
    {
        #region Ctors

        public UpdateMealEffect(BaseEffectInjects injects,
                               ILogger<BaseEffect<UpdateMealAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(UpdateMealAction action, IDispatcher dispatcher)
        {
            try
            {
                var targetMeal = await _injects.Dal.Instance.For<Meal>().Get
                    .LoadWith(x => x.Portions)
                    .FirstOrDefaultAsync(x => x.Id == action.Id)
                    ?? throw new Exception();

                targetMeal = targetMeal with
                {
                    Title = action.Meal.Title,
                    Order = action.Meal.Order,
                };

                await _injects.Dal.Instance.For<Meal>().Update.UpdateAsync(targetMeal);
                await _injects.Dal.Instance.For<Portion>().Delete.BulkDeleteAsync(targetMeal.Portions);

                int order = 0;
                var portions = action.Meal.Portions.Select(x => x with { Order = order++ }).ToList();
                await _injects.Dal.Instance.For<Portion>().Insert.BulkInsertAsync(portions);

                dispatcher.Dispatch(new UpdateMealSuccessAction
                {
                    Meal = targetMeal with
                    {
                        Portions = portions,
                    },
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new UpdateMealFailureAction
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
