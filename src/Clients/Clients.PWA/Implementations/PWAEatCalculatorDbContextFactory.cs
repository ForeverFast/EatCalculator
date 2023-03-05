using EatCalculator.UI.Shared.Api.LocalDatabase.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace Clients.PWA.Implementations
{
    public class PWAEatCalculatorDbContextFactory : IEatCalculatorDbContextFactory
    {
        #region Injects

        private readonly EatCalculatorDbContextSettings _eatCalculatorDbContextSettings;
        private readonly IDbContextFactory<EatCalculatorDbContext> _eatCalculatorDbContextFactory;
        private readonly IJSRuntime _jSRuntime;

        #endregion

        #region Ctors

        public PWAEatCalculatorDbContextFactory(
            IOptions<EatCalculatorDbContextSettings> eatCalculatorDbContextSettings,
            IDbContextFactory<EatCalculatorDbContext> eatCalculatorDbContextFactory,
            IJSRuntime jSRuntime)
        {
            _eatCalculatorDbContextSettings = eatCalculatorDbContextSettings.Value;
            _eatCalculatorDbContextFactory = eatCalculatorDbContextFactory;
            _jSRuntime = jSRuntime;

            _dbFilename = $"{_eatCalculatorDbContextSettings.DbName}.sqlite3";
            _backup = $"{_dbFilename}_bak";
            _backupName = _backup;

            _lastTask = SynchronizeAsync();
        }

        #endregion

        #region Fields

        private string _dbFilename = "eatcalculator.sqlite3";
        private string _backup;
        private string _backupName;

        private Task<int>? _lastTask = null;
        private int _lastStatus = -2;
        private bool _init = false;

        #endregion

        public async Task<EatCalculatorDbContext> CreateContextAsync()
        {
            await CheckForPendingTasksAsync();
            var ctx = await _eatCalculatorDbContextFactory.CreateDbContextAsync();

            if (!_init)
            {
                Console.WriteLine($"Last status: {_lastStatus}");
                await ctx.Database.EnsureCreatedAsync();
                _init = true;
            }

            ctx.SavedChanges += OnSavedChanges;

            return ctx;
        }

        private async Task CheckForPendingTasksAsync()
        {
            if (_lastTask != null)
            {
                _lastStatus = await _lastTask;
                _lastTask.Dispose();
                _lastTask = null;
                if (_lastStatus == 0)
                {
                    Restore();
                }
            }
        }

        private void OnSavedChanges(object? sender, SavedChangesEventArgs e)
            => _lastTask = SynchronizeAsync();

        private async Task<int> SynchronizeAsync()
        {
            if (_init)
            {
                Backup();
            }

            var result = await _jSRuntime.InvokeAsync<int>(
                "db.synchronizeDbWithCache", _backupName);
            var resultText = result == -1 ? "Failure" : (result == 0 ? "Restored" : "Cached");
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

            var source = restore ? $"Data Source={_backupName}" : $"Data Source={_dbFilename}";
            var target = restore ? $"Data Source={_dbFilename}" : $"Data Source={_backupName}";
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
