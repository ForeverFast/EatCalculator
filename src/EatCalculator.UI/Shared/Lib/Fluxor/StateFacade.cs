using EatCalculator.UI.Shared.Lib.Fluxor.Selectors;

namespace EatCalculator.UI.Shared.Lib.Fluxor
{
    internal abstract class StateFacade<T> where T : class
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

            StateSelector = _store.SubscribeSelector(StateSelectorPointer);
        }

        #endregion

        protected abstract ISelector<T> StateSelectorPointer { get; }

        public ISelectorSubscription<T> StateSelector { get; }
    }
}
