using EatCalculator.UI.Entities.Products.Models.Contracts;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor;

namespace EatCalculator.UI.Entities.Products.Models.Store.Actions
{
    internal record CreateProductAction : BaseAction
    {
        public required CreateProductContract Product { get; init; }
    }
    internal record CreateProductFailureAction : BaseFailureAction;
    internal record CreateProductSuccessAction : BaseSuccessAction
    {
        public required Product Product { get; init; }
    }
}
