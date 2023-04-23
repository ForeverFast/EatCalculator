namespace Client.Core.Entities.Viewer.Models.Store
{
    public sealed record ViewerState
    {
        public ViewerModel? Viewer { get; init; }
    }
}
