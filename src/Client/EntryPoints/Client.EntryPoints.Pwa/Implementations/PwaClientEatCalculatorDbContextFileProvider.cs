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

        public ValueTask<byte[]> GetDbFileAsync(string mainPath)
            => _jSRuntime.InvokeAsync<byte[]>("db.getCachedFile", GetDbFilePath(mainPath));

        public string GetDbFilePath(string mainPath)
        {
            var resultPath = $"{mainPath}";
            var dir = Path.GetDirectoryName(resultPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir!);

            return resultPath;
        }
    }
}
