using Blazored.LocalStorage;
using Client.Core.Entities.Viewer.Models;
using Client.Core.Entities.Viewer.Models.Store;
using Client.Core.Entities.Viewer.Models.Store.Actions;
using Client.Core.Shared.Api.HttpClient;
using Client.Core.Shared.Api.HttpClient.Requests.UserData;
using Client.Core.Shared.Api.HttpClient.Responses.UserData;
using Client.Core.Shared.Api.LocalDatabase.Context;
using Client.Core.Shared.Configs;
using DALQueryChain.EntityFramework.Builder;
using DALQueryChain.Interfaces;
using MediatR;
using MediatR.Courier;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Client.Core.Shared.Api.LocalDatabase.DalQc
{
    internal sealed class DalQcWrapper : IDalQcWrapper
    {
        #region Injects

        private readonly IServiceProvider _serviceProvider;
        private readonly IMediator _mediator;
        private readonly ICourier _courier;
        private readonly ILocalStorageService _localStorageService;
        //private readonly IActionSubscriber _actionSubscriber;
        private readonly IDispatcher _dispatcher;
        //private readonly ViewerStateFacade _viewerStateFacade;
        //private readonly IState<AppState> _appState; // TODO: переделать
        private readonly IClientEatCalculatorDbContextFileProvider _clientEatCalculatorDbContextDbFileProvider;
        private readonly ClientEatCalculatorDbContextSettings _clientEatCalculatorDbContextSettings;
        private readonly HttpEndpointsClient _httpEndpointsClient;
        private readonly IStringLocalizer<DefaultLocalization> _localizer;
        private readonly ILogger<DalQcWrapper> _logger;

        #endregion

        #region Ctors

        public DalQcWrapper(
            IServiceProvider serviceProvider,
            //IActionSubscriber actionSubscriber,
            IOptions<ClientEatCalculatorDbContextSettings> clientEatCalculatorDbContextSettings,
            IClientEatCalculatorDbContextFileProvider clientEatCalculatorDbContextDbFileProvider,
            HttpEndpointsClient httpEndpointsClient,
            IDispatcher dispatcher,
            IStringLocalizer<DefaultLocalization> localizer,
            IMediator mediator,
            ICourier courier,
            ILocalStorageService localStorageService,
            ILogger<DalQcWrapper> logger)
        //ViewerStateFacade viewerStateFacade,
        //IState<AppState> appState)
        {
            ArgumentNullException.ThrowIfNull(clientEatCalculatorDbContextSettings.Value, nameof(clientEatCalculatorDbContextSettings));

            _serviceProvider = serviceProvider;
            //_actionSubscriber = actionSubscriber;
            _dispatcher = dispatcher;
            _clientEatCalculatorDbContextSettings = clientEatCalculatorDbContextSettings.Value;
            _clientEatCalculatorDbContextDbFileProvider = clientEatCalculatorDbContextDbFileProvider;
            _httpEndpointsClient = httpEndpointsClient;
            _localizer = localizer;
            _mediator = mediator;
            _courier = courier;
            _localStorageService = localStorageService;

            _courier.Subscribe<InitializeViewerSuccessAction>(OnInitializeViewerSuccessAction);
            _logger = logger;

            //_viewerStateFacade = viewerStateFacade;
            //_appState = appState;

            //_viewerStateFacade.Viewer.StateChanged += OnInitializeViewerSuccessAction;

            //_actionSubscriber.SubscribeToAction<InitializeViewerSuccessAction>(this, OnInitializeViewerSuccessAction);
        }

        #endregion

        #region Fileds

        //private int _counter = 0;
        private ViewerModel? _currentViewer;
        private ClientEatCalculatorDbContext? _dbContext;
        private IDALQueryChain<ClientEatCalculatorDbContext>? _queryChain;

        #endregion

        #region Public

        public IDALQueryChain<ClientEatCalculatorDbContext> Instance
            => _queryChain ?? throw new InvalidOperationException("No");

        public DalQcState State { get; private set; }



        #endregion

        #region External events

        public async Task OnInitializeViewerSuccessAction(InitializeViewerSuccessAction notification, CancellationToken cancellationToken)
        {
            try
            {

                _logger.LogInformation("Start OnInitializeViewerSuccessAction");

                if (notification.Viewer == null)
                {
                    await _mediator.Publish(new DbDisposedNotification { });
                    await DisposeDbContextObjects();
                    return;
                }

                _currentViewer = notification.Viewer;

                var mainPath = GetMainPath();
                var connectionString = $"Data Source=\"{_clientEatCalculatorDbContextDbFileProvider.GetDbFilePath(mainPath)}\";";

                var options = new DbContextOptionsBuilder()
                    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                    .UseSqlite(connectionString);

                var dbContext = new ClientEatCalculatorDbContext(options.Options);

                _logger.LogInformation("Start MigrateAsync...");

                await dbContext.Database.MigrateAsync();//EnsureCreatedAsync();

                _logger.LogInformation("End MigrateAsync");

                dbContext.SavedChanges += OnDbContextSavedChanges;

                _queryChain = new BuildQuery<ClientEatCalculatorDbContext>(dbContext, _serviceProvider);

                _logger.LogInformation("Push DbInitializedNotification event");

                State = DalQcState.Initialized;
                await _mediator.Publish(new DbInitializedNotification
                {
                    UserId = _currentViewer.Id,
                    DbFileName = _clientEatCalculatorDbContextSettings.DbName,
                });

                _logger.LogInformation("Check updates...");

                await CheckUpdatesAsync();

                _logger.LogInformation("Push DbActivatedNotification event");

                State = DalQcState.Active;
                await _mediator.Publish(new DbActivatedNotification { });

                _logger.LogInformation("End OnInitializeViewerSuccessAction");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "fuck u");
                throw;
            }
        }

        private async void OnDbContextSavedChanges(object? sender, SavedChangesEventArgs e)
        {
            try
            {
                await _mediator.Publish(new DbUpdatedNotification { });

                //if (++_counter != 10)
                //    return;

                //_counter = 0;

                var fileData = await _clientEatCalculatorDbContextDbFileProvider.GetDbFileAsync(GetMainPath());
                var request = new UploadUserEatDataRequest
                {
                    DbFileData = fileData
                };

                var response = await _httpEndpointsClient.UserEatData.UploadUserEatDataAsync(request);
                if (!response.Succeeded)
                {
                    _dispatcher.Dispatch(new SynchronizeEatDataFailureAction
                    {
                        Messages = response.Messages,
                    });
                    return;
                }

                await _localStorageService.SetItemAsync(LocalStorageKeys.LastUpdateDate, response.Data.LastUpdateDate.Ticks);

                _dispatcher.Dispatch(new SynchronizeEatDataSuccessAction
                {
                    LastDbUpdateDate = response.Data.LastUpdateDate,
                });
            }
            catch (Exception _)
            {
                _dispatcher.Dispatch(new SynchronizeEatDataFailureAction
                {
                    Messages = new List<string> { _localizer[nameof(DefaultLocalization.UnhandledException)] },
                });
            }
        }

        #endregion

        #region Private methods

        private async ValueTask CheckUpdatesAsync()
        {
            if (_currentViewer == null)
                return;

            try
            {
                var localLastDbUpdateDate = await _localStorageService.GetItemAsync<long>(LocalStorageKeys.LastUpdateDate);

                var checkRequest = new CheckUpdatesRequest
                {
                    LastUpdateDate = new DateTime(localLastDbUpdateDate),
                };

                var checkResponse = await _httpEndpointsClient.UserEatData.CheckUpdatesAsync(checkRequest);
                if (!checkResponse.Succeeded)
                    return;

                if (checkResponse.Data.ServerDataState is not ServerDataState.NeedUpdate)
                    return;

                var loadRequest = new LoadUserEatDataRequest { };

                var loadResponse = await _httpEndpointsClient.UserEatData.LoadUserEatDataAsync(loadRequest);
                if (!loadResponse.Succeeded)
                {
                    return;
                }

                await _localStorageService.SetItemAsync(LocalStorageKeys.LastUpdateDate, loadResponse.Data.LastUpdateDate.Ticks);

                await _mediator.Send(new ChangeDbFileDataRequest
                {
                    FileData = loadResponse.Data.Data,
                    TargetFilePath = GetMainPath(),
                });
            }
            catch (Exception ex)
            {

            }
        }

        private string GetMainPath()
            => Path.Combine(_currentViewer!.Id.ToString(), _clientEatCalculatorDbContextSettings.DbName);

        private async ValueTask DisposeDbContextObjects()
        {
            State = DalQcState.Disposing;

            if (_dbContext != null)
                _dbContext.SavedChanges -= OnDbContextSavedChanges;
            if (_queryChain != null)
                ((IDisposable)_queryChain!).Dispose();

            _queryChain = null!;
            _dbContext = null!;

            State = DalQcState.Disabled;
        }

        #endregion

        public async ValueTask DisposeAsync()
        {
            //_viewerStateFacade.Viewer.StateChanged -= OnInitializeViewerSuccessAction;
            //_actionSubscriber.UnsubscribeFromAllActions(this);
            _courier.UnSubscribe<InitializeViewerSuccessAction>(OnInitializeViewerSuccessAction);

            await DisposeDbContextObjects();
        }
    }
}
