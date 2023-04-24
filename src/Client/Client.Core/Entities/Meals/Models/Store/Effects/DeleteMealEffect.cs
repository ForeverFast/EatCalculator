using Client.Core.Entities.Meals.Models.Store.Actions;
using Client.Core.Shared.Resources.Localizations;
using Microsoft.Extensions.Logging;

namespace Client.Core.Entities.Meals.Models.Store.Effects
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
                await _injects.Dal.Instance.For<Meal>().Delete.DeleteAsync(x => x.Id == action.Id);

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
                    Messages = new List<string>
                    {
                        _injects.Localizer[nameof(DefaultLocalization.UnhandledException)]
                    },
                });
            }
        }
    }
}
