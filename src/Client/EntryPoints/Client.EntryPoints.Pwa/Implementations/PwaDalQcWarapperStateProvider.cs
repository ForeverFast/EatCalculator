using Client.Core.Shared.Api.LocalDatabase.DalQc;
using Common;
using Microsoft.Data.Sqlite;
using Microsoft.JSInterop;

namespace Client.EntryPoints.Pwa.Implementations
{
    public class PwaDalQcWarapperStateProvider : IDisposable
    {
        #region Injects

        private readonly IJSRuntime _jSRuntime;

        #endregion

        #region Ctors

        public PwaDalQcWarapperStateProvider(
            IJSRuntime jSRuntime)
        {
            _jSRuntime = jSRuntime;
        }

        #endregion

        #region Fields

        private string _dbFilename = GlobalConstants.LocalUserDbFileName;
        private string _backup;
        private string _backupName;

        private int _lastStatus = -2;
        private bool _init = false;
        private SemaphoreSlim _semaphore = new(1, 1);

        #endregion

        public async Task Handle(DbInitializedNotification notification, CancellationToken ctn)
        {
            await _semaphore.WaitAsync(ctn);

            _dbFilename = Path.Combine(notification.UserId.ToString(), notification.DbFileName);
            _backup = $"{_dbFilename}_bak";
            _backupName = _backup;

            Console.WriteLine($"Last status: {_lastStatus}");

            await SynchronizeAsync();
        }

        public async Task Handle(DbUpdatedNotification _, CancellationToken ctn)
        {
            if (!_init)
                return;

            await _semaphore.WaitAsync(ctn);
            await SynchronizeAsync();
        }

        public async Task Handle(DbDisposedNotification _, CancellationToken ctn)
        {
            if (!_init)
                return;

            await _semaphore.WaitAsync(ctn);

            await _jSRuntime.InvokeVoidAsync("db.restoreJsState", ctn);

            _init = false;

            _semaphore.Release();
        }

        public async Task Handle(ChangeDbFileDataRequest request, CancellationToken ctn)
        {
            if (!_init)
                return;

            await _semaphore.WaitAsync(ctn);

            var tmpFilePath = "tmp-eat-calc.db";
            await File.WriteAllBytesAsync(tmpFilePath, request.FileData);
            await BackupDatabaseAsync(tmpFilePath, _dbFilename);
            await SynchronizeAsync();
        }

        private async Task SynchronizeAsync()
        {
            try
            {
                if (_init)
                    await Backup();

                var result = await _jSRuntime.InvokeAsync<int>("db.synchronizeDbWithCache", _backupName);
                var resultText = result switch
                {
                    1 => "Cached",
                    0 => "Restored",
                    -1 or _ => "Failure",
                };
                Console.WriteLine($"Synchronization status: {resultText}");

                if (result == -1)
                {
                    _init = true;
                }

                if (result == 0)
                {
                     await Restore();
                    _init = true;
                }
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private async Task Backup()
        {
            _backupName = $"{_backup}-{Guid.NewGuid().ToString().Split('-')[0]}";

            Console.WriteLine("Backup start...");
            await BackupDatabaseAsync(_dbFilename, _backupName);
            Console.WriteLine("Backup end");
        }

        private async Task Restore()
        {
            Console.WriteLine("Restore start...");
            await BackupDatabaseAsync(_backupName, _dbFilename);
            Console.WriteLine("Restore end");
        }

        public async Task BackupDatabaseAsync(string sourceDbFilePath, string targetDbFilePath)
        {
            var source = $"Data Source={sourceDbFilePath}";
            var target = $"Data Source={targetDbFilePath}";
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

        public void Dispose()
        {
            _semaphore.Dispose();
        }

       
    }
}


/*
 private void DoSwap(bool restore)
        {
            _backupName = restore ? _backup : $"{_backup}-{Guid.NewGuid().ToString().Split('-')[0]}";
            var dir = restore ? nameof(restore) : "backup";

            Console.WriteLine($"Begin {dir}.");

            var source = $"Data Source={(restore ? _backupName : _dbFilename)}";
            var target = $"Data Source={(restore ? _dbFilename : _backupName)}";
            using var src = new SqliteConnection(source);
            using var tgt = new SqliteConnection(target);

            src.Open();
            tgt.Open();

            src.BackupDatabase(tgt);

            tgt.Close();
            src.Close();

            Console.WriteLine($"End {dir}.");
        }
 */