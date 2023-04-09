namespace Client.Core.Shared.Api.LocalDatabase.Context
{
    public interface IClientEatCalculatorDbContextFactory
    {
        //EatCalculatorDbContext CreateContext();
        Task<ClientEatCalculatorDbContext> CreateContextAsync();
    }
}
