namespace EatCalculator.UI.Shared.Lib.Fluxor.Selectors
{
    public interface ISelector<TResult>
    {
        TResult Select(IStore state);
    }
}