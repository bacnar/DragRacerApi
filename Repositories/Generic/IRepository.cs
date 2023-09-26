using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DragRacerApi.Repositories.Generic
{
    public interface IRepository<T> : IDisposable where T : class
    {
        Task<EntityEntry<T>> AddAsync(T entity);

        Task AddRangeAsync(List<T> entity);

        Task DeleteAsync(int id);

        Task<bool> ExistsAsync(int id);

        Task<List<T>> GetAllAsync();

        Task<T?> GetAsync(int id);

        void Update(T entity);

        void UpdateRange(List<T> entities);

        Task<bool> SaveChangesAsync();
    }
}
