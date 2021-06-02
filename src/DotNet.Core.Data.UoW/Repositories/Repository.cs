using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DotNet.Core.Data.UoW.Persistence;
using DotNet.Core.Data.UoW.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DotNet.Core.Data.UoW.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
  {
    protected readonly ApplicationContext DbContext;

    public Repository(ApplicationContext dbContext)
    {
      DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    #region GET Methods
    
    /// <summary>
    ///     Gets entity by id.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns></returns>
    public virtual async Task<T> GetByIdAsync(int id)
    {
      return await DbContext.Set<T>().FindAsync(id);
    }

    /// <summary>
    ///     Gets entity by long id.
    /// </summary>
    /// <param name="id">The id value.</param>
    /// <returns></returns>
    public virtual async Task<T> GetByLongIdAsync(long id)
    {
      return await DbContext.Set<T>().FindAsync(id);
    }

    /// <summary>
    /// Gets the First entity based on a predicate.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns></returns>
    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
      return await DbContext.Set<T>().FirstOrDefaultAsync(predicate);
    }

    /// <summary>
    /// Gets the First entity based on a predicate and include delegate.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <returns></returns>
    public async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
    {
      IQueryable<T> query = DbContext.Set<T>();
      if (include != null) query = include(query);
      return await query.FirstOrDefaultAsync(predicate);
    }

    /// <summary>
    ///     Gets all.
    /// </summary>
    /// <returns>List of all the elements</returns>
    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
      return await DbContext.Set<T>().ToListAsync();
    }

    /// <summary>
    /// Gets all the entities based on a predicate
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <returns>An IReadOnlyList that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
    {
      return await DbContext.Set<T>().Where(predicate).ToListAsync();
    }

    /// <summary>
    /// Gets all the entities based on a predicate, OrderBy delegate and include delegate. This method defaults no-tracking query.
    /// </summary>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <param name="orderBy">A function to order elements.</param>
    /// <param name="include">A function to include navigation properties</param>
    /// <param name="disableTracking"><c>True</c> to disable changing tracking; otherwise, <c>false</c>. Default to <c>true</c>.</param>
    /// <returns>An IReadOnlyList that contains elements that satisfy the condition specified by <paramref name="predicate"/>.</returns>
    /// <remarks>This method default no-tracking query.</remarks>
    public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null,
      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
      Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null,
      bool disableTracking = true)
    {
      IQueryable<T> query = DbContext.Set<T>();
      if (disableTracking) query = query.AsNoTracking();

      if (include != null) query = include(query);

      if (predicate != null) query = query.Where(predicate);

      if (orderBy != null)
        return await orderBy(query).ToListAsync();
      return await query.ToListAsync();
    }

    #endregion

    #region ADD Methods

    /// <summary>
    ///     Adds the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public async Task AddAsync(T entity)
    {
      await DbContext.Set<T>().AddAsync(entity);
    }

    /// <summary>
    ///     Adds the list of specified entities.
    /// </summary>
    /// <param name="entities">The entity.</param>
    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
      await DbContext.Set<T>().AddRangeAsync(entities);
    }

    #endregion

    #region UPDATE Methods

    /// <summary>
    ///     Updates the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public async Task UpdateAsync(T entity)
    {
      DbContext.Entry(entity).State = EntityState.Modified;
    }

    #endregion

    #region DELETE Methods

    /// <summary>
    ///     Deletes the specified entity.
    /// </summary>
    /// <param name="entity">The entity.</param>
    public async Task DeleteAsync(T entity)
    {
      DbContext.Set<T>().Remove(entity);
    }

    /// <summary>
    ///     Deletes one or many entities matching the specified criteria
    /// </summary>
    /// <param name="criteria">The criteria.</param>
    /// <remarks>Count of entities deleted.</remarks>
    public async Task DeleteAsync(Expression<Func<T, bool>> criteria)
    {
      var records = await GetAsync(criteria);
      if (records.Any())
        DbContext.Set<T>().RemoveRange(records);
    }

    #endregion
  }
}
