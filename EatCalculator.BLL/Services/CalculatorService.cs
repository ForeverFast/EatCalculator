using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EatCalculator.BLL.Services
{
    internal class CalculatorService
    {
        public Task<CalculateModel> Calculate(double calories, double proteinPercentages, double fatPercentages, double carbohydratePercentages)
            => Task.FromResult(new CalculateModel
            {
                Protein = calories * (proteinPercentages / 100) / 4,
                Fat = calories * (fatPercentages / 100) / 9,
                Carbohydrate = calories * (carbohydratePercentages / 100) / 4,
            });
    }

    public record CalculateModel
    {
        public required double Protein { get; init; }
        public required double Fat { get; init; }
        public required double Carbohydrate { get; init; }
    } 
}
