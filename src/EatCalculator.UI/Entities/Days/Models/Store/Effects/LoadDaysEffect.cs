using EatCalculator.UI.Entities.Products.Models.Store.Actions;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor.Effects;
using Microsoft.Extensions.Logging;

namespace EatCalculator.UI.Entities.Days.Models.Store.Effects
{
    internal sealed class LoadDaysEffect : BaseEffect<LoadProductsAction>
    {
        #region Ctors

        public LoadDaysEffect(BaseEffectInjects injects,
                                  ILogger<BaseEffect<LoadProductsAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(LoadProductsAction action, IDispatcher dispatcher)
        {
            try
            {
                await Task.Yield();

                var productsList = new List<Product>
                {
                    new Product
                    {
                        Id = 0,
                        Name = "Гречка",
                        Grams = 100,
                        Protein = 50,
                        Fat = 20,
                        Carbohydrate = 120,
                    },
                    new Product
                    {
                        Id = 1,
                        Name = "Творог",
                        Grams = 100,
                        Protein = 150,
                        Fat = 10,
                        Carbohydrate = 10,
                    },

                    new Product
                    {
                        Id = 2,
                        Name = "Рис",
                        Grams = 100,
                        Protein = 50,
                        Fat = 50,
                        Carbohydrate = 50,
                    },
                };

                dispatcher.Dispatch(new LoadProductsSuccessAction
                {
                    Products = productsList,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new LoadProductsFailureAction
                {
                    ErrorMessage = "",
                });
            }
        }
    }
}
