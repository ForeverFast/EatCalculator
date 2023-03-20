using EatCalculator.UI.Entities.Meals.Models.Store.Actions;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor.Effects;
using Microsoft.Extensions.Logging;

namespace EatCalculator.UI.Entities.Meals.Models.Store.Effects
{
    internal sealed class DeleteMealEffect : BaseEffect<DeleteMealAction>
    {
        #region Ctors

        public DeleteMealEffect(BaseEffectInjects injects,
                               ILogger<BaseEffect<DeleteMealAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(DeleteMealAction action, IDispatcher dispatcher)
        {
            try
            {
                await _injects.Dal.For<Meal>().Delete.DeleteAsync(x => x.Id == action.Id);

                dispatcher.Dispatch(new DeleteMealSuccessAction
                {
                    Id = action.Id
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new DeleteMealFailureAction
                {
                    ErrorMessage = ex.Message,
                });
            }
        }
    }
}
