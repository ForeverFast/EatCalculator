namespace EatCalculator.UI.Shared.Lib.EntityAdapter
{
    public abstract record EntityState<TKey, TEntity>
        where TKey : notnull
        where TEntity : class
    {
        public IDictionary<TKey, TEntity> Entities { get; internal set; } = new Dictionary<TKey, TEntity>();

        public static EntityAdapter<TKey, TEntity> GetAdapter()
            => Adapters.Get<TKey, TEntity>();
    }
}
