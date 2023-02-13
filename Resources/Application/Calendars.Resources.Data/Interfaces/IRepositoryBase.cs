namespace Calendars.Resources.Data.Interfaces;

/// <summary>
///     Base interface for all repositories.
/// </summary>
/// <typeparam name="TEntity">
///     Containable type in repository.
/// </typeparam>
/// <typeparam name="TEntityIdentifier">
///     Id of containable type in repository.
/// </typeparam>
public interface IRepositoryBase<TEntity, TEntityIdentifier>
{
    Task<TEntity> GetByIdAsync(TEntityIdentifier id);
    Task<TEntity> SaveAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntityIdentifier id);
}