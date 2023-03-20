using EatCalculator.UI.Shared.Api.LocalDatabase.Context;

namespace Clients.PWA.Implementations
{
    internal class PwaEatCalculatorDbContextPathResolver : IEatCalculatorDbContextPathResolver
    {
        public string GetDbFilePath(string baseDbFilePath)
            => baseDbFilePath;
    }
}
