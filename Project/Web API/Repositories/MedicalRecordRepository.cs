namespace WebApi.Repositories
{
    using Data;
    using Models;
    
    public interface IMedicalRecordRepository : IRepository<MedicalRecord> { }
    
    public class MedicalRecordRepository(AppDbContext context) : GenericRepository<MedicalRecord>(context), IMedicalRecordRepository;
}