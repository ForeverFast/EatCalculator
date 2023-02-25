using EatCalculator.UI.Shared.Api.Models;
using EatCalculator.UI.Shared.Components;
using EatCalculator.UI.Shared.Lib.EntityAdapter;

namespace EatCalculator.UI.Entities.Products.Models.Store
{
    internal sealed record ProductState : EntityState<int, Product>
    {
        public LoadingState LoadingState { get; init; }
    }
}
