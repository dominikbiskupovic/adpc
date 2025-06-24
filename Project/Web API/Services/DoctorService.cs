namespace WebApi.Services
{
    using Infrastructure;
    using Models;
    using Dtos;

    public interface IDoctorService
    {
        Task<IEnumerable<Doctor>> GetAllAsync();
        Task<Doctor?> GetByIdAsync(long id);
        Task<Doctor> CreateAsync(DoctorCreateDto dto);
        Task<Doctor?> UpdateAsync(long id, DoctorUpdateDto dto);
        Task<bool> DeleteAsync(long id);
    }

    public class DoctorService(RepositoryFactory repositoryFactory) : IDoctorService
    {
        public Task<IEnumerable<Doctor>>    GetAllAsync()           => repositoryFactory.Doctors.GetAllAsync();
        public Task<Doctor?>                GetByIdAsync(long id)   => repositoryFactory.Doctors.GetByIdAsync(id);

        public async Task<Doctor> CreateAsync(DoctorCreateDto dto)
        {
            var entity = new Doctor { UserId = dto.UserId, FullName = dto.FullName };
            await repositoryFactory.Doctors.AddAsync(entity);
            await repositoryFactory.Doctors.SaveChangesAsync();
            return entity;
        }

        public async Task<Doctor?> UpdateAsync(long id, DoctorUpdateDto dto)
        {
            var entity = await repositoryFactory.Doctors.GetByIdAsync(id); 
            
            if (entity == null)
                return null;
            
            entity.UserId=dto.UserId;
            entity.FullName=dto.FullName;
            repositoryFactory.Doctors.Update(entity);
            await repositoryFactory.Doctors.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await repositoryFactory.Doctors.GetByIdAsync(id);
            
            if (entity == null)
                return false;
            
            repositoryFactory.Doctors.Delete(entity);
            await repositoryFactory.Doctors.SaveChangesAsync();
            return true;
        }
    }
}