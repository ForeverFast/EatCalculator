namespace UI
{
    public static class LoadingStateHelper
    {
        public static bool IsNoDataState(this LoadingState loadingState)
            => loadingState switch
            {
                { } when loadingState is LoadingState.NotTriggered or LoadingState.NoData => true,
                _ => false,
            };

        public static LoadingState HandleLoadingStates(params LoadingState[] loadingStates)
            => loadingStates switch
            {
                { } when loadingStates.All(x => x == LoadingState.Content) => LoadingState.Content,
                { } when loadingStates.All(x => x == LoadingState.NoData) => LoadingState.NoData,
                { } when loadingStates.All(x => x == LoadingState.NotTriggered) => LoadingState.NotTriggered,
                { } when loadingStates.Any(x => x == LoadingState.Error) => LoadingState.Error,
                _ => LoadingState.Loading,
            };

        public static LoadingState HandleLoadingStates(Func<bool> func, params LoadingState[] loadingStates)
        {
            var loadingStateResult = HandleLoadingStates(loadingStates);

            return loadingStateResult switch
            {
                { } when loadingStateResult == LoadingState.Content && func() => LoadingState.Content,
                { } when loadingStateResult != LoadingState.Content => loadingStateResult,
                _ => LoadingState.Loading
            };
        }
    }
}
