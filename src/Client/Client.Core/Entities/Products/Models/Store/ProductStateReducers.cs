using Client.Core.Entities.Products.Models.Store.Actions;

namespace Client.Core.Entities.Products.Models.Store
{
    internal static class ProductStateReducers
    {
        private static readonly EntityAdapter<int, Product> s_adapter = new(product => product.Id);

        [ReducerMethod]
        public static ProductState ReduceLoadProductsAction(ProductState state, LoadProductsSuccessAction _)
            => s_adapter.RemoveAll(state);

        [ReducerMethod]
        public static ProductState ReduceLoadProductsSuccessAction(ProductState state, LoadProductsSuccessAction action)
            => s_adapter.SetAll(state, action.Products);

        [ReducerMethod]
        public static ProductState ReduceCreateProductSuccessAction(ProductState state, CreateProductSuccessAction action)
            => s_adapter.Add(state, action.Product);

        [ReducerMethod]
        public static ProductState ReduceUpdateProductSuccessAction(ProductState state, UpdateProductSuccessAction action)
            => s_adapter.Update(state, action.Product);

        [ReducerMethod]
        public static ProductState ReduceDeleteProductSuccessAction(ProductState state, DeleteProductSuccessAction action)
            => s_adapter.Remove(state, action.Id);
    }
}
