namespace Client.Core.Entities.Products.Models.Store
{
    internal sealed class ProductFeatureState : Feature<ProductState>
    {
        public override string GetName()
            => typeof(ProductState).FullName!;

        protected override ProductState GetInitialState()
            => (ProductState)ProductState.GetAdapter().GetInitialState();
    }
}
