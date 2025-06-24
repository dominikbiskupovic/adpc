namespace WebApi.Repositories
{
    using Data;
    using Models;
    
    public interface ICheckupRepository : IRepository<Checkup> { }
    
    public class CheckupRepository(AppDbContext context) : GenericRepository<Checkup>(context), ICheckupRepository;
}