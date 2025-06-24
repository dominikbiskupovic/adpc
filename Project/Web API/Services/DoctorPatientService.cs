namespace WebApi.Services
{
    using Infrastructure;
    using Models;
    using Dtos;

    public interface IDoctorPatientService
    {
        Task<IEnumerable<DoctorPatient>> GetAllAsync();
        Task<DoctorPatient?> GetByIdAsync(long id);
        Task<DoctorPatient> CreateAsync(DoctorPatientCreateDto dto);
        Task<DoctorPatient?> UpdateAsync(long id, DoctorPatientUpdateDto dto);
        Task<bool> DeleteAsync(long id);
    }

    public class DoctorPatientService(RepositoryFactory repositoryFactory) : IDoctorPatientService
    {
        public Task<IEnumerable<DoctorPatient>> GetAllAsync()           => repositoryFactory.DoctorPatients.GetAllAsync();
        public Task<DoctorPatient?>             GetByIdAsync(long id)   => repositoryFactory.DoctorPatients.GetByIdAsync(id);

        public async Task<DoctorPatient> CreateAsync(DoctorPatientCreateDto dto)
        {
            var entity = new DoctorPatient { DoctorId = dto.DoctorId, PatientId = dto.PatientId };
            await repositoryFactory.DoctorPatients.AddAsync(entity);
            await repositoryFactory.DoctorPatients.SaveChangesAsync();
            return entity;
        }

        public async Task<DoctorPatient?> UpdateAsync(long id, DoctorPatientUpdateDto dto)
        {
            var entity = await repositoryFactory.DoctorPatients.GetByIdAsync(id);
            if (entity == null) return null;
            entity.DoctorId = dto.DoctorId;
            entity.PatientId = dto.PatientId;
            repositoryFactory.DoctorPatients.Update(entity);
            await repositoryFactory.DoctorPatients.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await repositoryFactory.DoctorPatients.GetByIdAsync(id);
            if (entity == null) return false;
            repositoryFactory.DoctorPatients.Delete(entity);
            await repositoryFactory.DoctorPatients.SaveChangesAsync();
            return true;
        }
    }
}