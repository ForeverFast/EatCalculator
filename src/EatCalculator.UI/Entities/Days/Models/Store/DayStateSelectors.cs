using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor.Selectors;

namespace EatCalculator.UI.Entities.Days.Models.Store
{
    internal static class DayStateSelectors
    {
        public static ISelector<DayState> SelectFeatureState { get; private set; }
            = SelectorFactory.CreateFeatureSelector<DayState>();

        public static ISelector<Day> SelectCurrentDay { get; private set; }
            = SelectorFactory.CreateSelector(SelectFeatureState,
                state => state.Entities.Values.FirstOrDefault(x => x.Equals(state.CurrentDayId))!);

        public static ISelector<List<Day>> SelectDays { get; private set; }
            = SelectorFactory.CreateSelector(SelectFeatureState, state => state.Entities.Values.ToList());
    }
}
