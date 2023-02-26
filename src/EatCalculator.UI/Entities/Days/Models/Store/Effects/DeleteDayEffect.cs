using EatCalculator.UI.Entities.Days.Models.Store.Actions;
using EatCalculator.UI.Shared.Lib.Fluxor.Effects;
using Microsoft.Extensions.Logging;

namespace EatCalculator.UI.Entities.Days.Models.Store.Effects
{
    internal sealed class DeleteDayEffect : BaseEffect<DeleteDayAction>
    {
        #region Ctors

        public DeleteDayEffect(BaseEffectInjects injects,
                               ILogger<BaseEffect<DeleteDayAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(DeleteDayAction action, IDispatcher dispatcher)
        {
            try
            {
                await Task.Yield();

                dispatcher.Dispatch(new DeleteDaySuccessAction
                {
                    Id = action.Id
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new DeleteDayFailureAction
                {
                    ErrorMessage = ex.Message,
                });
            }
        }
    }
}
