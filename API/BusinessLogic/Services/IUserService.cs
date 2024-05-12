using API.Models;

namespace API.Services
{
    public interface IUserService
    {
        Task<UserViewModel> Add(User model);
        Task<bool> Delete(User model);
        Task<bool> DeleteById(string id);
        Task<IEnumerable<UserViewModel>> Get();
        Task<UserViewModel?> GetById(string id);
        Task<UserViewModel> Update(User model);
    }
}