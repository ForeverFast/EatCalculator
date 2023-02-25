namespace EatCalculator.UI.Shared.Lib.Fluxor.Selectors
{
    public class SelectorResult<TResult>
    {
        public TResult Result { get; set; }

        public SelectorResult(TResult result)
        {
            Result = result;
        }
    }
}