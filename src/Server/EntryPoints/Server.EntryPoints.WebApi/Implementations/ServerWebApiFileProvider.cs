using Common;
using Server.Core.Interfaces;
using System.Text;

namespace Server.EntryPoints.WebApi.Implementations
{
    public class ServerWebApiFileProvider : IFileProvider
    {
        #region Fields

        private const string s_basePath = "user-files";

        #endregion

        public async ValueTask<byte[]> GetFileAsync(string path, CancellationToken ctn = default)
        {
            if (!File.Exists(path))
                return Array.Empty<byte>();

            using var memStream = new MemoryStream();
            using var fstream = File.OpenRead(path);
            await fstream.CopyToAsync(memStream);

            return memStream.ToArray();
        }

        public async ValueTask<string> CreateFileAsync(byte[] fileByteArray,
                                                       int userId,
                                                       string fileName = GlobalConstants.LocalUserDbFileName,
                                                       bool dbFile = true,
                                                       CancellationToken ctn = default)
        {
            if (fileByteArray.Length == 0)
                return string.Empty;

            string resultFileName = dbFile ? fileName : GetUniqueFileTitle(fileName);
            string folderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, s_basePath, userId.ToString());
            if (!dbFile)
                folderPath = Path.Combine(folderPath, Guid.NewGuid().ToString());

            if (!Directory.Exists(folderPath))
                Directory.CreateDirectory(folderPath);
            string filePath = Path.Combine(folderPath, resultFileName);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await fileStream.WriteAsync(fileByteArray, ctn);

            return filePath;
        }

        public async ValueTask UpdateFile(byte[] fileByteArray,
                                     string filePath,
                                     CancellationToken ctn = default)
        {
            await DeleteFile(filePath, false, ctn);

            using var fileStream = new FileStream(filePath, FileMode.Create);
            await fileStream.WriteAsync(fileByteArray, ctn);
        }

        public ValueTask DeleteFile(string filePath, bool deleteFolderIfEmpty = true, CancellationToken ctn = default)
        {
            if (File.Exists(filePath))
            {
                var folderPath = Path.GetDirectoryName(filePath);

                File.Delete(filePath);

                if (!Directory.EnumerateFileSystemEntries(folderPath!).Any() && deleteFolderIfEmpty)
                    Directory.Delete(folderPath!);
            }

            return ValueTask.CompletedTask;
        }

        #region Private methods

        private static string GetUniqueFileTitle(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString()[..4]
                      + Path.GetExtension(fileName);
        }

        #endregion
    }
}
