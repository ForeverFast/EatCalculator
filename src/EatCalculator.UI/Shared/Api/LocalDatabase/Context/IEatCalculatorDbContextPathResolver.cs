namespace EatCalculator.UI.Shared.Api.LocalDatabase.Context
{
    public interface IEatCalculatorDbContextPathResolver
    {
        string GetDbFilePath(string baseDbFilePath);
    }
}
