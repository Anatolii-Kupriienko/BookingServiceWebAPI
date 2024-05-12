using API.Models;

namespace API.Services
{
    public interface IUserService : ICrud<User>
    {
        void UpdatePassword(int id, string password);  
    }
}