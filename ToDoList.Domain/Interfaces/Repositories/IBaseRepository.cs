using System.Linq.Expressions;

namespace ToDoList.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity>
    {
        Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<IQueryable<TEntity>> FindByConditions(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken);
    }
}
