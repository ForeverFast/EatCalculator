using Client.Core.Entities.Viewer.Models.Store.Actions;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client.Core.Entities.Viewer.Models.Store.Effects
{
    internal sealed class SynchronizeEatDataEffect : BaseEffect<SynchronizeEatDataAction>
    {
        #region Ctors

        public SynchronizeEatDataEffect(BaseEffectInjects injects, ILogger<BaseEffect<SynchronizeEatDataAction>> logger) : base(injects, logger)
        {
        }

        #endregion

        public override Task HandleAsync(SynchronizeEatDataAction action, IDispatcher dispatcher)
        {
            return Task.CompletedTask;
        }
    }
}
