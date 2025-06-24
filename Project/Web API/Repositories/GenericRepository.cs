namespace WebApi.Repositories
{
    using Microsoft.EntityFrameworkCore;
    using Data;
    
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(long id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
    }

    public class GenericRepository<T>(AppDbContext ctx) : IRepository<T> where T : class
    {
        public async Task<IEnumerable<T>>   GetAllAsync()           => await ctx.Set<T>().ToListAsync();
        public async Task<T?>               GetByIdAsync(long id)   => await ctx.Set<T>().FindAsync(id);
        public async Task                   AddAsync(T entity)      => await ctx.Set<T>().AddAsync(entity);
        public async Task                   SaveChangesAsync()      => await ctx.SaveChangesAsync();
        
        public void Update(T entity) => ctx.Set<T>().Update(entity);
        public void Delete(T entity) => ctx.Set<T>().Remove(entity);
    }
}