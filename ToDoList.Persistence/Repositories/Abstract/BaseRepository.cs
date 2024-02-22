using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ToDoList.Domain.Interfaces.Repositories;

namespace ToDoList.Persistence.Repositories.Abstract
{
    abstract public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        private readonly DataContext _context;

        protected BaseRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<TEntity> CreateAsync(TEntity entity, CancellationToken cancellationToken)
        {
            await _context.Set<TEntity>().AddAsync(entity, cancellationToken);

            return entity;
        }

        public async Task DeleteAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public async Task<IQueryable<TEntity>> FindByConditions(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            return await Task.FromResult(_context.Set<TEntity>().Where(expression).AsNoTracking());
        }

        public async Task<IQueryable<TEntity>> GetAllAsync()
        {
            return await Task.FromResult(_context.Set<TEntity>().AsNoTracking());
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            // _context.Entry(entity).State = EntityState.Modified;
            await Task.FromResult(_context.Set<TEntity>().Update(entity));
            return entity;
        }
    }
}
