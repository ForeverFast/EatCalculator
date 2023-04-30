using DALQueryChain.Interfaces;
using MediatR;

namespace Client.Core.Shared.Api.LocalDatabase.Context
{
    public delegate Task DbInitializedEventHandler(string path);
    public delegate Task DbUpdatedEventHandler();
    public delegate Task DbDisposedEventHandler();
    public delegate void DbActivatedEventHandler();

    public delegate Task CallToReplace(byte[] fileData);

    public enum DalQcState
    {
        Disabled,
        Initialized,
        Active,
        Disposing
    }

    public record DbInitializedNotification : INotification
    {
        public required string PathToFile { get; init; }
    }
    public record DbActivatedNotification : INotification;
    public record DbUpdatedNotification : INotification;
    public record DbDisposedNotification : INotification;
    public record ChangeDbFileDataRequest : IRequest
    {
        public required byte[] FileData { get; init; }
    }

    public interface IDalQcWrapper : IAsyncDisposable
    {
        IDALQueryChain<ClientEatCalculatorDbContext> Instance { get; }

        DalQcState State { get; }
    }
}
