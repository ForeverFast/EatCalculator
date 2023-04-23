using Client.Core.Entities.Viewer.Models.Store;
using Client.Core.Entities.Viewer.Models.Store.Actions;
using Client.Core.Features.Viewer.SignInForm.Models;
using Client.Core.Features.Viewer.SignUpForm;
using Client.Core.Shared.Lib;
using Client.Core.Shared.Lib.BaseComponents;
using Client.Core.Shared.Resources.Localizations;
using Microsoft.Extensions.Localization;

namespace Client.Core.Features.Viewer.SignInForm
{
    public partial class SignInForm : BaseFluxorComponent
    {
        #region Injects

        [Inject] NavigationManager _navigationManager { get; init; } = null!;

        [Inject] ViewerStateFacade _viewerStateFacade { get; init; } = null!;

        [Inject] BaseValidator<SignInViewModel> _signInViewModelValidator { get; init; } = null!;

        [Inject] IStringLocalizer<IdentityLocalization> _identityLocalizer { get; init; } = null!;

        #endregion

        #region UI Fields

        private MudForm? _signInForm;
        private SignInViewModel _signInViewModel = new();
        private bool _checking = false;
        private List<string> _errorMessages = new();
        private bool _showPasswordText = false;

        private string _passwordFieldIcon
            => _showPasswordText
            ? Icons.Material.Filled.Visibility
            : Icons.Material.Filled.VisibilityOff;

        private InputType _passwordFieldInputType
            => _showPasswordText
            ? InputType.Text
            : InputType.Password;

        #endregion

        #region LC Methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            SubscribeToAction<SignInFailureAction>(OnSignInFailureAction);
            SubscribeToAction<SignInSuccessAction>(OnSignInSuccessAction);
        }

        #endregion

        #region External events

        private void OnSignInFailureAction(SignInFailureAction action)
            => _errorMessages = action.Messages;

        private void OnSignInSuccessAction(SignInSuccessAction _)
            => _navigationManager.NavigateToIndexPage();

        #endregion

        #region Internal events

        private void OnPasswordFieldAdornmentIconClick()
            => _showPasswordText = !_showPasswordText;

        private async Task OnSubmit()
        {
            if (_signInForm == null)
                return;

            _errorMessages.Clear(); 

            await _signInForm.Validate();
            if (!_signInForm.IsValid)
                return;

            _viewerStateFacade.SignIn(_signInViewModel.Login, _signInViewModel.Password);
        }

        private void OnNavigateToSignUpButtonClick()
            => _navigationManager.NavigateToSignUpPage();

        #endregion
    }
}
