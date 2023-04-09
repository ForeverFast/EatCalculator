using Client.Core.Entities.Days.Models.Store;
using Client.Core.Shared.Configs;
using Client.Core.Shared.Lib;
using Client.Core.Shared.Lib.BaseComponents;
using System.Globalization;

namespace Client.Core.Pages.Days
{
    [Route($"{Routes.Days.BasePath}/{Routes.Days.Calendar}")]
    public partial class CalendarPage : BasePageComponent
    {
        #region Injects

        [Inject] DayStateFacade _dayStateFacade { get; init; } = null!;

        #endregion

        #region Selectors

        private ISelectorSubscription<List<Day>> _daysSelector
            => _dayStateFacade.Days;

        private ISelectorSubscription<List<DayDateBind>> _dayDateBindsSelector
            => _dayStateFacade.DayDateBinds;

        #endregion

        #region UI Fields

        private int _currentMonth = DateTime.Now.Month;
        private int _currentYear = DateTime.Now.Year;

        private string _currentMonthTitle
            => GetMonthTitle(_currentMonth);
        private DateOnly _previousMonth
            => new DateOnly(_currentYear, _currentMonth, 1).AddMonths(-1);
        private DateOnly _nextMonth
            => new DateOnly(_currentYear, _currentMonth, 1).AddMonths(1);
        private string _previousMonthTitle
            => $"{GetMonthTitle(_previousMonth.Month)} ({_previousMonth.ToString("yyyy.MM")})";
        private string _nextMonthTitle
            => $"{GetMonthTitle(_nextMonth.Month)} ({_nextMonth.ToString("yyyy.MM")})";

        private List<DayDateBindInfo> _dayDateBindsInfo
            => Enumerable.Range(1, DateTime.DaysInMonth(_currentYear, _currentMonth))
                .Select(day =>
                {
                    var targetDate = new DateOnly(_currentYear, _currentMonth, day);
                    var targetDay = _daysSelector.Value.FirstOrDefault(x => x.DayDateBinds.Any(y => y.Date == targetDate));
                    return new DayDateBindInfo
                    {
                        Date = targetDate,
                        Day = targetDay,
                    };
                })
                .ToList();

        #endregion

        #region LC Methods

        #endregion

        #region Internal events

        private void OnChangeMonthButtonClick(bool next)
        {
            var newDate = new DateTime(_currentYear, _currentMonth, 1).AddMonths(next ? 1 : -1);
            _currentYear = newDate.Year;
            _currentMonth = newDate.Month;
        }

        private void OnDayDateBindInfoBlockClick(DayDateBindInfo dayDateBindInfo)
        {
            if (dayDateBindInfo.Day == null)
                return;

            _navigationManager.NavigateToDayPage(dayDateBindInfo.Day.Id);
        }

        private void OnSelectDayForDateMenuButtonClick(Day day, DayDateBindInfo dayDateBindInfo)
            => _dayStateFacade.AttachDayToDate(day.Id, dayDateBindInfo.Date);

        private void OnDetachDayFromDateButtonClick(DayDateBindInfo dayDateBindInfo)
            => _dayStateFacade.DetachDayFromDate(
                dayDateBindInfo.Day!.Id,
                dayDateBindInfo.Day!.DayDateBinds.First(x => x.Date == dayDateBindInfo.Date).Id);

        #endregion

        #region Private methods

        private string GetMonthTitle(int month)
            => CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(month);

        #endregion

        #region Inner models

        private record DayDateBindInfo
        {
            public required DateOnly Date { get; init; }
            public Day? Day { get; init; }

            public string DateWithFormat()
                => Date.ToString("dd MMMM", CultureInfo.CurrentCulture);
        }

        #endregion
    }
}
