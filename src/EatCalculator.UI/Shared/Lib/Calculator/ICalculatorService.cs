using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Lib.Calculator.Models;

namespace EatCalculator.UI.Shared.Lib.Calculator
{
    public interface ICalculatorService
    {
        double KkalForDay { get; }

        double ProteinKkalDivider { get; }
        double FatKkalDivider { get; }
        double CarbohydrateKkalDivider { get; }

        double DefaultProteinPercentagesForDay { get; }
        double DefaultFatPercentagesForDay { get; }
        double DefaultCarbohydratePercentagesForDay { get; }

        int DefaultMealCountForDay { get; }

        double GetMacronutrientGramsForDay(double kkalForDay, double macronutrientPercentage, double macronutrientDivider);
        double GetMacronutrientGramsForMeal(double kkalForDay, double macronutrientPercentage, double macronutrientDivider, int mealsCount);
        double GetMacronutrientGramsForPortion(double macronutrientGramsInMeal, double macronutrientPercentageInPortion, double macronutrientGramsInProduct, double per);
        double GetMacronutrientGramsInProductPerDefaultGrams(double macronutrientGrams, double per);

        PortionGramsResult GetPortionGrams(Day day, Portion portion, Product product);
        PortionGramsResult GetPortionGrams(Portion portion, Product product, double proteinGramsInMeal, double fatGramsInMeal, double carbohydrateGramsInMeal);
        MealPortionsGramsResult GetMealPortionsGrams(Day day, List<PortionWithProductInfo> portionsWithProductInfo);
    }
}
