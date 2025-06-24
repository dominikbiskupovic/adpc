namespace WebApi.Services
{
    using Infrastructure;
    using Models;
    using Dtos;
    
    public interface IPatientService
    {
        Task<IEnumerable<Patient>> GetAllAsync();
        Task<Patient?> GetByIdAsync(long id);
        Task<Patient> CreateAsync(PatientCreateDto dto);
        Task<Patient?> UpdateAsync(long id, PatientUpdateDto dto);
        Task<bool> DeleteAsync(long id);
    }
    public class PatientService(RepositoryFactory repositoryFactory) : IPatientService
    {
        public async Task<IEnumerable<Patient>> GetAllAsync()           => await repositoryFactory.Patients.GetAllAsync();
        public async Task<Patient?>             GetByIdAsync(long id)   => await repositoryFactory.Patients.GetByIdAsync(id);

        public async Task<Patient> CreateAsync(PatientCreateDto dto)
        {
            var patient = new Patient {
                PersonalId   = dto.PersonalId,
                Name         = dto.Name,
                Surname      = dto.Surname,
                DateOfBirth  = dto.DateOfBirth,
                Sex          = dto.Sex
            };
            await repositoryFactory.Patients.AddAsync(patient);
            await repositoryFactory.Patients.SaveChangesAsync();

            var record = new MedicalRecord { PatientId = patient.Id };
            
            await repositoryFactory.MedicalRecords.AddAsync(record);
            await repositoryFactory.MedicalRecords.SaveChangesAsync();

            return patient;
        }

        public async Task<Patient?> UpdateAsync(long id, PatientUpdateDto dto)
        {
            var entity = await repositoryFactory.Patients.GetByIdAsync(id);
            
            if (entity == null) 
                return null;
            
            entity.PersonalId = dto.PersonalId;
            entity.Name = dto.Name;
            entity.Surname = dto.Surname;
            entity.DateOfBirth = dto.DateOfBirth;
            entity.Sex = dto.Sex;
            
            repositoryFactory.Patients.Update(entity);
            
            await repositoryFactory.Patients.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await repositoryFactory.Patients.GetByIdAsync(id);
            
            if (entity == null) 
                return false;
            
            repositoryFactory.Patients.Delete(entity);
            
            await repositoryFactory.Patients.SaveChangesAsync();
            return true;
        }
    }
}