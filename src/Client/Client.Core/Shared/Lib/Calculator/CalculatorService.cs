using Client.Core.Shared.Lib.Calculator.Models;

namespace Client.Core.Shared.Lib.Calculator
{
    internal sealed class CalculatorService : ICalculatorService
    {
        #region Default for day

        public double DefaultProteinPercentagesForDay => 30.0;
        public double DefaultFatPercentagesForDay => 10.0;
        public double DefaultCarbohydratePercentagesForDay => 60.0;
        public int DefaultMealCountForDay => 3;

        #endregion

        public const double DefaultPerGramsValue = 100.0;

        public double ProteinKkalDivider => 4.0;
        public double FatKkalDivider => 9.0;
        public double CarbohydrateKkalDivider => 4.0;

        public double GetMacronutrientGramsForDay(double kkalForDay, double macronutrientPercentage, double macronutrientDivider)
            => true switch
            {
                { } when macronutrientPercentage == 0 => 0,
                _ => kkalForDay * (macronutrientPercentage / 100) / macronutrientDivider,
            };

        public double GetMacronutrientGramsForMeal(double kkalForDay, double macronutrientPercentage, double macronutrientDivider, int mealsCount)
        {
            var macronutrientGramsInDay = GetMacronutrientGramsForDay(kkalForDay, macronutrientPercentage, macronutrientDivider);

            return true switch
            {
                { } when macronutrientGramsInDay == 0 || mealsCount == 0 => 0,
                _ => macronutrientGramsInDay / mealsCount,
            };
        }

        public double GetMacronutrientGramsForPortion(double macronutrientGramsInMeal, double macronutrientPercentageInPortion, double macronutrientGramsInProduct, double per)
        {
            macronutrientGramsInProduct = GetMacronutrientGramsInProductPerDefaultGrams(macronutrientGramsInProduct, per);

            return true switch
            {
                { } when macronutrientPercentageInPortion == 0 => 0,
                _ => macronutrientGramsInMeal * DefaultPerGramsValue / macronutrientGramsInProduct * (macronutrientPercentageInPortion / 100),
            };
        }

        public double GetMacronutrientGramsInProductPerDefaultGrams(double macronutrientGrams, double per)
            => true switch
            {
                { } when macronutrientGrams == 0 => 0,
                { } when per != DefaultPerGramsValue => macronutrientGrams * DefaultPerGramsValue / per,
                _ => macronutrientGrams
            };

        public PortionGramsResult GetPortionGrams(Day day, Portion portion, Product product)
        {
            var proteinGramsInMeal = GetMacronutrientGramsForMeal(day.Kilocalories, day.ProteinPercentages, ProteinKkalDivider, day.ProteinMealCount);
            var fatGramsInMeal = GetMacronutrientGramsForMeal(day.Kilocalories, day.FatPercentages, FatKkalDivider, day.FatMealCount);
            var carbohydrateGramsInMeal = GetMacronutrientGramsForMeal(day.Kilocalories, day.CarbohydratePercentages, CarbohydrateKkalDivider, day.CarbohydrateMealCount);

            return new PortionGramsResult
            {
                Product = product,
                ProteinGrams = GetMacronutrientGramsForPortion(proteinGramsInMeal, portion.ProteinPercentages, product.Protein, product.Grams),
                FatGrams = GetMacronutrientGramsForPortion(fatGramsInMeal, portion.FatPercentages, product.Fat, product.Grams),
                CarbohydrateGrams = GetMacronutrientGramsForPortion(carbohydrateGramsInMeal, portion.CarbohydratePercentages, product.Carbohydrate, product.Grams),
            };
        }

        public PortionGramsResult GetPortionGrams(Portion portion, Product product, double proteinGramsInMeal, double fatGramsInMeal, double carbohydrateGramsInMeal)
            => new PortionGramsResult
            {
                Product = product,
                ProteinGrams = GetMacronutrientGramsForPortion(proteinGramsInMeal, portion.ProteinPercentages, product.Protein, product.Grams),
                FatGrams = GetMacronutrientGramsForPortion(fatGramsInMeal, portion.FatPercentages, product.Fat, product.Grams),
                CarbohydrateGrams = GetMacronutrientGramsForPortion(carbohydrateGramsInMeal, portion.CarbohydratePercentages, product.Carbohydrate, product.Grams),
            };

        public MealPortionsGramsResult GetMealPortionsGrams(Day day, List<PortionWithProductInfo> portionsWithProductInfo)
        {
            var proteinGramsInMeal = GetMacronutrientGramsForMeal(day.Kilocalories, day.ProteinPercentages, ProteinKkalDivider, day.ProteinMealCount);
            var fatGramsInMeal = GetMacronutrientGramsForMeal(day.Kilocalories, day.FatPercentages, FatKkalDivider, day.FatMealCount);
            var carbohydrateGramsInMeal = GetMacronutrientGramsForMeal(day.Kilocalories, day.CarbohydratePercentages, CarbohydrateKkalDivider, day.CarbohydrateMealCount);

            return new MealPortionsGramsResult
            {
                PortionGramsResults = portionsWithProductInfo
                    .Select(portionWithProductInfo => GetPortionGrams(portionWithProductInfo.Portion,
                                                                      portionWithProductInfo.Product,
                                                                      proteinGramsInMeal,
                                                                      fatGramsInMeal,
                                                                      carbohydrateGramsInMeal))
                    .ToList(),
            };
        }
    }
}
