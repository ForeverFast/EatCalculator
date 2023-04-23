using Client.Core.Entities.Meals.Models.Store.Actions;
using Client.Core.Shared.Resources.Localizations;
using DALQueryChain.EntityFramework.Extensions;
using Microsoft.Extensions.Logging;

namespace Client.Core.Entities.Meals.Models.Store.Effects
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
                    Messages = new List<string>
                    {
                        _injects.Localizer[nameof(DefaultLocalization.UnhandledException)]
                    },
                });
            }
        }
    }
}
