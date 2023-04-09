namespace UI.Lib.Fluxor
{
    public abstract record BaseAction;
    public abstract record BaseFailureAction : BaseAction
    {
        public required string ErrorMessage { get; init; }
    }
    public abstract record BaseSuccessAction : BaseAction;
}
