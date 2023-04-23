using Client.Core.Entities.Viewer.Models.Store.Actions;

namespace Client.Core.Entities.Viewer.Models.Store
{
    internal static class ViewerStateReducers
    {
        [ReducerMethod]
        public static ViewerState ReduceInitializeViewerSuccessAction(ViewerState state, InitializeViewerSuccessAction action)
            => state with
            {
                Viewer = action.Viewer,
            };

        [ReducerMethod]
        public static ViewerState ReduceSignInSuccessAction(ViewerState state, SignInSuccessAction action)
            => state with
            {
                Viewer = action.Viewer,
            };

        [ReducerMethod]
        public static ViewerState ReduceSignUpSuccessAction(ViewerState state, SignUpSuccessAction action)
            => state with
            {
                Viewer = action.Viewer,
            };

        [ReducerMethod]
        public static ViewerState ReduceViewerEatDataSuccessAction(ViewerState state, ViewerEatDataSuccessAction action)
        {
            if (state.Viewer == null)
                return state;

            return state with
            {
                Viewer = state.Viewer with { LastDbUpdateDate = action.LastDbUpdateDate, },
            };
        }
    }
}
