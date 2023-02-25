using EatCalculator.UI.Shared.Lib.Validation;
using FluentValidation;

namespace EatCalculator.UI.Features.Products.CreateProduct.Models
{
    internal sealed class CreateProductViewModelValidator : BaseValidator<CreateProductViewModel>
    {
        public CreateProductViewModelValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .MaximumLength(64);

            RuleFor(x => x.Grams)
                .GreaterThan(0.0);
            RuleFor(x => x.Protein)
                .GreaterThan(0.0);
            RuleFor(x => x.Fat)
                .GreaterThan(0.0);
            RuleFor(x => x.Carbohydrate)
                .GreaterThan(0.0);
        }
    }
}
