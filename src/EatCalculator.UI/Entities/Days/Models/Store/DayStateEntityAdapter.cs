using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.EntityAdapter;

namespace EatCalculator.UI.Entities.Days.Models.Store
{
    internal sealed class DayStateEntityAdapter : EntityAdapter<int, Day>
    {
        protected override Func<Day, int> SelectId
            => day => day.Id;

        public override EntityState<int, Day> GetInitialState()
            => new DayState { };
    }
}
