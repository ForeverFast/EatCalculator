using Fluxor;
using Mapster;

namespace EatCalculator.UI.Shared.Lib.EntityAdapter
{
    public abstract class EntityAdapter<TKey, TEntity>
        where TKey : notnull
        where TEntity : class
    {
        protected abstract Func<TEntity, TKey> SelectId { get; }

        public abstract EntityState<TKey, TEntity> GetInitialState();


        /// <summary>
        /// Add one entity to the collection.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="state"></param>
        public void Add(TEntity entity, EntityState<TKey, TEntity> state)
        {
            state.Entities.TryAdd(SelectId(entity), entity);
        }

        /// <summary>
        /// Add multiple entities to the collection.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="state"></param>
        public void AddRange(IEnumerable<TEntity> entities, EntityState<TKey, TEntity> state)
        {
            foreach (var entity in entities)
                state.Entities.TryAdd(SelectId(entity), entity);
        }

        /// <summary>
        /// Replace current collection with provided collection.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="state"></param>
        public TState SetAll<TState>(IEnumerable<TEntity> entities, EntityState<TKey, TEntity> state)
            where TState : EntityState<TKey, TEntity>
            => (TState)(state with
            {
                Entities = entities.ToDictionary(x => SelectId(x), x => x),
            });

        /// <summary>
        /// Add or Replace one entity in the collection.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="state"></param>
        public void SetOne(TEntity entity, EntityState<TKey, TEntity> state)
        {
            state.Entities[SelectId(entity)] = entity;
        }

        /// <summary>
        /// Add or Replace multiple entities in the collection.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="state"></param>
        public void SetMany(IEnumerable<TEntity> entities, EntityState<TKey, TEntity> state)
        {
            foreach (var entity in entities)
                state.Entities[SelectId(entity)] = entity;
        }


        /// <summary>
        /// Remove one entity from the collection.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        public void Remove(TKey id, EntityState<TKey, TEntity> state)
        {
            state.Entities.Remove(id);
        }

        /// <summary>
        /// Remove multiple entities from the collection, by ids.
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="state"></param>
        public void RemoveRange(IEnumerable<TKey> ids, EntityState<TKey, TEntity> state)
        {
            foreach (var id in ids)
                state.Entities.Remove(id);
        }

        /// <summary>
        /// Remove multiple entities from the collection, by predicate.
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="state"></param>
        public void RemoveRange(Predicate<TEntity> predicate, EntityState<TKey, TEntity> state)
        {
            foreach (var entity in state.Entities.Where(x => predicate(x.Value)).ToList())
                state.Entities.Remove(entity.Key);
        }

        /// <summary>
        /// Clear entity collection.
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="state"></param>
        public void RemoveAll(EntityState<TKey, TEntity> state)
            => state.Entities.Clear();



        /// <summary>
        /// Update one entity in the collection.
        /// </summary>
        /// <param name="updatedEntity"></param>
        /// <param name="state"></param>
        public void Update(TEntity updatedEntity, EntityState<TKey, TEntity> state)
        {
            if (!state.Entities.TryGetValue(SelectId(updatedEntity), out var targetEntity))
                return;

            updatedEntity.Adapt(targetEntity);
        }

        /// <summary>
        /// Update multiple entities in the collection. Supports partial updates.
        /// </summary>
        /// <param name="updatedEntities"></param>
        /// <param name="state"></param>
        public void UpdateRange(IEnumerable<TEntity> updatedEntities, EntityState<TKey, TEntity> state)
        {
            foreach (var updatedEntity in updatedEntities)
            {
                if (!state.Entities.TryGetValue(SelectId(updatedEntity), out var targetEntity))
                    continue;

                updatedEntity.Adapt(targetEntity);
            }
        }

        /// <summary>
        /// Add or Update one entity in the collection.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="state"></param>
        public void Upsert(TEntity entity, EntityState<TKey, TEntity> state)
        {
            if (!state.Entities.TryGetValue(SelectId(entity), out var targetEntity))
            {
                state.Entities.TryAdd(SelectId(entity), entity);
                return;
            }

            entity.Adapt(targetEntity);
        }

        /// <summary>
        /// Add or Update multiple entities in the collection.
        /// </summary>
        /// <param name="entities"></param>
        /// <param name="state"></param>
        public void UpsertRange(IEnumerable<TEntity> entities, EntityState<TKey, TEntity> state)
        {
            foreach (var entity in entities)
            {
                if (!state.Entities.TryGetValue(SelectId(entity), out var targetEntity))
                {
                    state.Entities.TryAdd(SelectId(entity), entity);
                    return;
                }

                entity.Adapt(targetEntity);
            }
        }
    }
}
