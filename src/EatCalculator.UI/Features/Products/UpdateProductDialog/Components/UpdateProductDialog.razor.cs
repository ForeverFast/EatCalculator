using EatCalculator.UI.Entities.Products.Models.Contracts;
using EatCalculator.UI.Entities.Products.Models.Store;
using EatCalculator.UI.Entities.Products.Models.Store.Actions;
using EatCalculator.UI.Features.Products.UpdateProductDialog.Models;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Validation;

namespace EatCalculator.UI.Features.Products.UpdateProductDialog.Components
{
    public partial class UpdateProductDialog : BaseDialogComponent
    {
        #region Params 

        [Parameter, EditorRequired] public required Product Product { get; set; }

        #endregion

        #region Injects

        [Inject] ProductStateFacade _productStateFacade { get; init; } = null!;

        [Inject] BaseValidator<UpdateProductViewModel> _updateProductViewModelValidator { get; init; } = null!;

        #endregion

        #region UI Fields

        private MudForm? _updateProductForm;
        private UpdateProductViewModel _updateProductViewModel = new();
        private bool _saving = false;

        #endregion

        #region State methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            SubscribeToAction<UpdateProductSuccessAction>(OnUpdateProductSuccessAction);

            _updateProductViewModel.Title = Product.Title;
            _updateProductViewModel.Description = Product.Description;
            _updateProductViewModel.Grams = Product.Grams;
            _updateProductViewModel.Protein = Product.Protein;
            _updateProductViewModel.Fat = Product.Fat;
            _updateProductViewModel.Carbohydrate = Product.Carbohydrate;
        }

        #endregion

        #region External events

        private void OnUpdateProductSuccessAction(UpdateProductSuccessAction _)
            => Close();

        #endregion

        #region Internal events

        private void OnBackButtonClick()
            => Close();

        private async Task OnSubmit()
        {
            if (_updateProductForm == null)
                return;

            await _updateProductForm.Validate();
            if (!_updateProductForm.IsValid)
                return;

            _productStateFacade.UpdateProduct(Product.Id, new UpdateProductContract
            {
                Title = _updateProductViewModel.Title,
                Description = _updateProductViewModel.Description,
                Grams = _updateProductViewModel.Grams,
                Protein = _updateProductViewModel.Protein,
                Fat = _updateProductViewModel.Fat,
                Carbohydrate = _updateProductViewModel.Carbohydrate,
            });
        }

        #endregion

        #region Private methods

        private void Close()
            => MudDialog.Close();

        #endregion

        #region Config

        public static readonly DialogOptions DialogOptions = new()
        {
            FullScreen = true,
            FullWidth = true,
            NoHeader = true,
            MaxWidth = MaxWidth.False
        };

        #endregion
    }
}
