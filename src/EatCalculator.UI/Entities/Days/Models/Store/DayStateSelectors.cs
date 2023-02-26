using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Components;
using EatCalculator.UI.Shared.Lib.Fluxor.Selectors;

namespace EatCalculator.UI.Entities.Days.Models.Store
{
    internal static class DayStateSelectors
    {
        public static ISelector<DayState> SelectFeatureState { get; private set; }
            = SelectorFactory.CreateFeatureSelector<DayState>();

        public static ISelector<List<Day>> SelectProducts { get; private set; }
            = SelectorFactory.CreateSelector(SelectFeatureState, state => state.Entities.Values.ToList());

        public static ISelector<LoadingState> SelectProductsLoadingState { get; private set; }
            = SelectorFactory.CreateSelector(SelectFeatureState, state => state.LoadingState);
    }
}
