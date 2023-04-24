using Client.Core.Shared.Api.LocalDatabase.Context;
using Microsoft.JSInterop;

namespace Client.EntryPoints.Pwa.Implementations
{
    public class PwaClientEatCalculatorDbContextFileProvider : IClientEatCalculatorDbContextFileProvider
    {
        #region Injects

        private readonly IJSRuntime _jSRuntime;

        #endregion

        #region Ctors

        public PwaClientEatCalculatorDbContextFileProvider(IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        #endregion

        public ValueTask<byte[]> GetDbFileAsync(string _)
            => _jSRuntime.InvokeAsync<byte[]>("db.getCachedFile");

        public string GetDbFilePath(string mainPath)
            => mainPath;
    }
}
