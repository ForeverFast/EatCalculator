using EatCalculator.UI.Entities.Products.Models.Store.Actions;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor.Effects;
using Microsoft.Extensions.Logging;

namespace EatCalculator.UI.Entities.Products.Models.Store.Effects
{
    internal sealed class DeleteProductEffect : BaseEffect<DeleteProductAction>
    {
        #region Ctors

        public DeleteProductEffect(BaseEffectInjects injects,
                                   ILogger<BaseEffect<DeleteProductAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(DeleteProductAction action, IDispatcher dispatcher)
        {
            try
            {
                await _injects.Dal.For<Product>().Delete.DeleteAsync(x => x.Id == action.Id);  

                dispatcher.Dispatch(new DeleteProductSuccessAction
                {
                    Id = action.Id
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new DeleteProductFailureAction
                {
                    ErrorMessage = ex.Message
                });
            }
        }
    }
}
