using DALQueryChain.Interfaces;

namespace Client.Core.Shared.Api.LocalDatabase.Context
{
    public delegate Task DbInitializedEventHandler(DbInitializedEventArgs args);
    public delegate Task DbUpdatedEventHandler();
    public delegate Task DbDisposedEventHandler();

    public record DbInitializedEventArgs
    {
        public required string Path { get; init; }
    }

    public interface IDalQcWrapper : IAsyncDisposable
    {
        IDALQueryChain<ClientEatCalculatorDbContext> Instance { get; }

        event DbInitializedEventHandler? DbInitialized;
        event DbUpdatedEventHandler? DbUpdated;
        event DbDisposedEventHandler? DbDisposed;
    }
}
