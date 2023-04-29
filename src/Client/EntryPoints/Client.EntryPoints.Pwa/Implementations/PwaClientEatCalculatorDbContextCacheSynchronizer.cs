using Client.Core.Shared.Api.LocalDatabase.Context;
using Common;
using Microsoft.Data.Sqlite;
using Microsoft.JSInterop;
using System.Threading.Channels;

namespace Client.EntryPoints.Pwa.Implementations
{
    public class PwaClientEatCalculatorDbContextCacheSynchronizer : IDisposable
    {
        #region Injects

        private readonly IJSRuntime _jSRuntime;
        private readonly IDalQcWrapper _dalQcWrapper;

        #endregion

        #region Ctors

        public PwaClientEatCalculatorDbContextCacheSynchronizer(
            IJSRuntime jSRuntime,
            IDalQcWrapper dalQcWrapper)
        {
            _jSRuntime = jSRuntime;
            _dalQcWrapper = dalQcWrapper;

            _dalQcWrapper.DbInitialized += OnDbInitialized;
            _dalQcWrapper.DbUpdated += OnDbUpdated;
            _dalQcWrapper.DbDisposed += OnDbDisposed;
        }
        
        private async Task OnDbInitialized(DbInitializedEventArgs args)
        {
            await _semaphore.WaitAsync();
 
            _dbFilename = $"{args.Path}";
            _backup = $"{_dbFilename}_bak";
            _backupName = _backup;

            Console.WriteLine($"Last status: {_lastStatus}");

            await SynchronizeAsync().ConfigureAwait(false);
        }

        private async Task OnDbUpdated()
        {
            if (!_init)
                return;

            await _semaphore.WaitAsync();
            await SynchronizeAsync();
        }

        private async Task OnDbDisposed()
        {
            if (!_init)
                return;

            await _semaphore.WaitAsync();

            await _jSRuntime.InvokeVoidAsync("db.restoreJsState");

            _init = false;

            _semaphore.Release();
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

        private async Task<int> SynchronizeAsync()
        {
            try
            {
                if (_init)
                    Backup();

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
                    _dalQcWrapper.TriggerDbActivatedEvent();
                }

                if (result == 0)
                {
                    Restore();
                    _init = true;
                    _dalQcWrapper.TriggerDbActivatedEvent();
                }

                return result;
            }
            finally
            {
                _semaphore.Release();
            }
        }

        private void Backup()
            => DoSwap(false);

        private void Restore()
            => DoSwap(true);

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

        public void Dispose()
        {
            _semaphore.Dispose();
        }

        public record Command;
    }
}
