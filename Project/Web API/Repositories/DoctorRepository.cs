namespace WebApi.Repositories
{
    using Data;
    using Models;
    
    public interface IDoctorRepository : IRepository<Doctor> { }
    
    public class DoctorRepository(AppDbContext context) : GenericRepository<Doctor>(context), IDoctorRepository;
}