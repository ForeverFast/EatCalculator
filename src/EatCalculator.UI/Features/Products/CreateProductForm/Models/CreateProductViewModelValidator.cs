using EatCalculator.UI.Shared.Lib.Validation;
using FluentValidation;

namespace EatCalculator.UI.Features.Products.CreateProductForm.Models
{
    internal sealed class CreateProductViewModelValidator : BaseValidator<CreateProductViewModel>
    {
        public CreateProductViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Название не должно быть пустым")
                .MaximumLength(128).WithMessage("Название не должно превышать 128 символов");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Описание не должно превышать 500 символов");

            RuleFor(x => x.Grams)
                .GreaterThan(0.0).WithMessage("Кол-во грамм должно быть больше 0");

            RuleFor(x => x.Protein)
                .GreaterThan(0.0).WithMessage("Кол-во белков должно быть больше 0");

            RuleFor(x => x.Fat)
                .GreaterThan(0.0).WithMessage("Кол-во жиров должно быть больше 0");

            RuleFor(x => x.Carbohydrate)
                .GreaterThan(0.0).WithMessage("Кол-во углеводов должно быть больше 0");
        }
    }
}
