using Client.Core.Shared.Api.LocalDatabase.Context;
using Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace Client.EntryPoints.Pwa.Implementations
{
    public class PwaClientEatCalculatorDbContextFileProvider : IClientEatCalculatorDbContextFileProvider
    {
        #region Injects

        private readonly DalQcWrapperEventManager _dalQcWrapperEventManager;
        private readonly IJSRuntime _jSRuntime;

        #endregion

        #region Ctors

        public PwaClientEatCalculatorDbContextFileProvider(
            DalQcWrapperEventManager dalQcWrapperEventManager,
            IJSRuntime jSRuntime)
        {
            _dalQcWrapperEventManager = dalQcWrapperEventManager;
            _jSRuntime = jSRuntime;

            _dalQcWrapperEventManager.DbCreated += OnDbCreated;
            _dalQcWrapperEventManager.DbUpdated += OnDbUpdated;
            _dalQcWrapperEventManager.DbDisposed += OnDbDisposed;

            
        }

        private async Task OnDbCreated(DbCreatedEventArgs args)
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

            await _jSRuntime.InvokeAsync<int>("db.dispose", _backupName);
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

        public async Task<byte[]> GetDbFileAsync()
        {
            await _jSRuntime.InvokeAsync<int>("db.dispose", _backupName);
            
        }

        public string GetDbFilePath(string mainPath)
            => mainPath;

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
