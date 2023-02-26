using EatCalculator.UI.Entities.Products.Models.Contracts;
using EatCalculator.UI.Entities.Products.Models.Store;
using EatCalculator.UI.Entities.Products.Models.Store.Actions;
using EatCalculator.UI.Features.Products.CreateProductForm.Models;
using EatCalculator.UI.Shared.Lib.Validation;

namespace EatCalculator.UI.Features.Products.CreateProductForm.Components
{
    public partial class CreateProductForm : BaseFluxorComponent
    {
        #region Injects

        [Inject] NavigationManager _navigationManager { get; init; } = null!;

        [Inject] ProductStateFacade _productStateFacade { get; init; } = null!;

        [Inject] BaseValidator<CreateProductViewModel> _createProductViewModelValidator { get; init; } = null!;

        #endregion

        #region UI Fields

        private MudForm? _createProductForm;
        private CreateProductViewModel _createProductViewModel = new();
        private bool _saving = false;

        #endregion

        #region State methods

        protected override void OnInitialized()
        {
            base.OnInitialized();

            SubscribeToAction<CreateProductSuccessAction>(OnCreateProductSuccessAction);
        }    

        #endregion

        #region Internal events

        private async void OnSubmit()
        {
            if (_createProductForm == null)
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

        #region External events

        private void OnCreateProductSuccessAction(CreateProductSuccessAction action)
        {
            //_navigationManager.NavigateTo()
        }

        #endregion
    }
}
