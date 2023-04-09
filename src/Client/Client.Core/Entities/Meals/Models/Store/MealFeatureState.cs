namespace Client.Core.Entities.Meals.Models.Store
{
    internal sealed class MealFeatureState : Feature<MealState>
    {
        public override string GetName()
            => typeof(MealState).FullName!;

        protected override MealState GetInitialState()
            => (MealState)MealState.GetAdapter().GetInitialState();
    }
}
