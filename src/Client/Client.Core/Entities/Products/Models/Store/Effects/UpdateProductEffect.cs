using Client.Core.Entities.Products.Models.Store.Actions;
using Client.Core.Shared.Resources.Localizations;
using Microsoft.Extensions.Logging;

namespace Client.Core.Entities.Products.Models.Store.Effects
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
                var targetProduct = await _injects.Dal.Instance.For<Product>().Get.FirstOrDefaultAsync(x => x.Id == action.Id)
                    ?? throw new Exception();

                targetProduct = targetProduct with
                {
                    Title = action.Product.Title,
                    Description = action.Product.Description,
                    Grams = action.Product.Grams,
                    Protein = action.Product.Protein,
                    Fat = action.Product.Fat,
                    Carbohydrate = action.Product.Carbohydrate,
                };

                await _injects.Dal.Instance.For<Product>().Update.UpdateAsync(targetProduct);

                dispatcher.Dispatch(new UpdateProductSuccessAction
                {
                    Product = targetProduct,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new UpdateProductFailureAction
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
