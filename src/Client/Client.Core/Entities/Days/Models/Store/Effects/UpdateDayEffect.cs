﻿using Client.Core.Entities.Days.Models.Store.Actions;
using Client.Core.Shared.Resources.Localizations;
using Microsoft.Extensions.Logging;

namespace Client.Core.Entities.Days.Models.Store.Effects
{
    internal sealed class UpdateDayEffect : BaseEffect<UpdateDayAction>
    {
        #region Ctors

        public UpdateDayEffect(BaseEffectInjects injects,
                               ILogger<BaseEffect<UpdateDayAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override async Task HandleAsync(UpdateDayAction action, IDispatcher dispatcher)
        {
            try
            {
                var targetDay = await _injects.Dal.Instance.For<Day>().Get
                    .FirstOrDefaultAsync(x => x.Id == action.Id)
                    ?? throw new Exception();

                targetDay = targetDay with
                {
                    Title = action.Day.Title,
                    Description = action.Day.Description,
                    Kilocalories = action.Day.Kilocalories,
                    ProteinPercentages = action.Day.ProteinPercentages,
                    FatPercentages = action.Day.FatPercentages,
                    CarbohydratePercentages = action.Day.CarbohydratePercentages,
                    ProteinMealCount = action.Day.ProteinMealCount,
                    FatMealCount = action.Day.FatMealCount,
                    CarbohydrateMealCount = action.Day.CarbohydrateMealCount,
                };

                await _injects.Dal.Instance.For<Day>().Update.UpdateAsync(targetDay);

                dispatcher.Dispatch(new UpdateDaySuccessAction
                {
                    Day = targetDay,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "");
                dispatcher.Dispatch(new UpdateDayFailureAction
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
