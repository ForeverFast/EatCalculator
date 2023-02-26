﻿using System.Collections.Immutable;

namespace EatCalculator.UI.Shared.Lib.EntityAdapter
{
    public abstract record EntityState<TKey, TEntity>
        where TKey : notnull
        where TEntity : class
    {
        public ImmutableDictionary<TKey, TEntity> Entities { get; internal set; } = ImmutableDictionary<TKey, TEntity>.Empty;

        public static EntityAdapter<TKey, TEntity> GetAdapter()
            => Adapters.Get<TKey, TEntity>();
    }
}
