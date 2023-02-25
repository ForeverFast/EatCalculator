using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor;

namespace EatCalculator.UI.Entities.Products.Models.Store.Actions
{
    internal record LoadProductsAction : BaseAction;
    internal record LoadProductsFailureAction : BaseFailureAction;
    internal record LoadProductsSuccessAction : BaseSuccessAction
    {
        public required IEnumerable<Product> Products { get; init; }
    }
}
