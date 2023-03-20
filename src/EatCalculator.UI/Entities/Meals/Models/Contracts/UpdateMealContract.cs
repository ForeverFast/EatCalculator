using EatCalculator.UI.Shared.Api.Models;

namespace EatCalculator.UI.Entities.Meals.Models.Contracts
{
    internal sealed record UpdateMealContract
    {
        public required string Title { get; init; }
        public int Order { get; init; }

        public List<Portion> Portions { get; init; } = new(); 
    }
}
