using EatCalculator.UI.Entities.Days.Models.Contracts;
using EatCalculator.UI.Entities.Days.Models.Store.Actions;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Components;
using EatCalculator.UI.Shared.Lib.Fluxor;
using EatCalculator.UI.Shared.Lib.Fluxor.Selectors;

namespace EatCalculator.UI.Entities.Days.Models.Store
{
    internal sealed class DayStateFacade : StateFacade<DayState>
    {
        #region Ctors
        
        public DayStateFacade(IStore store, IDispatcher dispatcher) : base(store, dispatcher)
        {
            CurrentDaySelector = _store.SubscribeSelector(DayStateSelectors.SelectCurrentDay);
            ListSelector = _store.SubscribeSelector(DayStateSelectors.SelectDays);
        }

        #endregion

        #region Selectors

        protected override ISelector<DayState> StateSelectorPointer
            => DayStateSelectors.SelectFeatureState;

        public ISelectorSubscription<Day> CurrentDaySelector { get; }
        public ISelectorSubscription<List<Day>> ListSelector { get; }

        #endregion

        public void SelectDay(int dayId)
            => _dispatcher.Dispatch(new SelectDayAction
            {
                DayId = dayId,
            });

        public void LoadDays()
            => _dispatcher.Dispatch(new LoadDaysAction { });

        public void CreateDay(CreateDayContract day)
            => _dispatcher.Dispatch(new CreateDayAction { Day = day, });

        public void UpdateDay(int id, UpdateDayContract day)
            => _dispatcher.Dispatch(new UpdateDayAction
            {
                Id = id,
                Day = day,
            });

        public void DeleteDay(int id)
            => _dispatcher.Dispatch(new DeleteDayAction { Id = id, });



        public void Dispose()
        {
            StateSelector?.Dispose();
            ListSelector.Dispose();
        }
    }
}
