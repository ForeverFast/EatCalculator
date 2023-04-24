namespace Client.Core.Shared.Api.LocalDatabase.Context
{
    public interface IClientEatCalculatorDbContextFileProvider
    {
        string GetDbFilePath(string mainPath);
        ValueTask<byte[]> GetDbFileAsync(string mainPath);
    }
}
