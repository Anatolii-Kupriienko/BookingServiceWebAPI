using API.Models;

namespace API.Services
{
    public interface IUserService
    {
        Task<UserViewModel> RegisterAsync(UserRegisterModel model);
        Task<bool> DeleteByIdAsync(string id);
        Task<IEnumerable<UserViewModel>> GetAllAsync();
        Task<UserViewModel?> GetByIdAsync(string id);
        Task<UserViewModel?> GetByUsernameAsync(string username);
        Task<UserViewModel> UpdateAsync(UserUpdateModel model);
        Task AddToRoleAsync(string userId, string roleName);
        Task RemoveFromRoleAsync(string userId, string roleName);
        Task<IEnumerable<UserViewModel>> GetInRoleAsync(string roleName);
    }
}