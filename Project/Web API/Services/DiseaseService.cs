namespace WebApi.Services
{
    using Infrastructure;
    using Models;
    using Dtos;

    public interface IDiseaseService
    {
        Task<IEnumerable<Disease>> GetAllAsync();
        Task<Disease?> GetByIdAsync(long id);
        Task<Disease> CreateAsync(DiseaseCreateDto dto);
        Task<Disease?> UpdateAsync(long id, DiseaseUpdateDto dto);
        Task<bool> DeleteAsync(long id);
    }

    public class DiseaseService(RepositoryFactory repositoryFactory) : IDiseaseService
    {
        public async Task<IEnumerable<Disease>> GetAllAsync()           => await repositoryFactory.Diseases.GetAllAsync();
        public async Task<Disease?>             GetByIdAsync(long id)   => await repositoryFactory.Diseases.GetByIdAsync(id);

        public async Task<Disease> CreateAsync(DiseaseCreateDto dto)
        {
            var entity = new Disease
            {
                MedicalRecordId = dto.MedicalRecordId,
                Name = dto.Name,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate
            };
            await repositoryFactory.Diseases.AddAsync(entity);
            await repositoryFactory.Diseases.SaveChangesAsync();
            return entity;
        }

        public async Task<Disease?> UpdateAsync(long id, DiseaseUpdateDto dto)
        {
            var entity = await repositoryFactory.Diseases.GetByIdAsync(id);
            if (entity == null) return null;
            entity.MedicalRecordId = dto.MedicalRecordId;
            entity.Name = dto.Name;
            entity.StartDate = dto.StartDate;
            entity.EndDate = dto.EndDate;
            repositoryFactory.Diseases.Update(entity);
            await repositoryFactory.Diseases.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await repositoryFactory.Diseases.GetByIdAsync(id);
            if (entity == null) return false;
            repositoryFactory.Diseases.Delete(entity);
            await repositoryFactory.Diseases.SaveChangesAsync();
            return true;
        }
    }
}