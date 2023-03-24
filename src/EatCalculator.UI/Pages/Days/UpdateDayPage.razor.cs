using EatCalculator.UI.Entities.Days.Models.Contracts;
using EatCalculator.UI.Entities.Days.Models.Store;
using EatCalculator.UI.Entities.Meals.Models.Store;
using EatCalculator.UI.Features.Meals.UpdateMealDialog.Components;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Configs;
using EatCalculator.UI.Shared.Lib;
using EatCalculator.UI.Shared.Lib.Fluxor.Selectors;
using EatCalculator.UI.Shared.Lib.Validation.SingleValueValidators;
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

        private string _title = string.Empty;
        private TitleValidator _titleValidator = null!;

        private double _proteinPercentages;
        private double _fatPercentages;
        private double _carbohydratePercentages;

        private int _proteinMealCount;
        private int _fatMealCount;
        private int _carbohydrateMealCount;

        private List<string> _dayValidation = new();

        #endregion

        #region Selectors

        private ISelectorSubscription<Day?> _currentDay
            => _dayStateFacade.CurrentDay;

        private ISelectorSubscription<List<Meal>> _meals
            => _mealStateFacade.Meals;

        #endregion

        #region LC Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            _titleValidator = new();

            _currentDay.PropertyChanged += OnCurrentDayChanged;
            _meals.PropertyChanged += OnMealsChanged;
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

        private void OnMealsChanged(object? sender, PropertyChangedEventArgs e)
            => ValidateDay();

        #endregion

        #region Internal events

        private void OnBackToDaysButtonClick()
            => _navigationManager.NavigateToIndexPage();

        private void OnDeleteDayButtonClick()
        {

        }

        private void OnChangeDayInfo(Action action)
        {
            if (_currentDay.Value == null || !ValidateDay(action))
                return;

            _dayStateFacade.UpdateDay(DayId, new UpdateDayContract
            {
                Title = _title,
                Description = _currentDay.Value.Description,
                ProteinPercentages = _proteinPercentages,
                FatPercentages = _fatPercentages,
                CarbohydratePercentages = _carbohydratePercentages,
                ProteinMealCount = _proteinMealCount,
                FatMealCount = _fatMealCount,
                CarbohydrateMealCount = _carbohydrateMealCount,
            });
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

            _title = _currentDay.Value.Title;

            _proteinPercentages = _currentDay.Value.ProteinPercentages;
            _fatPercentages = _currentDay.Value.FatPercentages;
            _carbohydratePercentages = _currentDay.Value.CarbohydratePercentages;

            _proteinMealCount = _currentDay.Value.ProteinMealCount;
            _fatMealCount = _currentDay.Value.FatMealCount;
            _carbohydrateMealCount = _currentDay.Value.CarbohydrateMealCount;

            _mealStateFacade.LoadMeals(_currentDay.Value.Id);
        }

        private bool ValidateDay(Action? action = null)
        {
            action?.Invoke();
            _dayValidation.Clear();

            bool correctPercentages = _proteinPercentages + _fatPercentages + _carbohydratePercentages == 100.0;
            if (!correctPercentages)
                _dayValidation.Add("Сумма БЖУ в % на день должна быть равна 100%");

            var actualProteinMealCount = _meals.Value.Where(x => x.Portions.Any(x => x.ProteinPercentages > 0)).Count();
            if (actualProteinMealCount > _proteinMealCount)
                _dayValidation.Add("Много белковых приёмов пищи");
            if (actualProteinMealCount < _proteinMealCount)
                _dayValidation.Add("Мало белковых приёмов пищи");

            var actualFatMealCount = _meals.Value.Where(x => x.Portions.Any(x => x.FatPercentages > 0)).Count();
            if (actualFatMealCount > _fatMealCount)
                _dayValidation.Add("Много жировых приёмов пищи");
            if (actualFatMealCount < _fatMealCount)
                _dayValidation.Add("Мало жировых приёмов пищи");

            var actualCarbohydrateMealCount = _meals.Value.Where(x => x.Portions.Any(x => x.CarbohydratePercentages > 0)).Count();
            if (actualCarbohydrateMealCount > _carbohydrateMealCount)
                _dayValidation.Add("Много углеводных приёмов пищи");
            if (actualCarbohydrateMealCount < _carbohydrateMealCount)
                _dayValidation.Add("Мало углеводных приёмов пищи");

            return _dayValidation.Count > 0;
        }

        #endregion
    }
}