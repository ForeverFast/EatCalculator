using Client.Core.Shared.Api.LocalDatabase.Context;
using Common;
using Microsoft.Data.Sqlite;
using Microsoft.JSInterop;

namespace Client.EntryPoints.Pwa.Implementations
{
    public class PwaClientEatCalculatorDbContextCacheSynchronizer
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
            await CheckForPendingTasksAsync();

            _dbFilename = $"{args.Path}";
            _backup = $"{_dbFilename}_bak";
            _backupName = _backup;

            Console.WriteLine($"Last status: {_lastStatus}");

            _lastTask = SynchronizeAsync();
        }

        private async Task OnDbUpdated()
        {
            await CheckForPendingTasksAsync();

            _lastTask = SynchronizeAsync();
        }

        private async Task OnDbDisposed()
        {
            await CheckForPendingTasksAsync();

            await _jSRuntime.InvokeAsync<int>("db.restoreJsState");
        }

        #endregion

        #region Fields

        private string _dbFilename = GlobalConstants.LocalUserDbFileName;
        private string _backup;
        private string _backupName;

        private Task<int>? _lastTask = null;
        private int _lastStatus = -2;
        private bool _init = false;

        #endregion

        

        private async ValueTask CheckForPendingTasksAsync()
        {
            if (_lastTask == null)
                return;

            _lastStatus = await _lastTask;
            _lastTask.Dispose();
            _lastTask = null;

            if (_lastStatus != 0)
                return;

            Restore();
        }

        private async Task<int> SynchronizeAsync()
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

            return result;
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
    }
}
