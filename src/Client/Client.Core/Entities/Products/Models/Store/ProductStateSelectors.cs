namespace Client.Core.Entities.Products.Models.Store
{
    internal static class ProductStateSelectors
    {
        public static ISelector<ProductState> SelectFeatureState { get; private set; }
            = SelectorFactory.CreateFeatureSelector<ProductState>();

        public static ISelector<List<Product>> SelectProducts { get; private set; }
            = SelectorFactory.CreateSelector(SelectFeatureState, state => state.Entities.Values.ToList());
    }
}
