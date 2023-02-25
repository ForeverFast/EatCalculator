using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.EntityAdapter;

namespace EatCalculator.UI.Entities.Portions.Models.Store
{
    internal sealed class PortionEntityAdapter : EntityAdapter<int, Portion>
    {
        protected override Func<Portion, int> SelectId
            => (Portion portion) => portion.Id;

        public override EntityState<int, Portion> GetInitialState()
            => new PortionState { };
    }
}
