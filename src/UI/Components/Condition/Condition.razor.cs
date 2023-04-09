namespace UI
{
    public partial class Condition : ComponentBase
    {
        #region Params

        [Parameter] public bool Evaluation { get; set; }
        [Parameter] public RenderFragment? Match { get; set; }
        [Parameter] public RenderFragment? NotMatch { get; set; }

        #endregion
    }
}
