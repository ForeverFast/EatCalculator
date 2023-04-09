using Client.Core.Shared.Api.LocalDatabase.Context;

namespace Client.EntryPoints.Maui.Implementations
{
    internal sealed class MauiClientEatCalculatorDbContextPathResolver : IClientEatCalculatorDbContextPathResolver
    {
        public string GetDbFilePath(string baseDbFilePath)
            => Path.Combine(FileSystem.AppDataDirectory, baseDbFilePath);
    }
}
