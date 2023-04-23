using Client.Core.Entities.Days.Models.Store.Actions;
using Client.Core.Shared.Resources.Localizations;
using DALQueryChain.EntityFramework.Extensions;
using Microsoft.Extensions.Logging;

namespace Client.Core.Entities.Days.Models.Store.Effects
{
    internal sealed class LoadDaysEffect : BaseEffect<LoadDaysAction>
    {
        #region Ctors

        public LoadDaysEffect(BaseEffectInjects injects,
                              ILogger<BaseEffect<LoadDaysAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(LoadDaysAction action, IDispatcher dispatcher)
        {
            try
            {
                var days = await _injects.Dal.For<Day>()
                    .Get
                    .LoadWith(x => x.DayDateBinds)
                    .ToListAsync();

                dispatcher.Dispatch(new LoadDaysSuccessAction
                {
                    Days = days,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new LoadDaysFailureAction
                {
                    Messages = new List<string>
                    {
                        _injects.Localizer[nameof(DefaultLocalization.UnhandledException)]
                    },
                });
            }
        }
    }
}
