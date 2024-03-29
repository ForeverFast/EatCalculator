﻿using Blazored.LocalStorage;
using Client.Core.Shared.Api.HttpClient;
using Client.Core.Shared.Api.LocalDatabase.DalQc;
using Client.Core.Shared.Lib.Calculator;
using Client.Core.Shared.Lib.FrameworkAbstractions;
using DALQueryChain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Localization;

namespace Client.Core.Shared.Lib.Fluxor
{
    internal class BaseEffectInjects
    {
        #region Injects

        public HttpEndpointsClient HttpEndpointsClient { get; }    
        public AuthenticationStateProvider AuthenticationStateProvider { get; }

        public IStringLocalizer<DefaultLocalization> Localizer { get; }

        public IMediator Mediator { get; }
        public ISnackbar Snackbar { get; }
        public ILocalStorageService LocalStorageService { get; }
        public IDalQcWrapper Dal { get; }

        public ICalculatorService CalculatorService { get; }

        #endregion

        #region Ctors

        public BaseEffectInjects(IDalQcWrapper dal,
                                 ISnackbar snackbar,
                                 ICalculatorService calculatorService,
                                 AuthenticationStateProvider authenticationStateProvider,
                                 HttpEndpointsClient httpEndpointsClient,
                                 ILocalStorageService localStorageService,
                                 IStringLocalizer<DefaultLocalization> localizer,
                                 IMediator mediator)
        {
            Dal = dal;
            Snackbar = snackbar;
            CalculatorService = calculatorService;
            AuthenticationStateProvider = authenticationStateProvider;
            HttpEndpointsClient = httpEndpointsClient;
            LocalStorageService = localStorageService;
            Localizer = localizer;
            Mediator = mediator;
        }

        #endregion
    }
}
