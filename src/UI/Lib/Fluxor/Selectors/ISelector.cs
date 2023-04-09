namespace UI.Lib.Fluxor
{
    public interface ISelector<TResult>
    {
        TResult Select(IStore state);
    }
}