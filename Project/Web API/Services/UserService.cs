namespace WebApi.Services
{
    using Infrastructure;
    using Models;
    using Dtos;

    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User?> GetByIdAsync(long id);
        Task<User> CreateAsync(UserCreateDto dto);
        Task<User?> UpdateAsync(long id, UserUpdateDto dto);
        Task<bool> DeleteAsync(long id);
    }

    public class UserService(RepositoryFactory repositoryFactory) : IUserService
    {
        public Task<IEnumerable<User>>  GetAllAsync()           => repositoryFactory.Users.GetAllAsync();
        public Task<User?>              GetByIdAsync(long id)   => repositoryFactory.Users.GetByIdAsync(id);

        public async Task<User> CreateAsync(UserCreateDto dto)
        {
            var entity = new User
            {
                Username = dto.Username,
                Password = dto.Password,
                IsAdmin = dto.IsAdmin
            };
            await repositoryFactory.Users.AddAsync(entity);
            await repositoryFactory.Users.SaveChangesAsync();
            return entity;
        }

        public async Task<User?> UpdateAsync(long id, UserUpdateDto dto)
        {
            var entity = await repositoryFactory.Users.GetByIdAsync(id);
            if (entity == null) return null;
            entity.Username = dto.Username;
            entity.Password = dto.Password;
            entity.IsAdmin = dto.IsAdmin;
            repositoryFactory.Users.Update(entity);
            await repositoryFactory.Users.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await repositoryFactory.Users.GetByIdAsync(id);
            if (entity == null) return false;
            repositoryFactory.Users.Delete(entity);
            await repositoryFactory.Users.SaveChangesAsync();
            return true;
        }
    }
}