using Client.Core.Shared.Api.LocalDatabase.Context;

namespace Client.EntryPoints.Maui.Implementations
{
    internal sealed class MauiClientEatCalculatorDbContextFileProvider : IClientEatCalculatorDbContextFileProvider
    {
        public async ValueTask<byte[]> GetDbFileAsync(string mainPath)
            => await File.ReadAllBytesAsync(GetDbFilePath(mainPath));
        
        public string GetDbFilePath(string mainPath)
            => Path.Combine(FileSystem.AppDataDirectory, mainPath);
    }
}
