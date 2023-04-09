using Client.Core.Entities.Days.Models.Contracts;
using Client.Core.Entities.Days.Models.Store;
using Client.Core.Entities.Days.Models.Store.Actions;
using Client.Core.Features.Days.CreateDayDialog.Models;
using Client.Core.Shared.Lib.BaseComponents;

namespace Client.Core.Features.Days.CreateDayDialog
{
    public partial class CreateDayDialog : BaseDialogComponent
    {
        #region Injects

        [Inject] NavigationManager _navigationManager { get; init; } = null!;

        [Inject] DayStateFacade _dayStateFacade { get; init; } = null!;

        [Inject] BaseValidator<CreateDayViewModel> _createDayViewModelValidator { get; init; } = null!;

        #endregion

        #region UI Fields

        private MudForm? _createDayForm;
        private CreateDayViewModel _createDayViewModel = new();
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

        private void OnBackButtonClick()
            => Close();

        private async Task OnSubmit()
        {
            if (_createDayForm == null)
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
            => MudDialog.Close();

        #endregion
    }
}
