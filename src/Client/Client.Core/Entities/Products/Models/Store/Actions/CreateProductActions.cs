using Client.Core.Entities.Products.Models.Contracts;

namespace Client.Core.Entities.Products.Models.Store.Actions
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
