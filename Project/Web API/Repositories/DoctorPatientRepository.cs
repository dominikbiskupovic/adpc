namespace WebApi.Repositories
{
    using Data;
    using Models;
    
    public interface IDoctorPatientRepository : IRepository<DoctorPatient> { }
    
    public class DoctorPatientRepository(AppDbContext context) : GenericRepository<DoctorPatient>(context), IDoctorPatientRepository;
}