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
        }

        #endregion

        public abstract T State { get; }
    }
}
