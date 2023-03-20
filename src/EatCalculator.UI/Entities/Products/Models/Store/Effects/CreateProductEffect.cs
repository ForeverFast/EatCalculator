using EatCalculator.UI.Entities.Products.Models.Store.Actions;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor.Effects;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace EatCalculator.UI.Entities.Products.Models.Store.Effects
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

                var createdProduct = await _injects.Dal.For<Product>().Insert.InsertWithObjectAsync(newProduct);

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
                    ErrorMessage = ex.Message,
                });
            }
        }
    }
}
