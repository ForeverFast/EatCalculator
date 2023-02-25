using EatCalculator.UI.Entities.Products.Models.Store.Actions;
using EatCalculator.UI.Shared.Components;

namespace EatCalculator.UI.Entities.Products.Models.Store
{
    internal static class ProductReducers
    {
        public static ProductEntityAdapter s_adapter => (ProductEntityAdapter)ProductState.GetAdapter();

        [ReducerMethod]
        public static ProductState ReduceLoadProductsSuccessAction(ProductState state, LoadProductsSuccessAction action)
            => s_adapter.SetAll<ProductState>(action.Products, state) with
            {
                LoadingState = LoadingState.Content
            };

        [ReducerMethod]
        public static ProductState ReduceCreateProductSuccessAction(ProductState state, CreateProductSuccessAction action)
        {
            ProductState.GetAdapter().Add(action.Product, state);

            return state with { };
        }

        [ReducerMethod]
        public static ProductState ReduceUpdateProductSuccessAction(ProductState state, UpdateProductSuccessAction action)
        {
            ProductState.GetAdapter().Update(action.Product, state);

            return state with { };
        }

        [ReducerMethod]
        public static ProductState ReduceDeleteProductSuccessAction(ProductState state, DeleteProductSuccessAction action)
        {
            ProductState.GetAdapter().Remove(action.Id, state);

            return state with { };
        }
    }
}
