namespace Client.Core.Entities.Products.Models.Store.Actions
{
    internal record LoadProductsAction : BaseAction;
    internal record LoadProductsFailureAction : BaseFailureAction;
    internal record LoadProductsSuccessAction : BaseSuccessAction
    {
        public required IEnumerable<Product> Products { get; init; }
    }
}
