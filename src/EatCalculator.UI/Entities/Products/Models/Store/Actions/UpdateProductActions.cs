using EatCalculator.UI.Entities.Products.Models.Contracts;
using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor;

namespace EatCalculator.UI.Entities.Products.Models.Store.Actions
{
    internal record UpdateProductAction : BaseAction
    {
        public required int Id { get; init; }
        public required UpdateProductContract Product { get; init; }
    }
    internal record UpdateProductFailureAction : BaseFailureAction;
    internal record UpdateProductSuccessAction : BaseSuccessAction
    {
        public required Product Product { get; init; }
    }
}
