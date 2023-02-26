using EatCalculator.UI.Entities.Products.Models.Store.Actions;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor.Effects;
using Microsoft.Extensions.Logging;
using System.Security.Cryptography;

namespace EatCalculator.UI.Entities.Products.Models.Store.Effects
{
    internal sealed class UpdateProductEffect : BaseEffect<UpdateProductAction>
    {
        #region Ctors

        public UpdateProductEffect(BaseEffectInjects injects,
                                   ILogger<BaseEffect<UpdateProductAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(UpdateProductAction action, IDispatcher dispatcher)
        {
            try
            {
                await Task.Yield();

                var updatedProduct = new Product
                {
                    Id = RandomNumberGenerator.GetInt32(100, int.MaxValue),
                    Name = action.Product.Name,
                    Description = action.Product.Description,
                    Grams = action.Product.Grams,
                    Protein = action.Product.Protein,
                    Fat = action.Product.Fat,
                    Carbohydrate = action.Product.Carbohydrate,
                };

                dispatcher.Dispatch(new UpdateProductSuccessAction
                {
                    Product = updatedProduct,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new UpdateProductFailureAction
                {
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
