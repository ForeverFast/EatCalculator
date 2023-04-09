namespace Client.Core.Entities.Products.Models.Store
{
    internal sealed class ProductStateEntityAdapter : EntityAdapter<int, Product>
    {
        protected override Func<Product, int> SelectId
            => product => product.Id;

        public override EntityState<int, Product> GetInitialState()
            => new ProductState { };
    }
}
