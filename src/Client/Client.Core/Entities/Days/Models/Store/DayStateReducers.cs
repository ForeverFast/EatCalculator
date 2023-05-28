using Client.Core.Entities.Days.Models.Store.Actions;

namespace Client.Core.Entities.Days.Models.Store
{
    internal static class DayStateReducers
    {
        private static readonly EntityAdapter<int, Day> s_adapter = new(day => day.Id);

        [ReducerMethod]
        public static DayState ReduceSelectDayAction(DayState state, SelectDayAction action)
            => state with
            {
                CurrentDayId = action.DayId,
            };

        [ReducerMethod]
        public static DayState ReduceLoadDaysAction(DayState state, LoadDaysSuccessAction _)
            => s_adapter.RemoveAll(state);

        [ReducerMethod]
        public static DayState ReduceLoadDaysSuccessAction(DayState state, LoadDaysSuccessAction action)
            => s_adapter.SetAll(state, action.Days);

        [ReducerMethod]
        public static DayState ReduceCreateDaySuccessAction(DayState state, CreateDaySuccessAction action)
            => s_adapter.Add(state, action.Day);

        [ReducerMethod]
        public static DayState ReduceUpdateDaySuccessAction(DayState state, UpdateDaySuccessAction action)
            => s_adapter.Update(state, action.Day);

        [ReducerMethod]
        public static DayState ReduceDeleteDaySuccessAction(DayState state, DeleteDaySuccessAction action)
            => s_adapter.Remove(state, action.Id);

        [ReducerMethod]
        public static DayState ReduceAttachDayToDateSuccessAction(DayState state, AttachDayToDateSuccessAction action)
            => s_adapter.Map(state, action.DayDateBind.DayId, day => day with
            {
                DayDateBinds = day.DayDateBinds.Append(action.DayDateBind).ToList(),
            });

        [ReducerMethod]
        public static DayState ReduceDetachDayFromDateSuccessAction(DayState state, DetachDayFromDateSuccessAction action)
            => s_adapter.Map(state, action.DayId, day => day with
            {
                DayDateBinds = day.DayDateBinds.Where(x => x.Id != action.DayDateBindId).ToList(),
            });
    }
}
