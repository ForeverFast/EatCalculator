using Client.Core.Entities.Viewer.Models.Store;
using Client.Core.Shared.Api.LocalDatabase.Context;
using Fluxor;
using Microsoft.Extensions.Options;

namespace Client.EntryPoints.Maui.Implementations
{
    internal sealed class MauiClientEatCalculatorDbContextPathHelper : IClientEatCalculatorDbContextPathHelper
    {
        public string GetDbFilePath(string mainPath)
            => Path.Combine(FileSystem.AppDataDirectory, mainPath);
    }
}
