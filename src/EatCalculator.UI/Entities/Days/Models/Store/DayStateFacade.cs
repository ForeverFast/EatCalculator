using EatCalculator.UI.Entities.Days.Models.Contracts;
using EatCalculator.UI.Entities.Days.Models.Store.Actions;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor;
using EatCalculator.UI.Shared.Lib.Fluxor.Selectors;

namespace EatCalculator.UI.Entities.Days.Models.Store
{
    internal sealed class DayStateFacade : StateFacade<DayState>
    {
        #region Ctors

        public DayStateFacade(IStore store, IDispatcher dispatcher) : base(store, dispatcher)
        {
            CurrentDay = _store.SubscribeSelector(DayStateSelectors.SelectCurrentDay);
            Days = _store.SubscribeSelector(DayStateSelectors.SelectDays);
            DayDateBinds = _store.SubscribeSelector(DayStateSelectors.DayDateBinds);
        }

        #endregion

        #region Selectors

        protected override ISelector<DayState> SelectState
            => DayStateSelectors.SelectFeatureState;

        public ISelectorSubscription<Day?> CurrentDay { get; }
        public ISelectorSubscription<List<Day>> Days { get; }
        public ISelectorSubscription<List<DayDateBind>> DayDateBinds { get; }

        #endregion

        public void SelectDay(int dayId)
            => _dispatcher.Dispatch(new SelectDayAction
            {
                DayId = dayId,
            });

        public void LoadDays()
            => _dispatcher.Dispatch(new LoadDaysAction { });

        public void CreateDay(CreateDayContract day)
            => _dispatcher.Dispatch(new CreateDayAction
            {
                Day = day,
            });

        public void UpdateDay(int id, UpdateDayContract day)
            => _dispatcher.Dispatch(new UpdateDayAction
            {
                Id = id,
                Day = day,
            });

        public void DeleteDay(int id)
            => _dispatcher.Dispatch(new DeleteDayAction { Id = id, });

        public void AttachDayToDate(int dayId, DateOnly date)
            => _dispatcher.Dispatch(new AttachDayToDateAction
            {
                DayId = dayId,
                Date = date,
            });

        public void DetachDayFromDate(int dayId, int dayDateBindId)
            => _dispatcher.Dispatch(new DetachDayFromDateAction
            {
                DayId = dayId,
                DayDateBindId = dayDateBindId,
            });

        public override void Dispose()
        {
            base.Dispose();

            CurrentDay.Dispose();
            Days.Dispose();
            DayDateBinds.Dispose();
        }
    }
}
