namespace Client.Core.App.Models.Store
{
    internal class AppFeatureState : Feature<AppState>
    {
        public override string GetName()
           => typeof(AppState).FullName!;

        protected override AppState GetInitialState()
            => new AppState { };
    }
}
