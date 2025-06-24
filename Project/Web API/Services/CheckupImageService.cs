namespace WebApi.Services
{
    using Infrastructure;
    using Models;
    using Dtos;

    public interface ICheckupImageService
    {
        Task<IEnumerable<CheckupImage>> GetAllAsync();
        Task<CheckupImage?> GetByIdAsync(long id);
        Task<CheckupImage> CreateAsync(CheckupImageCreateDto dto);
        Task<CheckupImage?> UpdateAsync(long id, CheckupImageUpdateDto dto);
        Task<bool> DeleteAsync(long id);
    }

    public class CheckupImageService(RepositoryFactory repositoryFactory) : ICheckupImageService
    {
        public Task<IEnumerable<CheckupImage>>  GetAllAsync()           => repositoryFactory.CheckupImages.GetAllAsync();
        public Task<CheckupImage?>              GetByIdAsync(long id)   => repositoryFactory.CheckupImages.GetByIdAsync(id);

        public async Task<CheckupImage> CreateAsync(CheckupImageCreateDto dto)
        {
            var entity = new CheckupImage
            {
                CheckupId = dto.CheckupId,
                FileUrl = dto.FileUrl
            };
            await repositoryFactory.CheckupImages.AddAsync(entity);
            await repositoryFactory.CheckupImages.SaveChangesAsync();
            return entity;
        }

        public async Task<CheckupImage?> UpdateAsync(long id, CheckupImageUpdateDto dto)
        {
            var entity = await repositoryFactory.CheckupImages.GetByIdAsync(id);
            if (entity == null) return null;
            entity.CheckupId = dto.CheckupId;
            entity.FileUrl = dto.FileUrl;
            repositoryFactory.CheckupImages.Update(entity);
            await repositoryFactory.CheckupImages.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await repositoryFactory.CheckupImages.GetByIdAsync(id);
            if (entity == null) return false;
            repositoryFactory.CheckupImages.Delete(entity);
            await repositoryFactory.CheckupImages.SaveChangesAsync();
            return true;
        }
    }
}