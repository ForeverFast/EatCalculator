using Client.Core.Entities.Products.Models.Contracts;
using Client.Core.Entities.Products.Models.Store;
using Client.Core.Entities.Products.Models.Store.Actions;
using Client.Core.Features.Products.CreateProductDialog.Models;
using Client.Core.Shared.Lib.BaseComponents;

namespace Client.Core.Features.Products.CreateProductDialog
{
    public partial class CreateProductDialog : BaseDialogComponent
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

        #region External events

        private void OnCreateProductSuccessAction(CreateProductSuccessAction action)
            => Close();

        #endregion

        #region Internal events

        private void OnBackButtonClick()
            => Close();

        private async Task OnSubmit()
        {
            if (_createProductForm == null)
                return;

            await _createProductForm.Validate();
            if (!_createProductForm.IsValid)
                return;

            _productStateFacade.CreateProduct(new CreateProductContract
            {
                Name = _createProductViewModel.Title,
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
            => MudDialog.Close();

        #endregion
    }
}
