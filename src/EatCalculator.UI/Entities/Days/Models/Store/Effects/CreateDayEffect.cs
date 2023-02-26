using EatCalculator.UI.Entities.Days.Models.Store.Actions;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor.Effects;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace EatCalculator.UI.Entities.Days.Models.Store.Effects
{
    internal sealed class CreateDayEffect : BaseEffect<CreateDayAction>
    {
        #region Ctors

        public CreateDayEffect(BaseEffectInjects injects,
                               ILogger<BaseEffect<CreateDayAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(CreateDayAction action, IDispatcher dispatcher)
        {
            try
            {
                await Task.Yield();

                var createdProduct = new Day
                {
                    Id = RandomNumberGenerator.GetInt32(100, int.MaxValue),
                    Title = action.Day.Title,
                };

                dispatcher.Dispatch(new CreateDaySuccessAction
                {
                    Day = createdProduct,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new CreateDayFailureAction
                {
                    ErrorMessage = ex.Message,
                });
            }
        }
    }
}
