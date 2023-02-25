using Microsoft.Extensions.Logging;

namespace EatCalculator.UI.Shared.Lib.Fluxor.Effects
{
    internal abstract class BaseEffect<TAction> : Effect<TAction>
        where TAction : BaseAction
    {
        #region Injects

        protected readonly BaseEffectInjects _injects;
        protected readonly ILogger<BaseEffect<TAction>> _logger;

        #endregion

        #region Ctors

        protected BaseEffect(BaseEffectInjects injects,
                             ILogger<BaseEffect<TAction>> logger)
        {
            _injects = injects;
            _logger = logger;
        }

        #endregion
    }
}
