using Microsoft.AspNetCore.Components.Authorization;

namespace Client.Core.Entities.Viewer.Models.Store.Actions
{
    internal record InitializeViewerAction : BaseAction
    {
        public required Task<AuthenticationState> AuthenticationStateTask { get; init; }
    }
    internal record InitializeViewerFailureAction : BaseFailureAction;
    internal record InitializeViewerSuccessAction : BaseSuccessAction
    {
        public required ViewerModel? Viewer { get; set; }
    }
}
