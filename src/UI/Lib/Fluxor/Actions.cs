namespace UI.Lib.Fluxor
{
    public abstract record BaseAction;
    public abstract record BaseFailureAction : BaseAction
    {
        public required List<string> Messages { get; init; }
    }
    public abstract record BaseSuccessAction : BaseAction;
}
