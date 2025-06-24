namespace WebApi.Repositories
{
    using Data;
    using Models;
    
    public interface IDiseaseRepository : IRepository<Disease> { }
    
    public class DiseaseRepository(AppDbContext context) : GenericRepository<Disease>(context), IDiseaseRepository;
}