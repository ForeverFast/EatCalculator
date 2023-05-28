using Client.Core.Shared.Api.Models;
using Client.Core.Shared.Lib.Calculator;
using Client.Core.Shared.Lib.Calculator.Models;
using Client.Core.Tests.Data;
using FluentAssertions;

namespace Client.Core.Tests
{
    public class CalculatorServiceTests
    {
        private readonly CalculatorService _calculatorService;
        private readonly List<Product> _products;
        private readonly Day _testDay;

        public CalculatorServiceTests()
        {
            _calculatorService = new CalculatorService();
            _products = ProductData.Products;
            _testDay = new Day
            {
                Id = 0,
                Title = "TestDay",
                Kilocalories = 2500.0,
                ProteinPercentages = 30.0,
                FatPercentages = 10.0,
                CarbohydratePercentages = 60.0,
                ProteinMealCount = 6,
                FatMealCount = 5,
                CarbohydrateMealCount = 4,

                Meals = new List<Meal>
                {
                    new Meal
                    {
                        Id = 0,
                        DayId = 0,
                        Title = "TestMeal_1",
                        Portions = new List<Portion>
                        {
                            new Portion // Овсянка
                            {
                                Id= 0,
                                MealId= 0,
                                ProductId = 8,
                                ProteinPercentages = 0.0,
                                FatPercentages = 0.0,
                                CarbohydratePercentages = 100.0,
                            },
                            new Portion // Курица 
                            {
                                Id= 1,
                                MealId= 0,
                                ProductId = 3,
                                ProteinPercentages = 100.0,
                                FatPercentages = 20.0,
                                CarbohydratePercentages = 0.0,
                            },
                            new Portion // Оливковое масло
                            {
                                Id= 2,
                                MealId= 0,
                                ProductId = 10,
                                ProteinPercentages = 0.0,
                                FatPercentages = 80.0,
                                CarbohydratePercentages = 0.0,
                            }
                        }
                    }
                }
            };
        }

        [Theory]
        [InlineData(2500.0, 30.0, 4.0, 187.5)]
        [InlineData(2500.0, 10.0, 9.0, 27.78)]
        [InlineData(2500.0, 60.0, 4.0, 375.0)]
        public void Calculate_MacronutrientGramsForDay_SuccessCalculation(
            double kkalForDay,
            double macronutrientPercentage,
            double macronutrientDivider,
            double expectedMacronutrientGramsInDay
            )
        {
            // Act
            var result = _calculatorService.GetMacronutrientGramsForDay(kkalForDay, macronutrientPercentage, macronutrientDivider);

            // Assert
            Math.Round(result, 2).Should().Be(expectedMacronutrientGramsInDay);
        }

        [Theory]
        [InlineData(2500.0, 30.0, 4.0, 6, 31.25)]
        [InlineData(2500.0, 10.0, 9.0, 5, 5.56)]
        [InlineData(2500.0, 60.0, 4.0, 4, 93.75)]
        public void Calculate_MacronutrientGramsForMeal_SuccessCalculation(
            double kkalForDay,
            double macronutrientPercentage,
            double macronutrientDivider,
            int macronutrientMealCount,
            double expectedMacronutrientGramsInMeal
            )
        {
            // Act
            var result = _calculatorService.GetMacronutrientGramsForMeal(kkalForDay, macronutrientPercentage, macronutrientDivider, macronutrientMealCount);

            // Assert
            Math.Round(result, 2).Should().Be(expectedMacronutrientGramsInMeal);
        }

        [Theory]
        [InlineData(2.85, 30.0, 9.5)]
        [InlineData(0.69, 30.0, 2.3)]
        [InlineData(19.77, 30.0, 65.9)]
        public void Calculate_MacronutrientGramsInProductPerDefaultGrams_SuccessCalculation(
            double macronutrientGrams,
            double per,
            double expectedMacronutrientGramsInProduct
            )
        {
            // Act
            var result = _calculatorService.GetMacronutrientGramsInProductPerDefaultGrams(macronutrientGrams, per);

            // Assert
            Math.Round(result, 2).Should().Be(expectedMacronutrientGramsInProduct);
        }

        [Fact]
        public void Calculate_MacronutrientGramsForPortions_SuccessCalculation()
        {
            // Arrange
            var targetPortion = _testDay.Meals[0].Portions[0];
            var targetProduct = _products.First(x => x.Id == targetPortion.ProductId);

            // Act
            var result = _calculatorService.GetPortionGrams(_testDay, targetPortion, targetProduct);

            // Assert
            Math.Round(result.ProteinGrams, 2).Should().Be(0.0);
            Math.Round(result.FatGrams, 2).Should().Be(0.0);
            Math.Round(result.CarbohydrateGrams, 2).Should().Be(151.7);
        }

        [Fact]
        public void Calculate_MealPortionsGrams_SuccessCalculation()
        {
            // Arrange
            var targetMeal = _testDay.Meals[0];
            var portionsWithProductInfo = targetMeal.Portions
                .Select(portion => new PortionWithProductInfo
                {
                    Portion = portion,
                    Product = _products.First(x => x.Id == portion.ProductId)
                })
                .ToList();

            // Act
            var result = _calculatorService.GetMealPortionsGrams(_testDay, portionsWithProductInfo);

            // Assert

            // Овсянка
            Math.Round(result.PortionGramsResults[0].ProteinGrams, 2).Should().Be(0.0);
            Math.Round(result.PortionGramsResults[0].FatGrams, 2).Should().Be(0.0);
            Math.Round(result.PortionGramsResults[0].CarbohydrateGrams, 2).Should().Be(151.7);

            // Курица
            Math.Round(result.PortionGramsResults[1].ProteinGrams, 2).Should().Be(148.81);
            Math.Round(result.PortionGramsResults[1].FatGrams, 2).Should().Be(13.55);
            Math.Round(result.PortionGramsResults[1].CarbohydrateGrams, 2).Should().Be(0.0);

            // Масло
            Math.Round(result.PortionGramsResults[2].ProteinGrams, 2).Should().Be(0.0);
            Math.Round(result.PortionGramsResults[2].FatGrams, 2).Should().Be(4.45);
            Math.Round(result.PortionGramsResults[2].CarbohydrateGrams, 2).Should().Be(0.0);
        }
    }
}
