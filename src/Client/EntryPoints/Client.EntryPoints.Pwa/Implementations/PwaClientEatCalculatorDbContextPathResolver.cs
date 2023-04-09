using Client.Core.Shared.Api.LocalDatabase.Context;

namespace Client.EntryPoints.Pwa.Implementations
{
    internal class PwaClientEatCalculatorDbContextPathResolver : IClientEatCalculatorDbContextPathResolver
    {
        public string GetDbFilePath(string baseDbFilePath)
            => baseDbFilePath;
    }
}
