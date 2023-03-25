using DALQueryChain.Interfaces;
using EatCalculator.UI.Shared.Api.LocalDatabase.Context;
using EatCalculator.UI.Shared.Lib.Calculator;

namespace EatCalculator.UI.Shared.Lib.Fluxor.Effects
{
    internal class BaseEffectInjects
    {
        #region Injects

        public ISnackbar Snackbar { get; }

        public IDALQueryChain<EatCalculatorDbContext> Dal { get; }

        public ICalculatorService CalculatorService { get; }

        #endregion

        #region Ctors

        public BaseEffectInjects(IDALQueryChain<EatCalculatorDbContext> dal,
                                 ISnackbar snackbar,
                                 ICalculatorService calculatorService)
        {
            Dal = dal;
            Snackbar = snackbar;
            CalculatorService = calculatorService;
        }

        #endregion
    }
}
