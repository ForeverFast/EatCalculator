using Client.Core.Entities.Products.Models.Contracts;

namespace Client.Core.Entities.Products.Models.Store.Actions
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
