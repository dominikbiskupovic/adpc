namespace WebApi.Repositories
{
    using Data;
    using Models;
    
    public interface IUserRepository : IRepository<User> { }
    
    public class UserRepository(AppDbContext context) : GenericRepository<User>(context), IUserRepository;
}