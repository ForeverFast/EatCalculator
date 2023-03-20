using EatCalculator.UI.Entities.Days.Models.Store.Actions;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor.Effects;
using Microsoft.Extensions.Logging;

namespace EatCalculator.UI.Entities.Days.Models.Store.Effects
{
    internal sealed class LoadDaysEffect : BaseEffect<LoadDaysAction>
    {
        #region Ctors

        public LoadDaysEffect(BaseEffectInjects injects,
                              ILogger<BaseEffect<LoadDaysAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(LoadDaysAction action, IDispatcher dispatcher)
        {
            try
            {
                var days = await _injects.Dal.For<Day>().Get.ToListAsync();

                dispatcher.Dispatch(new LoadDaysSuccessAction
                {
                    Days = days,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new LoadDaysFailureAction
                {
                    ErrorMessage = "",
                });
            }
        }
    }
}
