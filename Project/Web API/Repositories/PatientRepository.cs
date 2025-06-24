namespace WebApi.Repositories
{
    using Data;
    using Models;
    
    public interface IPatientRepository : IRepository<Patient> { }
    
    public class PatientRepository(AppDbContext ctx) : GenericRepository<Patient>(ctx), IPatientRepository;
}