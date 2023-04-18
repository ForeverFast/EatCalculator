namespace Client.Core.Entities.Viewer.Models.Store.Actions
{
    internal record SignUpAction : BaseAction
    {
        public required string Login { get; init; }
        public required string Email { get; init; }
        public required string Password { get; init; }
    }
    internal record SignUpFailureAction : BaseFailureAction;
    internal record SignUpSuccessAction : BaseSuccessAction
    {
        public required ViewerModel ViewerModel { get; init; }
    }
}
