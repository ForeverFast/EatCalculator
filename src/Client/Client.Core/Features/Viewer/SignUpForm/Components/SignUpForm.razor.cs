using Client.Core.Entities.Viewer.Models.Store;
using Client.Core.Entities.Viewer.Models.Store.Actions;
using Client.Core.Features.Viewer.SignUpForm.Models;
using Client.Core.Shared.Lib;
using Client.Core.Shared.Lib.BaseComponents;

namespace Client.Core.Features.Viewer.SignUpForm
{
    public partial class SignUpForm : BaseFluxorComponent
    {
        #region Injects

        [Inject] NavigationManager _navigationManager { get; init; } = null!;

        [Inject] ViewerStateFacade _viewerStateFacade { get; init; } = null!;

        [Inject] BaseValidator<SignUpViewModel> _signUpViewModelValidator { get; init; } = null!;

        #endregion

        #region UI Fields

        private MudForm? _signUpForm;
        private SignUpViewModel _signUpViewModel = new();
        private bool _checking = false;
        private List<string> _errorMessages = new();

        #endregion

        #region LC Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            SubscribeToAction<SignUpFailureAction>(OnSignUpFailureAction);
            SubscribeToAction<SignUpSuccessAction>(OnSignUpSuccessAction);
        }

        #endregion

        #region External events

        private void OnSignUpFailureAction(SignUpFailureAction action)
            => _errorMessages = action.Messages;

        private void OnSignUpSuccessAction(SignUpSuccessAction action)
            => _navigationManager.NavigateToIndexPage();

        #endregion

        #region Internal events

        private async Task OnSubmit()
        {
            if (_signUpForm == null)
                return;

            _errorMessages.Clear();

            await _signUpForm.Validate();
            if (!_signUpForm.IsValid)
                return;

            _viewerStateFacade.SignUp(_signUpViewModel.UserName, _signUpViewModel.Email, _signUpViewModel.Password);
        }

        private void OnNavigateToSignInButtonClick()
            => _navigationManager.NavigateToSignInPage();

        #endregion
    }
}
