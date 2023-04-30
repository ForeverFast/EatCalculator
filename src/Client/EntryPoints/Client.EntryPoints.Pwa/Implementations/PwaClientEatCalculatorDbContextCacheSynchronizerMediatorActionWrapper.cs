using Client.Core.Shared.Api.LocalDatabase.Context;
using MediatR;

namespace Client.EntryPoints.Pwa.Implementations
{
    public class PwaClientEatCalculatorDbContextCacheSynchronizerMediatorActionWrapper
        : INotificationHandler<DbInitializedNotification>
        , INotificationHandler<DbUpdatedNotification>
        , INotificationHandler<DbDisposedNotification>
        , IRequestHandler<ChangeDbFileDataRequest>
    {

        #region Injects 

        private readonly PwaClientEatCalculatorDbContextCacheSynchronizer _pwaClientEatCalculatorDbContextCacheSynchronizer;

        #endregion

        #region Ctors

        public PwaClientEatCalculatorDbContextCacheSynchronizerMediatorActionWrapper(
            PwaClientEatCalculatorDbContextCacheSynchronizer pwaClientEatCalculatorDbContextCacheSynchronizer)
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
