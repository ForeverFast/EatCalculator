using DALQueryChain.Interfaces;

namespace Client.Core.Shared.Api.LocalDatabase.Context
{
    public delegate Task DbCreatedEventHandler(DbCreatedEventArgs args);
    public delegate Task DbUpdatedEventHandler();
    public delegate Task DbDisposedEventHandler();

    public record DbCreatedEventArgs
    {
        public required string Path { get; init; }
    }

    public interface IDalQcWrapper : IAsyncDisposable
    {
        IDALQueryChain<ClientEatCalculatorDbContext> Instance { get; }

        event DbCreatedEventHandler? DbCreated;
        event DbUpdatedEventHandler? DbUpdated;
        event DbDisposedEventHandler? DbDisposed;
    }
}
