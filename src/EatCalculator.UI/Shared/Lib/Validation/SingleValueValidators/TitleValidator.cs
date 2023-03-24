using FluentValidation;

namespace EatCalculator.UI.Shared.Lib.Validation.SingleValueValidators
{
    internal class TitleValidator : BaseSingleValueValidator<string>
    {
        public TitleValidator(Action<IRuleBuilderInitial<string, string>>? rule = null) : base(rule)
        {
            RuleFor(x => x)
                .NotEmpty().WithMessage("Название не может быть пустым")
                .MinimumLength(2).WithMessage("Название должно быть не меньше 2 символов")
                .MaximumLength(32).WithMessage("Название должно быть не больше 32 символов");
        }
    }
}
