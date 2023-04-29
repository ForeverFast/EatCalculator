using Client.Core.Shared.Api.LocalDatabase.Context;
using Microsoft.Data.Sqlite;

namespace Client.EntryPoints.Maui.Implementations
{
    internal sealed class MauiClientEatCalculatorDbContextFileProvider : IClientEatCalculatorDbContextFileProvider
    {
        public async ValueTask<byte[]> GetDbFileAsync(string mainPath)
        {
            var filePath = GetDbFilePath(mainPath);
            var fileDirPath = Path.GetDirectoryName(filePath)!;
            var tmpFile = Path.Combine(fileDirPath, "tmp-eat-calc.db");

            await Backup(filePath, tmpFile);

            return await File.ReadAllBytesAsync(tmpFile);
        }

        async Task Backup(string srcFile, string tgtFile)
        {
            var source = $"Data Source={srcFile}";
            var target = $"Data Source={tgtFile}";
            var src = new SqliteConnection(source);
            var tgt = new SqliteConnection(target);

            await src.OpenAsync();
            await tgt.OpenAsync();

            src.BackupDatabase(tgt);

            await src.CloseAsync();
            await tgt.CloseAsync();

            await src.DisposeAsync();
            await tgt.DisposeAsync();

            SqliteConnection.ClearPool(tgt);
            SqliteConnection.ClearPool(src);
        }

        public string GetDbFilePath(string mainPath)
        {
            var resultPath = Path.Combine(FileSystem.AppDataDirectory, mainPath);
            var dir = Path.GetDirectoryName(resultPath);
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir!);

            return resultPath;
        }
    }
}
