using Client.Core.Entities.Viewer.Models.Store;
using Client.Core.Entities.Viewer.Models.Store.Actions;
using Client.Core.Shared.Api.HttpClient;
using Client.Core.Shared.Api.HttpClient.Requests.UserData;
using DALQueryChain.EntityFramework.Builder;
using DALQueryChain.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace Client.Core.Shared.Api.LocalDatabase.Context
{
    internal sealed class DalQcWrapper : IDalQcWrapper
    {
        #region Injects

        private readonly IServiceProvider _serviceProvider;
        private readonly IActionSubscriber _actionSubscriber;
        private readonly IDispatcher _dispatcher;
        private readonly IState<ViewerState> _viewerState;
        private readonly IClientEatCalculatorDbContextFileProvider _clientEatCalculatorDbContextDbFileProvider;
        private readonly ClientEatCalculatorDbContextSettings _clientEatCalculatorDbContextSettings;
        private readonly HttpEndpointsClient _httpEndpointsClient;
        private readonly IStringLocalizer<DefaultLocalization> _localizer;

        #endregion

        #region Ctors

        public DalQcWrapper(
            IServiceProvider serviceProvider,
            IActionSubscriber actionSubscriber,
            IOptions<ClientEatCalculatorDbContextSettings> clientEatCalculatorDbContextSettings,
            IClientEatCalculatorDbContextFileProvider clientEatCalculatorDbContextDbFileProvider,
            HttpEndpointsClient httpEndpointsClient,
            IDispatcher dispatcher,
            IStringLocalizer<DefaultLocalization> localizer,
            IState<ViewerState> viewerState)
        {
            ArgumentNullException.ThrowIfNull(clientEatCalculatorDbContextSettings.Value, nameof(clientEatCalculatorDbContextSettings));

            _serviceProvider = serviceProvider;
            _actionSubscriber = actionSubscriber;
            _dispatcher = dispatcher;
            _clientEatCalculatorDbContextSettings = clientEatCalculatorDbContextSettings.Value;
            _clientEatCalculatorDbContextDbFileProvider = clientEatCalculatorDbContextDbFileProvider;
            _httpEndpointsClient = httpEndpointsClient;
            _localizer = localizer;
            _viewerState = viewerState;

            _actionSubscriber.SubscribeToAction<InitializeViewerSuccessAction>(this, OnInitializeViewerSuccessAction);
        }

        #endregion

        #region Fileds

        private ClientEatCalculatorDbContext? _dbContext;
        private IDALQueryChain<ClientEatCalculatorDbContext>? _queryChain;

        #endregion

        #region Public

        public IDALQueryChain<ClientEatCalculatorDbContext> Instance
            => _queryChain ?? throw new InvalidOperationException("No");

        public event DbInitializedEventHandler? DbInitialized;
        public event DbUpdatedEventHandler? DbUpdated;
        public event DbDisposedEventHandler? DbDisposed;

        #endregion

        #region External events

        private async void OnInitializeViewerSuccessAction(InitializeViewerSuccessAction action)
        {
            if (action.Viewer == null)
            {
                await DisposeDbContextObjects();
                return;
            }

            var mainPath = GetMainPath();
            var connectionString = _clientEatCalculatorDbContextDbFileProvider.GetDbFilePath(mainPath);

            var options = new DbContextOptionsBuilder()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .UseSqlite(connectionString);

            var dbContext = new ClientEatCalculatorDbContext(options.Options);

            await dbContext.Database.MigrateAsync();

            dbContext.SavedChanges += OnDbContextSavedChanges;

            _queryChain = new BuildQuery<ClientEatCalculatorDbContext>(dbContext, _serviceProvider);

            if (DbInitialized != null)
                await DbInitialized.Invoke(new DbInitializedEventArgs { Path = mainPath });
        }

        private async void OnDbContextSavedChanges(object? sender, SavedChangesEventArgs e)
        {
            try
            {
                if (DbUpdated != null)
                    await DbUpdated.Invoke();

                var fileData = await _clientEatCalculatorDbContextDbFileProvider.GetDbFileAsync(GetMainPath());
                var request = new UploadUserEatDataRequest
                {
                    DbFileData = fileData
                };

                var response = await _httpEndpointsClient.UserEatData.UploadUserEatDataAsync(request);
                if (!response.Succeeded)
                {
                    _dispatcher.Dispatch(new ViewerEatDataFailureAction
                    {
                        Messages = response.Messages,
                    });
                    return;
                }

                _dispatcher.Dispatch(new ViewerEatDataSuccessAction
                {
                    LastDbUpdateDate = response.Data.LastUpdateDate,
                });
            }
            catch (Exception _)
            {
                _dispatcher.Dispatch(new ViewerEatDataFailureAction
                {
                    Messages = new List<string> { _localizer[nameof(DefaultLocalization.UnhandledException)] },
                });
            }
        }

        #endregion

        #region Private methods

        private string GetMainPath()
            => $"{_viewerState.Value.Viewer!.Id}/{_clientEatCalculatorDbContextSettings.DbName}";

        private async ValueTask DisposeDbContextObjects()
        {
            if (DbDisposed != null)
                await DbDisposed.Invoke();
            if (_dbContext != null)
                _dbContext.SavedChanges -= OnDbContextSavedChanges;
            if (_queryChain != null)
                ((IDisposable)_queryChain!).Dispose();

            _queryChain = null!;
            _dbContext = null!;
        }

        #endregion

        public async ValueTask DisposeAsync()
        {
            _actionSubscriber.UnsubscribeFromAllActions(this);
            await DisposeDbContextObjects();
        }
    }
}
