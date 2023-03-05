namespace EatCalculator.UI.Shared.Api.LocalDatabase.Context
{
    public interface IEatCalculatorDbContextFactory
    {
        //EatCalculatorDbContext CreateContext();
        Task<EatCalculatorDbContext> CreateContextAsync();
    }
}
