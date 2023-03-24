using EatCalculator.UI.Entities.Products.Models.Store;
using EatCalculator.UI.Shared.Api.Models;

namespace EatCalculator.UI.Features.Meals
{
    public partial class MealPortionsInfo
    {
        #region Params

        [Parameter, EditorRequired] public required Day Day { get; init; }
        [Parameter, EditorRequired] public required Meal Meal { get; init; }

        #endregion

        #region Injects

        [Inject] ProductStateFacade _productStateFacade { get; init; } = null!;

        #endregion

        #region UI Fields

        private double _proteinGramsForMeal;
        private double _fatGramsForMeal;
        private double _carbohydrateGramsForMeal;

        #endregion

        #region LC Methods

        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            _proteinGramsForMeal = GetPFCGramsValueForMeal(_kkal, Day.ProteinPercentages, _proteinKoef, Day.ProteinMealCount);
            _fatGramsForMeal = GetPFCGramsValueForMeal(_kkal, Day.FatPercentages, _fatKoef, Day.FatMealCount);
            _carbohydrateGramsForMeal = GetPFCGramsValueForMeal(_kkal, Day.CarbohydratePercentages, _carbohydrateKoef, Day.CarbohydrateMealCount);
        }

        #endregion


        // TODO: вынести куда-нибудь
        private double _kkal = 2500.0;
        private double _defaultPer = 100.0;
        private double _proteinKoef = 4;
        private double _fatKoef = 9;
        private double _carbohydrateKoef = 4;

        private double GetPFCGramsValueForDay(double totalKkal, double pfcValuePercentage, double kkalKoef)
            => true switch
            {
                { } when pfcValuePercentage == 0 => 0,
                _ => totalKkal * (pfcValuePercentage / 100) / kkalKoef,
            };

        private double GetPFCGramsValueForMeal(double totalKkal, double pfcValuePercentage, double kkalKoef, int mealsCount)
        {
            var pfcGramsValueForDay = GetPFCGramsValueForDay(totalKkal, pfcValuePercentage, kkalKoef);

            return true switch
            {
                { } when pfcGramsValueForDay == 0 || mealsCount == 0 => 0,
                _ => pfcGramsValueForDay / mealsCount,
            };
        }

        private double GetPFCGramsValueForProduct(double pfcValue, double per)
            => true switch
            {
                { } when pfcValue == 0 => 0,
                { } when per != _defaultPer => (pfcValue * _defaultPer / per),
                _ => pfcValue
            };

        private double GetPFCGramsValueForPortion(double pfcGramsForMeal, double pfcGramsForProduct, double per, double pfcPortionPercentage)
        {
            pfcGramsForProduct = GetPFCGramsValueForProduct(pfcGramsForProduct, per);
            if (pfcPortionPercentage == 0)
                return 0.0;

            return pfcGramsForMeal * _defaultPer / pfcGramsForProduct * (pfcPortionPercentage / 100);
        }

        private string GetPortionsInfo(Portion portion, Product product)
        {
            var proteinGramsTotal = GetPFCGramsValueForPortion(_proteinGramsForMeal, product.Protein, product.Grams, portion.ProteinPercentages);
            var fatGramsTotal = GetPFCGramsValueForPortion(_fatGramsForMeal, product.Fat, product.Grams, portion.FatPercentages);
            var carbohydrateGramsTotal = GetPFCGramsValueForPortion(_carbohydrateGramsForMeal, product.Carbohydrate, product.Grams, portion.CarbohydratePercentages);

            return $"{Math.Round(proteinGramsTotal, 2)} / {Math.Round(fatGramsTotal, 2)} / {Math.Round(carbohydrateGramsTotal, 2)}";
        }
    }
}
