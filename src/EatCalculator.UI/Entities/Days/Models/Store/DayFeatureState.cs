namespace EatCalculator.UI.Entities.Days.Models.Store
{
    internal sealed class DayFeatureState : Feature<DayState>
    {
        public override string GetName()
            => typeof(DayState).FullName!;

        protected override DayState GetInitialState()
            => (DayState)DayState.GetAdapter().GetInitialState();
    }
}
