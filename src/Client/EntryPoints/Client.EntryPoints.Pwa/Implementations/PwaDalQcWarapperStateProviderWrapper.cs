using Client.Core.Shared.Api.LocalDatabase.DalQc;
using MediatR;

namespace Client.EntryPoints.Pwa.Implementations
{
    public class PwaDalQcWarapperStateProviderWrapper
        : INotificationHandler<DbInitializedNotification>
        , INotificationHandler<DbUpdatedNotification>
        , INotificationHandler<DbDisposedNotification>
        , IRequestHandler<ChangeDbFileDataRequest>
    {

        #region Injects 

        private readonly PwaDalQcWarapperStateProvider _pwaClientEatCalculatorDbContextCacheSynchronizer;

        #endregion

        #region Ctors

        public PwaDalQcWarapperStateProviderWrapper(
            PwaDalQcWarapperStateProvider pwaClientEatCalculatorDbContextCacheSynchronizer)
        {
            _pwaClientEatCalculatorDbContextCacheSynchronizer = pwaClientEatCalculatorDbContextCacheSynchronizer;
        }

        public Task Handle(DbInitializedNotification notification, CancellationToken cancellationToken)
            => _pwaClientEatCalculatorDbContextCacheSynchronizer.Handle(notification, cancellationToken);
        public Task Handle(DbUpdatedNotification notification, CancellationToken cancellationToken)
            => _pwaClientEatCalculatorDbContextCacheSynchronizer.Handle(notification, cancellationToken);
        public Task Handle(DbDisposedNotification notification, CancellationToken cancellationToken)
            => _pwaClientEatCalculatorDbContextCacheSynchronizer.Handle(notification, cancellationToken);
        public Task Handle(ChangeDbFileDataRequest request, CancellationToken cancellationToken)
            => _pwaClientEatCalculatorDbContextCacheSynchronizer.Handle(request, cancellationToken);

        #endregion


    }
}
