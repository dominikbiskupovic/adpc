namespace WebApi.Repositories
{
    using Data;
    using Models;
    
    public interface IPrescriptionRepository : IRepository<Prescription> { }
    
    public class PrescriptionRepository(AppDbContext context) : GenericRepository<Prescription>(context), IPrescriptionRepository;
}