using EatCalculator.UI.Shared.Api.LocalDatabase.Context;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.JSInterop;

namespace Clients.PWA.Implementations
{
    public class PwaEatCalculatorDbContextFactory : IEatCalculatorDbContextFactory
    {
        #region Injects

        private readonly EatCalculatorDbContextSettings _eatCalculatorDbContextSettings;
        private readonly IDbContextFactory<EatCalculatorDbContext> _eatCalculatorDbContextFactory;
        private readonly IJSRuntime _jSRuntime;

        #endregion

        #region Ctors

        public PwaEatCalculatorDbContextFactory(
            IOptions<EatCalculatorDbContextSettings> eatCalculatorDbContextSettings,
            IDbContextFactory<EatCalculatorDbContext> eatCalculatorDbContextFactory,
            IJSRuntime jSRuntime)
        {
            _eatCalculatorDbContextSettings = eatCalculatorDbContextSettings.Value;
            _eatCalculatorDbContextFactory = eatCalculatorDbContextFactory;
            _jSRuntime = jSRuntime;

            _dbFilename = $"{_eatCalculatorDbContextSettings.DbName}";
            _backup = $"{_dbFilename}_bak";
            _backupName = _backup;

            _lastTask = SynchronizeAsync();
        }

        #endregion

        #region Fields

        private string _dbFilename = "eat-calculator.db";
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

        private void OnSavedChanges(object? sender, SavedChangesEventArgs e)
            => _lastTask = SynchronizeAsync();

        private async Task<int> SynchronizeAsync()
        {
            if (_init)
            {
                Backup();
            }

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
