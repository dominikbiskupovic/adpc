namespace WebApi.Services
{
    using Infrastructure;
    using Models;
    using Dtos;

    public interface IMedicalRecordService
    {
        Task<IEnumerable<MedicalRecord>> GetAllAsync();
        Task<MedicalRecord?> GetByIdAsync(long id);
        Task<MedicalRecord> CreateAsync(MedicalRecordCreateDto dto);
        Task<MedicalRecord?> UpdateAsync(long id, MedicalRecordUpdateDto dto);
        Task<bool> DeleteAsync(long id);
    }

    public class MedicalRecordService(RepositoryFactory repositoryFactory) : IMedicalRecordService
    {
        public async Task<IEnumerable<MedicalRecord>>   GetAllAsync()           => await repositoryFactory.MedicalRecords.GetAllAsync();
        public async Task<MedicalRecord?>               GetByIdAsync(long id)   => await repositoryFactory.MedicalRecords.GetByIdAsync(id);

        public async Task<MedicalRecord> CreateAsync(MedicalRecordCreateDto dto)
        {
            var entity = new MedicalRecord { };
            await repositoryFactory.MedicalRecords.AddAsync(entity);
            await repositoryFactory.MedicalRecords.SaveChangesAsync();
            return entity;
        }

        public async Task<MedicalRecord?> UpdateAsync(long id, MedicalRecordUpdateDto dto)
        {
            var entity = await repositoryFactory.MedicalRecords.GetByIdAsync(id);
            if (entity == null) return null;
            repositoryFactory.MedicalRecords.Update(entity);
            await repositoryFactory.MedicalRecords.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await repositoryFactory.MedicalRecords.GetByIdAsync(id);
            if (entity == null) return false;
            repositoryFactory.MedicalRecords.Delete(entity);
            await repositoryFactory.MedicalRecords.SaveChangesAsync();
            return true;
        }
    }
}