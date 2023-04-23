namespace Client.Core.Shared.Api.LocalDatabase.Context
{
    public interface IClientEatCalculatorDbContextFileProvider
    {
        string GetDbFilePath(string mainPath);
        Task<byte[]> GetDbFileAsync();
    }
}
