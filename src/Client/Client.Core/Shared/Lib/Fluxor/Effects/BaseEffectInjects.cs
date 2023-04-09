using Client.Core.Shared.Api.LocalDatabase.Context;
using Client.Core.Shared.Lib.Calculator;
using DALQueryChain.Interfaces;

namespace Client.Core.Shared.Lib.Fluxor
{
    internal class BaseEffectInjects
    {
        #region Injects

        public ISnackbar Snackbar { get; }

        public IDALQueryChain<ClientEatCalculatorDbContext> Dal { get; }

        public ICalculatorService CalculatorService { get; }

        #endregion

        #region Ctors

        public BaseEffectInjects(IDALQueryChain<ClientEatCalculatorDbContext> dal,
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
