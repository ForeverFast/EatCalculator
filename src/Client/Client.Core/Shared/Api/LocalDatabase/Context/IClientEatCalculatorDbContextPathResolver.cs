namespace Client.Core.Shared.Api.LocalDatabase.Context
{
    public interface IClientEatCalculatorDbContextPathResolver
    {
        string GetDbFilePath(string baseDbFilePath);
    }
}
