namespace Client.Core.Entities.Days.Models.Store
{
    internal sealed class DayStateEntityAdapter : EntityAdapter<int, Day>
    {
        protected override Func<Day, int> SelectId
            => day => day.Id;

        public override EntityState<int, Day> GetInitialState()
            => new DayState { };
    }
}
