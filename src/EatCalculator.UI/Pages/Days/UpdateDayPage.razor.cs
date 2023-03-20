using EatCalculator.UI.Entities.Days.Models.Store;
using EatCalculator.UI.Entities.Meals.Models.Store;
using EatCalculator.UI.Features.Meals.UpdateMealDialog.Components;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Configs;
using EatCalculator.UI.Shared.Lib.Fluxor.Selectors;
using System.ComponentModel;

namespace EatCalculator.UI.Pages.Days
{
    [Route($"{Routes.Days.BasePath}/{{DayId:int}}/{Routes.Days.Update}")]
    public partial class UpdateDayPage : BasePageComponent
    {
        #region Params

        [Parameter] public int DayId { get; set; }

        #endregion

        #region Injects

        [Inject] IDialogService _dialogService { get; init; } = null!;

        [Inject] DayStateFacade _dayStateFacade { get; init; } = null!;
        [Inject] MealStateFacade _mealStateFacade { get; init; } = null!;

        #endregion

        #region UI Fields

        #endregion

        #region Selectors

        private ISelectorSubscription<Day?> _currentDay
            => _dayStateFacade.CurrentDay;

        private ISelectorSubscription<List<Meal>> _meals
            => _mealStateFacade.Meals;

        #endregion

        #region State methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _currentDay.PropertyChanged += OnCurrentDayChanged;
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (DayId != _currentDay.Value?.Id)
            {
                _dayStateFacade.SelectDay(DayId);
                return;
            }

            LoadDayData();
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            _currentDay.PropertyChanged -= OnCurrentDayChanged;
        }

        #endregion

        #region External events

        private void OnCurrentDayChanged(object? sender, PropertyChangedEventArgs e)
           => LoadDayData();

        #endregion

        #region Internal events

        private void OnDeleteDayButtonClick()
        {

        }

        private void OnEditMealButtonClick(Meal meal)
            => _dialogService.Show<UpdateMealDialog>(
                "",
                new DialogParameters
                {
                    { nameof(UpdateMealDialog.Meal), meal },
                },
                UpdateMealDialog.DialogOptions);

        #endregion

        #region Private methods

        private void LoadDayData()
        {
            if (_currentDay.Value == null)
                return;

            _mealStateFacade.LoadMeals(_currentDay.Value.Id);
        }

        #endregion
    }
}
