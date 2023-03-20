using EatCalculator.UI.Shared.Api.LocalDatabase.Context;

namespace Clients.Maui.Implementations
{
    internal sealed class MauiEatCalculatorDbContextPathResolver : IEatCalculatorDbContextPathResolver
    {
        public string GetDbFilePath(string baseDbFilePath)
            => Path.Combine(FileSystem.AppDataDirectory, baseDbFilePath);
    }
}
