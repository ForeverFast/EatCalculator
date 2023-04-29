using DALQueryChain.Interfaces;

namespace Client.Core.Shared.Api.LocalDatabase.Context
{
    public delegate Task DbInitializedEventHandler(DbInitializedEventArgs args);
    public delegate Task DbUpdatedEventHandler();
    public delegate Task DbDisposedEventHandler();
    public delegate void DbActivatedEventHandler();

    public record DbInitializedEventArgs
    {
        public required string Path { get; init; }
    }

    public enum DalQcState
    {
        Disabled,
        Initialized,
        Active,
        Disposing
    }

    public interface IDalQcWrapper : IAsyncDisposable
    {
        IDALQueryChain<ClientEatCalculatorDbContext> Instance { get; }

        public DalQcState State { get; }

        event DbInitializedEventHandler? DbInitialized;
        event DbUpdatedEventHandler? DbUpdated;
        event DbDisposedEventHandler? DbDisposed;
        event DbActivatedEventHandler? DbActivated;

        void TriggerDbActivatedEvent();
    }
}
