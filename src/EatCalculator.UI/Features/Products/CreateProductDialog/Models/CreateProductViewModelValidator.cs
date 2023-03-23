using EatCalculator.UI.Shared.Lib.Validation;
using FluentValidation;

namespace EatCalculator.UI.Features.Products.CreateProductDialog.Models
{
    internal sealed class CreateProductViewModelValidator : BaseValidator<CreateProductViewModel>
    {
        public CreateProductViewModelValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Название не должно быть пустым")
                .MaximumLength(128).WithMessage("Название не должно превышать 128 символов");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Описание не должно превышать 500 символов");

            RuleFor(x => x.Grams)
                .GreaterThan(0.0).WithMessage("Кол-во грамм должно быть больше 0");

            RuleFor(x => x.Protein)
                .Must((model, _) => CheckPFC(model)).WithMessage("Хотя бы одно из полей БЖУ должно быть заполнено")
                .GreaterThanOrEqualTo(0.0).WithMessage("Кол-во белков должно быть больше 0");

            RuleFor(x => x.Fat)
                .Must((model, _) => CheckPFC(model)).WithMessage("Хотя бы одно из полей БЖУ должно быть заполнено")
                .GreaterThanOrEqualTo(0.0).WithMessage("Кол-во жиров должно быть больше 0");

            RuleFor(x => x.Carbohydrate)
                .Must((model, _) => CheckPFC(model)).WithMessage("Хотя бы одно из полей БЖУ должно быть заполнено")
                .GreaterThanOrEqualTo(0.0).WithMessage("Кол-во углеводов должно быть больше 0");

        }

        private bool CheckPFC(CreateProductViewModel model)
            => !(model.Protein == 0 && model.Fat == 0 && model.Carbohydrate == 0);
    }
}
