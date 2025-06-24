namespace WebApi.Repositories
{
    using Data;
    using Models;
    
    public interface ICheckupImageRepository : IRepository<CheckupImage> { }
    
    public class CheckupImageRepository(AppDbContext context) : GenericRepository<CheckupImage>(context), ICheckupImageRepository;
}