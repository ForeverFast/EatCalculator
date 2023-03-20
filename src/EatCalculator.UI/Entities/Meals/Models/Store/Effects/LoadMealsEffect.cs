using DALQueryChain.EntityFramework.Extensions;
using EatCalculator.UI.Entities.Meals.Models.Store.Actions;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor.Effects;
using Microsoft.Extensions.Logging;

namespace EatCalculator.UI.Entities.Meals.Models.Store.Effects
{
    internal sealed class LoadMealsEffect : BaseEffect<LoadMealsAction>
    {
        #region Ctors

        public LoadMealsEffect(BaseEffectInjects injects,
                              ILogger<BaseEffect<LoadMealsAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(LoadMealsAction action, IDispatcher dispatcher)
        {
            try
            {
                var meals = await _injects.Dal.For<Meal>().Get
                    .LoadWith(x => x.Portions)
                    .Where(x => x.DayId == action.DayId)    
                    .ToListAsync();

                dispatcher.Dispatch(new LoadMealsSuccessAction
                {
                    Meals = meals,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new LoadMealsFailureAction
                {
                    ErrorMessage = "",
                });
            }
        }
    }
}
