using Blazored.LocalStorage;
using Client.Core.Shared.Api.HttpClient;
using Client.Core.Shared.Api.LocalDatabase.Context;
using Client.Core.Shared.Lib.Calculator;
using Client.Core.Shared.Lib.FrameworkAbstractions;
using DALQueryChain.Interfaces;

namespace Client.Core.Shared.Lib.Fluxor
{
    internal class BaseEffectInjects
    {
        #region Injects

        public HttpEndpointsClient HttpEndpointsClient { get; }    
        public ClientAppAuthenticationStateProviderWrapper AuthenticationStateProvider { get; }    

        public ISnackbar Snackbar { get; }
        public ILocalStorageService LocalStorageService { get; }
        public IDALQueryChain<ClientEatCalculatorDbContext> Dal { get; }

        public ICalculatorService CalculatorService { get; }

        #endregion

        #region Ctors

        public BaseEffectInjects(IDALQueryChain<ClientEatCalculatorDbContext> dal,
                                 ISnackbar snackbar,
                                 ICalculatorService calculatorService,
                                 ClientAppAuthenticationStateProviderWrapper authenticationStateProvider,
                                 HttpEndpointsClient httpEndpointsClient,
                                 ILocalStorageService localStorageService)
        {
            Dal = dal;
            Snackbar = snackbar;
            CalculatorService = calculatorService;
            AuthenticationStateProvider = authenticationStateProvider;
            HttpEndpointsClient = httpEndpointsClient;
            LocalStorageService = localStorageService;
        }

        #endregion
    }
}
