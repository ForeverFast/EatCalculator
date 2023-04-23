namespace Client.Core.Entities.Viewer.Models.Store
{
    internal class ViewerFeatureState : Feature<ViewerState>
    {
        public override string GetName()
           => typeof(ViewerState).FullName!;

        protected override ViewerState GetInitialState()
            => new ViewerState { };
    }
}
