using DALQueryChain.Interfaces;
using EatCalculator.UI.Shared.Api.LocalDatabase.Context;

namespace EatCalculator.UI.Shared.Lib.Fluxor.Effects
{
    internal class BaseEffectInjects
    {
        #region Injects

        public IDALQueryChain<EatCalculatorDbContext> Dal { get; }

        #endregion

        #region Ctors

        public BaseEffectInjects(IDALQueryChain<EatCalculatorDbContext> dal)
        {
            Dal = dal;
        }

        #endregion
    }
}
