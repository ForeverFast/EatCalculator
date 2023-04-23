namespace Client.Core.Features.Viewer.SignInForm.Models
{
    internal sealed class SignInViewModelValidator : BaseValidator<SignInViewModel>
    {
        public SignInViewModelValidator()
        {
            RuleFor(x => x.Login)
                .NotEmpty().WithMessage("Введите логин");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Введите пароль");
        }
    }
}
