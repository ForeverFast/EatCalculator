using Client.Core.Entities.Products.Models.Store.Actions;
using Client.Core.Shared.Resources.Localizations;
using Microsoft.Extensions.Logging;

namespace Client.Core.Entities.Products.Models.Store.Effects
{
    internal sealed class CreateProductEffect : BaseEffect<CreateProductAction>
    {
        #region Ctors

        public CreateProductEffect(BaseEffectInjects injects,
                                   ILogger<BaseEffect<CreateProductAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(CreateProductAction action, IDispatcher dispatcher)
        {
            try
            {
                var newProduct = new Product
                {
                    Id = 0,
                    Title = action.Product.Name,
                    Description = action.Product.Description,
                    Grams = action.Product.Grams,
                    Protein = action.Product.Protein,
                    Fat = action.Product.Fat,
                    Carbohydrate = action.Product.Carbohydrate,
                };

                var createdProduct = await _injects.Dal.Instance.For<Product>().Insert.InsertWithObjectAsync(newProduct);

                _injects.Snackbar.Add("Продукт добавлен", MudBlazor.Severity.Normal, config => { config.HideIcon = true; });

                dispatcher.Dispatch(new CreateProductSuccessAction
                {
                    Product = createdProduct,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new CreateProductFailureAction
                {
                    Messages = new List<string>
                    {
                        _injects.Localizer[nameof(DefaultLocalization.UnhandledException)]
                    },
                });
            }
        }
    }
}
