using Common;

namespace Server.Core.Interfaces
{
    public interface IFileProvider
    {
        ValueTask<byte[]> GetFileAsync(string path, CancellationToken ctn = default);

        ValueTask<string> CreateFileAsync(byte[] fileByteArray,
                                          int userId,
                                          string fileName = GlobalConstants.LocalUserDbFileName,
                                          bool dbFile = true,
                                          CancellationToken ctn = default);

        ValueTask UpdateFile(byte[] fileByteArray, string filePath, CancellationToken ctn = default);

        ValueTask DeleteFile(string filePath, bool deleteFolderIfEmpty = true, CancellationToken ctn = default);
    }
}
