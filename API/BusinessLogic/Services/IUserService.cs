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
    }
}