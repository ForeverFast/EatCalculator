using Client.Core.Entities.Products.Models.Store.Actions;
using Microsoft.Extensions.Logging;

namespace Client.Core.Entities.Products.Models.Store.Effects
{
    internal sealed class LoadProductsEffect : BaseEffect<LoadProductsAction>
    {
        #region Ctors

        public LoadProductsEffect(BaseEffectInjects injects,
                                  ILogger<BaseEffect<LoadProductsAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(LoadProductsAction action, IDispatcher dispatcher)
        {
            try
            {
                var products = await _injects.Dal.For<Product>().Get.ToListAsync();

                dispatcher.Dispatch(new LoadProductsSuccessAction
                {
                    Products = products,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new LoadProductsFailureAction
                {
                    ErrorMessage = ex.Message,
                });
            }
        }
    }
}
