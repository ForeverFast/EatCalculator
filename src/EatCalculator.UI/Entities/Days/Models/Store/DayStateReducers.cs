using EatCalculator.UI.Entities.Days.Models.Store.Actions;

namespace EatCalculator.UI.Entities.Days.Models.Store
{
    internal static class DayStateReducers
    {
        private static DayStateEntityAdapter s_adapter => (DayStateEntityAdapter)DayState.GetAdapter();



        [ReducerMethod]
        public static DayState ReduceSelectDayAction(DayState state, SelectDayAction action)
            => state with
            {
                CurrentDayId = action.DayId,
            };



        [ReducerMethod]
        public static DayState ReduceLoadDaysAction(DayState state, LoadDaysSuccessAction _)
            => s_adapter.RemoveAll<DayState>(state);

        [ReducerMethod]
        public static DayState ReduceLoadDaysSuccessAction(DayState state, LoadDaysSuccessAction action)
            => s_adapter.SetAll<DayState>(action.Days, state);



        [ReducerMethod]
        public static DayState ReduceCreateDaySuccessAction(DayState state, CreateDaySuccessAction action)
            => s_adapter.Add<DayState>(action.Day, state);

        [ReducerMethod]
        public static DayState ReduceUpdateDaySuccessAction(DayState state, UpdateDaySuccessAction action)
            => s_adapter.Update<DayState>(action.Day, state);

        [ReducerMethod]
        public static DayState ReduceDeleteDaySuccessAction(DayState state, DeleteDaySuccessAction action)
            => s_adapter.Remove<DayState>(action.Id, state);
    }
}
