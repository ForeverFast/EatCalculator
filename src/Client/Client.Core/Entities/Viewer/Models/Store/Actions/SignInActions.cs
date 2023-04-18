namespace Client.Core.Entities.Viewer.Models.Store.Actions
{
    internal record SignInAction : BaseAction
    {
        public required string Login { get; init; }
        public required string Password { get; init; }
    }
    internal record SignInFailureAction : BaseFailureAction;
    internal record SignInSuccessAction : BaseSuccessAction
    {
        public required ViewerModel ViewerModel { get; init; }
    }
}
