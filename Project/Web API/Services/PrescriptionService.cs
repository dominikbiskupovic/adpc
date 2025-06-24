namespace WebApi.Services
{
    using Infrastructure;
    using Models;
    using Dtos;

    public interface IPrescriptionService
    {
        Task<IEnumerable<Prescription>> GetAllAsync();
        Task<Prescription?> GetByIdAsync(long id);
        Task<Prescription> CreateAsync(PrescriptionCreateDto dto);
        Task<Prescription?> UpdateAsync(long id, PrescriptionUpdateDto dto);
        Task<bool> DeleteAsync(long id);
    }

    public class PrescriptionService(RepositoryFactory repositoryFactory) : IPrescriptionService
    {
        public Task<IEnumerable<Prescription>>  GetAllAsync()           => repositoryFactory.Prescriptions.GetAllAsync();
        public Task<Prescription?>              GetByIdAsync(long id)   => repositoryFactory.Prescriptions.GetByIdAsync(id);

        public async Task<Prescription> CreateAsync(PrescriptionCreateDto dto)
        {
            var entity = new Prescription
            {
                Name = dto.Name,
                CheckupId = dto.CheckupId
            };
            await repositoryFactory.Prescriptions.AddAsync(entity);
            await repositoryFactory.Prescriptions.SaveChangesAsync();
            return entity;
        }

        public async Task<Prescription?> UpdateAsync(long id, PrescriptionUpdateDto dto)
        {
            var entity = await repositoryFactory.Prescriptions.GetByIdAsync(id);
            if (entity == null) return null;
            entity.Name = dto.Name;
            entity.CheckupId = dto.CheckupId;
            repositoryFactory.Prescriptions.Update(entity);
            await repositoryFactory.Prescriptions.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await repositoryFactory.Prescriptions.GetByIdAsync(id);
            if (entity == null) return false;
            repositoryFactory.Prescriptions.Delete(entity);
            await repositoryFactory.Prescriptions.SaveChangesAsync();
            return true;
        }
    }
}