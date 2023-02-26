using EatCalculator.UI.Entities.Days.Models.Store.Actions;
using EatCalculator.UI.Entities.Products.Models.Store.Actions;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor.Effects;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace EatCalculator.UI.Entities.Days.Models.Store.Effects
{
    internal sealed class UpdateDayEffect : BaseEffect<UpdateDayAction>
    {
        #region Ctors

        public UpdateDayEffect(BaseEffectInjects injects,
                               ILogger<BaseEffect<UpdateDayAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(UpdateDayAction action, IDispatcher dispatcher)
        {
            try
            {
                await Task.Yield();

                var updatedDay = new Day
                {
                    Id = RandomNumberGenerator.GetInt32(100, int.MaxValue),
                    Title = action.Day.Title
                };

                dispatcher.Dispatch(new UpdateDaySuccessAction
                {
                    Day = updatedDay,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new UpdateDayFailureAction
                {
                    ErrorMessage = "",
                });
            }
        }
    }
}
