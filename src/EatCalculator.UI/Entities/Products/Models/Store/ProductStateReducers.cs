using EatCalculator.UI.Entities.Products.Models.Store.Actions;
using EatCalculator.UI.Shared.Components;

namespace EatCalculator.UI.Entities.Products.Models.Store
{
    internal static class ProductStateReducers
    {
        private static ProductStateEntityAdapter s_adapter => (ProductStateEntityAdapter)ProductState.GetAdapter();

        [ReducerMethod]
        public static ProductState ReduceLoadProductsAction(ProductState state, LoadProductsSuccessAction action)
            => s_adapter.RemoveAll<ProductState>(state) with
            {
                LoadingState = LoadingState.Loading,
            };

        [ReducerMethod]
        public static ProductState ReduceLoadProductsFailureAction(ProductState state, LoadProductsFailureAction action)
            => state with
            {
                LoadingState = LoadingState.Error
            };

        [ReducerMethod]
        public static ProductState ReduceLoadProductsSuccessAction(ProductState state, LoadProductsSuccessAction action)
            => s_adapter.SetAll<ProductState>(action.Products, state) with
            {
                LoadingState = LoadingState.Content
            };




        [ReducerMethod]
        public static ProductState ReduceCreateProductSuccessAction(ProductState state, CreateProductSuccessAction action)
            => s_adapter.Add<ProductState>(action.Product, state);

        [ReducerMethod]
        public static ProductState ReduceUpdateProductSuccessAction(ProductState state, UpdateProductSuccessAction action)
            => s_adapter.Update<ProductState>(action.Product, state);

        [ReducerMethod]
        public static ProductState ReduceDeleteProductSuccessAction(ProductState state, DeleteProductSuccessAction action)
            => s_adapter.Remove<ProductState>(action.Id, state);
    }
}
