using EatCalculator.UI.Entities.Days.Models.Contracts;
using EatCalculator.UI.Entities.Days.Models.Store;
using EatCalculator.UI.Entities.Days.Models.Store.Actions;
using EatCalculator.UI.Features.Days.CreateDayDialog.Models;
using EatCalculator.UI.Shared.Lib.Validation;

namespace EatCalculator.UI.Features.Days.CreateDayDialog.Components
{
    public partial class CreateDayDialog : BaseFluxorComponent 
    {
        #region Injects

        [Inject] NavigationManager _navigationManager { get; init; } = null!;

        [Inject] DayStateFacade _dayStateFacade { get; init; } = null!;

        [Inject] BaseValidator<CreateDayViewModel> _createDayViewModelValidator { get; init; } = null!;

        #endregion

        #region UI Fields

        private MudDialog? _createDayDialog;
        private bool _createDayDialogOpened;

        private MudForm? _createDayForm;
        private CreateDayViewModel? _createDayViewModel;
        private bool _creating = false;

        #endregion

        #region LC Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            SubscribeToAction<CreateDaySuccessAction>(OnCreateDaySuccessAction);
        }

        #endregion

        #region External events

        private void OnCreateDaySuccessAction(CreateDaySuccessAction _)
            => Close();

        #endregion

        #region Internal events

        private void OnClick()
        {
            _createDayDialogOpened = true;
            _createDayViewModel = new CreateDayViewModel { };
        }

        private void OnCancelButtonClick()
            => Close();

        private void OnDialogBackdropClick()
            => Close();

        private async Task OnSubmit()
        {
            if (_createDayForm == null || _createDayViewModel == null)
                return;

            await _createDayForm.Validate();
            if (!_createDayForm.IsValid)
                return;

            _dayStateFacade.CreateDay(new CreateDayContract
            {
                Title = _createDayViewModel.Title,
                Description = _createDayViewModel.Description,
                MealCount = _createDayViewModel.MealCount,
            });
        }

        #endregion

        #region Private methods

        private void Close()
            => _createDayDialogOpened = false;

        #endregion

        #region Config

        private readonly DialogOptions _dialogOptions = new() { FullScreen = true };

        #endregion
    }
}
