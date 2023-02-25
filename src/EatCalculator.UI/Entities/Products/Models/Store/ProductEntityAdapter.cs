using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.EntityAdapter;

namespace EatCalculator.UI.Entities.Products.Models.Store
{
    internal sealed class ProductEntityAdapter : EntityAdapter<int, Product>
    {
        protected override Func<Product, int> SelectId
            => product => product.Id;

        public override EntityState<int, Product> GetInitialState()
            => new ProductState { };
    }
}
