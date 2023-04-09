using Client.Core.Entities.Products.Models.Store;
using Client.Core.Shared.Lib.Calculator;
using Client.Core.Shared.Lib.Calculator.Models;

namespace Client.Core.Features.Meals.MealPortionsInfo
{
    public partial class MealPortionsInfo
    {
        #region Params

        [Parameter, EditorRequired] public required Day Day { get; init; }
        [Parameter, EditorRequired] public required Meal Meal { get; init; }

        #endregion

        #region Injects

        [Inject] ProductStateFacade _productStateFacade { get; init; } = null!;
        [Inject] ICalculatorService _calculatorService { get; init; } = null!;

        #endregion

        #region UI Fields

        private MealPortionsGramsResult? _mealPortionsGrams;

        #endregion

        #region LC Methods

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            var portionsWithProductInfo = new List<PortionWithProductInfo>();
            foreach (var portion in Meal.Portions)
            {
                var product = _productStateFacade.GetProductById(portion.ProductId);
                if (product == null)
                    return;

                portionsWithProductInfo.Add(new PortionWithProductInfo
                {
                    Portion = portion,
                    Product = product,
                });
            }

            _mealPortionsGrams = _calculatorService.GetMealPortionsGrams(Day, portionsWithProductInfo);
        }

        #endregion

        #region Private methods

        public string FormatPortionGramsResult(PortionGramsResult portionGrams)
            => $"{Math.Round(portionGrams.ProteinGrams, 2)} / {Math.Round(portionGrams.FatGrams, 2)} / {Math.Round(portionGrams.CarbohydrateGrams, 2)}";

        #endregion
    }
}
