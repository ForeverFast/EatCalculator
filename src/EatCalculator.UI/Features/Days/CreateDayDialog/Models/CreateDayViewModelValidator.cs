using EatCalculator.UI.Shared.Lib.Validation;
using FluentValidation;

namespace EatCalculator.UI.Features.Days.CreateDayDialog.Models
{
    internal sealed class CreateDayViewModelValidator : BaseValidator<CreateDayViewModel>
    {
        public CreateDayViewModelValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Название не должно быть пустым")
                .MaximumLength(64).WithMessage("Название не должно превышать 64 символов");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Описание не должно превышать 500 символов");

            RuleFor(x => x.MealCount)
                .GreaterThan(0).WithMessage("Кол-во должно быть больше 0")
                .When(x => x.MealCount.HasValue);
        }
    }
}
