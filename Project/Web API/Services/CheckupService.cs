namespace WebApi.Services
{
    using Infrastructure;
    using Models;
    using Dtos;

    public interface ICheckupService
    {
        Task<IEnumerable<Checkup>> GetAllAsync();
        Task<Checkup?> GetByIdAsync(long id);
        Task<Checkup> CreateAsync(CheckupCreateDto dto);
        Task<Checkup?> UpdateAsync(long id, CheckupUpdateDto dto);
        Task<bool> DeleteAsync(long id);
    }

    public class CheckupService(RepositoryFactory repositoryFactory) : ICheckupService
    {
        public Task<IEnumerable<Checkup>>   GetAllAsync()           => repositoryFactory.Checkups.GetAllAsync();
        public Task<Checkup?>               GetByIdAsync(long id)   => repositoryFactory.Checkups.GetByIdAsync(id);

        public async Task<Checkup> CreateAsync(CheckupCreateDto dto)
        {
            var entity = new Checkup
            {
                PatientId = dto.PatientId,
                DoctorId = dto.DoctorId,
                Date = dto.Date,
                Type = dto.Type
            };
            await repositoryFactory.Checkups.AddAsync(entity);
            await repositoryFactory.Checkups.SaveChangesAsync();
            return entity;
        }

        public async Task<Checkup?> UpdateAsync(long id, CheckupUpdateDto dto)
        {
            var entity = await repositoryFactory.Checkups.GetByIdAsync(id);
            if (entity == null) return null;
            entity.PatientId = dto.PatientId;
            entity.DoctorId = dto.DoctorId;
            entity.Date = dto.Date;
            entity.Type = dto.Type;
            repositoryFactory.Checkups.Update(entity);
            await repositoryFactory.Checkups.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await repositoryFactory.Checkups.GetByIdAsync(id);
            if (entity == null) return false;
            repositoryFactory.Checkups.Delete(entity);
            await repositoryFactory.Checkups.SaveChangesAsync();
            return true;
        }
    }
}