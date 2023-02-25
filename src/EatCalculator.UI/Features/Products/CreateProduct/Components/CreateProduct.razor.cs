using EatCalculator.UI.Entities.Products.Models.Contracts;
using EatCalculator.UI.Entities.Products.Models.Store;
using EatCalculator.UI.Features.Products.CreateProduct.Models;
using EatCalculator.UI.Shared.Lib.Validation;

namespace EatCalculator.UI.Features.Products.CreateProduct.Components
{
    public partial class CreateProduct
    {
        #region Params

        [Parameter] public required RenderFragment ActivatorContent { get; set; }

        #region Injects

        [Inject] ProductStateFacade _productStateFacade { get; init; } = null!;

        [Inject] BaseValidator<CreateProductViewModel> _createProductViewModelValidator { get; init; } = null!;

        #endregion

        #endregion

        #region UI Fields

        private MudDialog? _createProductDialog;
        private bool _createProductDialogOpened;

        private MudForm? _createProductForm;
        private CreateProductViewModel? _createProductViewModel;
        private bool _saving = false;

        #endregion

        #region Internal events

        private void OnClick()
        {
            _createProductDialogOpened = true;
            _createProductViewModel = new CreateProductViewModel { };
        }

        private void OnCancelButtonClick()
            => Close();

        private void OnDialogBackdropClick()
            => Close();

        private async void OnSubmit()
        {
            if (_createProductForm == null || _createProductViewModel == null)
                return;

            await _createProductForm.Validate();
            if (!_createProductForm.IsValid)
                return;

            _productStateFacade.CreateProduct(new CreateProductContract
            {
                Name = _createProductViewModel.Name,
                Description = _createProductViewModel.Description,
                Grams = _createProductViewModel.Grams,
                Protein = _createProductViewModel.Protein,
                Fat = _createProductViewModel.Fat,
                Carbohydrate = _createProductViewModel.Carbohydrate,
            });
        }

        #endregion

        #region Private methods

        private void Close()
        {
            _createProductDialogOpened = false;
        }

        #endregion

        #region Config

        private readonly DialogOptions _dialogOptions = new() { FullWidth = true };

        #endregion
    }
}
