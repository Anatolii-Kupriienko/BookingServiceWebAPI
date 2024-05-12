using API.DbAccess.Models;
using API.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserModel> _userManager;

        public UserService(UserManager<UserModel> userManager)
        {
            _userManager = userManager;
        }
        
        public void Add(User model)
        {
            throw new NotImplementedException();
        }

        public void Delete(User model)
        {
            throw new NotImplementedException();
        }

        public void DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> Get()
        {
            throw new NotImplementedException();
        }

        public User? GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(User model)
        {
            throw new NotImplementedException();
        }

        public void UpdatePassword(int id, string password)
        {
            throw new NotImplementedException();
        }
    }
}