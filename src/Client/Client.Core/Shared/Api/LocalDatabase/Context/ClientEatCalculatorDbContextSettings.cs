namespace Client.Core.Shared.Api.LocalDatabase.Context
{
    public record ClientEatCalculatorDbContextSettings
    {
        public required string DbName { get; init; }
    }
}
