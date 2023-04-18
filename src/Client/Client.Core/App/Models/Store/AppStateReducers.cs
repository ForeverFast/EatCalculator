namespace Client.Core.App.Models.Store
{
    internal static class AppStateReducers
    {
        [ReducerMethod]
        public static AppState ReduceInitializeAppAction(AppState state, InitializeAppAction action)
            => state with
            {
                Platform = action.Platform,
            };
    }
}
