namespace Client.Core.Entities.Products.Models.Store.Actions
{
    internal record DeleteProductAction : BaseAction
    {
        public required int Id { get; init; }
    }
    internal record DeleteProductFailureAction : BaseFailureAction;
    internal record DeleteProductSuccessAction : BaseSuccessAction
    {
        public required int Id { get; init; }
    }

    internal record DeleteProductsAction : BaseAction
    {
        public required List<int> Ids { get; init; }
    }
    internal record DeleteProductsFailureAction : BaseFailureAction;
    internal record DeleteProductsSuccessAction : BaseSuccessAction
    {
        public required List<int> Id { get; init; }
    }
}
