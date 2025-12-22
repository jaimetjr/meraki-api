using Domain.Interfaces;
using Domain.Specifications;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);
        
        public virtual async Task<IReadOnlyCollection<T>> GetAllAsync() => 
            (await _dbSet.ToListAsync()).AsReadOnly();

        public virtual async Task<IReadOnlyCollection<T>> FindAsync(ISpecification<T> specification)
        {
            var query = _dbSet.AsQueryable();

            // Apply includes - EF Core's Include accepts Expression<Func<T, object?>>
            foreach (var include in specification.Includes)
            {
                query = EntityFrameworkQueryableExtensions.Include(query, include);
            }

            // Apply where clause
            var expression = specification.ToExpression();
            query = query.Where(expression);

            return (await query.ToListAsync()).AsReadOnly();
        }

        public virtual async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
        
        public virtual async Task UpdateAsync(T entity)
        {
            await Task.CompletedTask;
            _dbSet.Update(entity);
        }
        
        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null) _dbSet.Remove(entity);
        }
        
        public virtual async Task<bool> ExistsAsync(Guid id) => await _dbSet.FindAsync(id) != null;
    }
}
