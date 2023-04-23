namespace Client.Core.Features.Viewer.SignUpForm.Models
{
    internal sealed class SignUpViewModelValidator : BaseValidator<SignUpViewModel>
    {
        public SignUpViewModelValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty().WithMessage("Введите имя пользователя")
                .MinimumLength(3).WithMessage("Имя пользователя слишком короткое")
                .MaximumLength(64).WithMessage("Имя пользователя слишком длинное")
                .DisableSpecialChars().WithMessage("Имя пользователя не должно содержать недопустимых символов");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Введите email")
                .EmailAddress().WithMessage("Некорректный email");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Введите пароль")
                .MinimumLength(3).WithMessage("Пароль слишком короткий")
                .MaximumLength(64).WithMessage("Пароль слишком длинный");
        }
    }
}
