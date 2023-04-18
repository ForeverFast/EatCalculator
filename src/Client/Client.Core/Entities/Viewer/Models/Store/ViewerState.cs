namespace Client.Core.Entities.Viewer.Models.Store
{
    internal sealed record ViewerState
    {
        public ViewerModel? Viewer { get; init; }
    }
}
