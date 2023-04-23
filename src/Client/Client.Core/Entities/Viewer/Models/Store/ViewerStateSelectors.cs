namespace Client.Core.Entities.Viewer.Models.Store
{
    internal static class ViewerStateSelectors
    {
        public static ISelector<ViewerState> SelectFeatureState { get; private set; }
             = SelectorFactory.CreateFeatureSelector<ViewerState>();
    }
}
