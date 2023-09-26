using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using DragRacerApi.Contexts;

namespace DragRacerApi.Repositories.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : class
    {
        protected readonly RacerContext context;

        public GenericRepository(RacerContext context)
        {
            this.context = context;
        }

        public Task<EntityEntry<T>> AddAsync(T entity)
        {
            return context.AddAsync(entity).AsTask();
        }

        public Task AddRangeAsync(List<T> entities)
        {
            return context.AddRangeAsync(entities);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);

            if (entity == null)
            {
                throw new ArgumentException($"Entity with ID {id} does not exist.");
            }

            context.Set<T>().Remove(entity);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await GetAsync(id) != null;
        }

        public Task<List<T>> GetAllAsync()
        {
            return context.Set<T>().ToListAsync();
        }

        public Task<T?> GetAsync(int id)
        {
            return context.Set<T>().FindAsync(id).AsTask();
        }

        public void Update(T entity)
        {
            context.Update(entity);
        }

        public void UpdateRange(List<T> entities)
        {
            context.UpdateRange(entities);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                context.Dispose();
            }
        }
    }
}
