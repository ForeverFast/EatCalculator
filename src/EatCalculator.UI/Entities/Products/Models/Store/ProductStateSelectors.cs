using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Fluxor.Selectors;

namespace EatCalculator.UI.Entities.Products.Models.Store
{
    internal static class ProductStateSelectors
    {
        public static ISelector<ProductState> SelectFeatureState { get; private set; }
            = SelectorFactory.CreateFeatureSelector<ProductState>();

        public static ISelector<List<Product>> SelectProducts { get; private set; }
            = SelectorFactory.CreateSelector(SelectFeatureState, state => state.Entities.Values.ToList());
    }
}
