using Client.Core.Shared.Api.LocalDatabase.Context;
using DALQueryChain.Interfaces;

namespace Client.Core.Shared.Api.LocalDatabase.DalQc
{
    public enum DalQcState
    {
        Disabled,
        Initialized,
        Active,
        Disposing
    }

    public interface IDalQcWrapper : IDisposable
    {
        IDALQueryChain<ClientEatCalculatorDbContext> Instance { get; }

        DalQcState State { get; }
    }
}
