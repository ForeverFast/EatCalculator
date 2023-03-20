using EatCalculator.UI.Shared.Lib.Fluxor.Selectors;

namespace EatCalculator.UI.Shared.Lib.Fluxor
{
    internal abstract class StateFacade<T> : IDisposable
        where T : class
    {
        #region Injects

        protected readonly IStore _store;
        protected readonly IDispatcher _dispatcher;

        #endregion

        #region Ctors

        protected StateFacade(IStore store, IDispatcher dispatcher)
        {
            _store = store;
            _dispatcher = dispatcher;

            State = _store.SubscribeSelector(SelectState);
        }

        #endregion

        protected abstract ISelector<T> SelectState { get; }

        public ISelectorSubscription<T> State { get; }

        public virtual void Dispose()
        {
            State?.Dispose();
        }
    }
}
