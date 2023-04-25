using Client.Core.Shared.Api.LocalDatabase.Context;

namespace Client.EntryPoints.Maui.Implementations
{
    internal sealed class MauiClientEatCalculatorDbContextFileProvider : IClientEatCalculatorDbContextFileProvider
    {
        public async ValueTask<byte[]> GetDbFileAsync(string mainPath)
            => await File.ReadAllBytesAsync(GetDbFilePath(mainPath));
        
        public string GetDbFilePath(string mainPath)
        {
            var resultPath = Path.Combine(FileSystem.AppDataDirectory,mainPath);
            var dir = Path.GetDirectoryName(resultPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir!);

            return resultPath;
        }
    }
}
