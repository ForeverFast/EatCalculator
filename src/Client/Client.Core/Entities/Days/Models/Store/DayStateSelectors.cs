namespace Client.Core.Entities.Days.Models.Store
{
    internal static class DayStateSelectors
    {
        public static ISelector<DayState> SelectFeatureState { get; private set; }
            = SelectorFactory.CreateFeatureSelector<DayState>();

        public static ISelector<Day?> SelectCurrentDay { get; private set; }
            = SelectorFactory.CreateSelector(SelectFeatureState,
                state => state.Entities.Values.FirstOrDefault(x => x.Id == state.CurrentDayId));

        public static ISelector<List<Day>> SelectDays { get; private set; }
            = SelectorFactory.CreateSelector(SelectFeatureState, state => state.Entities.Values.ToList());

        public static ISelector<List<DayDateBind>> DayDateBinds { get; private set; }
            = SelectorFactory.CreateSelector(SelectDays, state => state.SelectMany(x => x.DayDateBinds).ToList());
    }
}
