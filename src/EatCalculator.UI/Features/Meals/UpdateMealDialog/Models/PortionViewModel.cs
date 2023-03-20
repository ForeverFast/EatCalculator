namespace EatCalculator.UI.Features.Meals.UpdateMealDialog.Models
{
    internal class PortionViewModel
    {
        public required int ProductId { get; init; }
    
        public double ProteinPercentages { get; set; }
        public double FatPercentages { get; set; }
        public double CarbohydratePercentages { get; set; }
    }
}
