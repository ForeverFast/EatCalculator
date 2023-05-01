

namespace Client.Core.Shared.Api.LocalDatabase.DalQc
{
    public record DbInitializedNotification : INotification
    {
        public required int UserId { get; init; }
        public required string DbFileName { get; init; }
    }
    public record DbActivatedNotification : INotification;
    public record DbUpdatedNotification : INotification;
    public record DbDisposedNotification : INotification;
}
