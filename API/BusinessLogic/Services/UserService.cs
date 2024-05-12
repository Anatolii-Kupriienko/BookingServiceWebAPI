using API.DbAccess.Models;
using API.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly IMapper mapper;

        public UserService(UserManager<UserModel> userManager, IMapper mapper)
        {
            _userManager = userManager;
            this.mapper = mapper;
        }

        public async Task<UserViewModel> Add(User model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var userModel = mapper.Map<UserModel>(model);
            await _userManager.CreateAsync(userModel, model.Password);
            var user = await _userManager.FindByNameAsync(model.Username);
            return mapper.Map<UserViewModel>(user);
        }

        public async Task<bool> Delete(User model)
        {
            var result = await _userManager.DeleteAsync(mapper.Map<UserModel>(model));

            return result.Succeeded;
        }

        public async Task<bool> DeleteById(string id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is null)
            {
                throw new ArgumentException("User not found");
            }

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        public async Task<IEnumerable<UserViewModel>> Get()
        {
            var users = await _userManager.Users.ToListAsync();

            return mapper.Map<IEnumerable<UserViewModel>>(users);
        }

        public async Task<UserViewModel?> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user is not null)
            {
                return mapper.Map<UserViewModel>(user);
            }

            return null;
        }

        public async Task<UserViewModel> Update(User model)
        {
            var user = await ValidateUser(model);

            if (!string.IsNullOrEmpty(model.NewPassword))
            {
                await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
            }

            user = mapper.Map<UserModel>(model);
            await _userManager.UpdateAsync(user);

            return mapper.Map<UserViewModel>(await _userManager.FindByIdAsync(model.Id!));
        }

        private async Task<UserModel> ValidateUser(User model)
        {
            var user = await _userManager.FindByIdAsync(model.Id ?? string.Empty);
            if (user is null)
            {
                throw new ArgumentException("User not found");
            }

            var passwordCheckResult = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!passwordCheckResult)
            {
                throw new ArgumentException("Password is incorrect");
            }

            return user;
        }

    }
}