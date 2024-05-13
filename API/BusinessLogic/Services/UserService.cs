using API.BusinessLogic.Services;
using API.DbAccess;
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
        private readonly IRepository<OwnerTypeModel> _ownerTypeRepository;
        private readonly IMapper mapper;

        public UserService(UserManager<UserModel> userManager, IMapper mapper, IRepository<OwnerTypeModel> ownerTypeRepository)
        {
            _userManager = userManager;
            this.mapper = mapper;
            _ownerTypeRepository = ownerTypeRepository;
        }

        public async Task<UserViewModel> RegisterAsync(UserRegisterModel model)
        {
            if (model == null)
            {
                throw new ArgumentNullException(nameof(model));
            }

            var userModel = mapper.Map<UserModel>(model);
            await _userManager.CreateAsync(userModel, model.Password);
            var user = await _userManager.FindByNameAsync(model.Username);
            // this is a very basic way to assign roles, in a production application you should have a more complex logic
            if (user.OwnerType is not null && user.OwnerType.Type is "Owner" or "Hotel")
            {
                await _userManager.AddToRoleAsync(user, "Owner");
            }

            return mapper.Map<UserViewModel>(user);
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user is null)
            {
                throw new ArgumentException("User not found");
            }

            var result = await _userManager.DeleteAsync(user);

            return result.Succeeded;
        }

        public async Task<IEnumerable<UserViewModel>> GetAllAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            var types = _ownerTypeRepository.Get();
            foreach (var user in users)
            {
                if (user.OwnerTypeId is null) continue;

                var type = types.FirstOrDefault(t => t.Id == user.OwnerTypeId);
                user.OwnerType = type;
            }

            return mapper.Map<IEnumerable<UserViewModel>>(users);
        }

        public async Task<UserViewModel?> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());

            if (user is not null)
            {
                var userViewModel = mapper.Map<UserViewModel>(user);
                if (user.OwnerTypeId is not null)
                {
                    var type = _ownerTypeRepository.GetById((int)user.OwnerTypeId);
                    userViewModel.OwnerType = mapper.Map<OwnerType>(type);
                }

                return userViewModel;
            }

            return null;
        }

        public async Task<UserViewModel?> GetByUsernameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);

            if (user is not null)
            {
                var userViewModel = mapper.Map<UserViewModel>(user);
                if (user.OwnerTypeId is not null)
                {
                    var type = mapper.Map<OwnerType>(_ownerTypeRepository.GetById((int)user.OwnerTypeId));
                    userViewModel.OwnerType = type;
                }

                return userViewModel;
            }

            return null;
        }

        public async Task<UserViewModel> UpdateAsync(UserUpdateModel model)
        {
            var user = await ValidateUserAsync(model);

            if (!string.IsNullOrEmpty(model.NewPassword))
            {
                await _userManager.ChangePasswordAsync(user, model.Password, model.NewPassword);
            }

            user.Email = model.Email;
            user.UserName = model.Username;
            if (model.OwnerType is not null)
            {
                user.OwnerTypeId = model.OwnerType.Id;
            }
            await _userManager.UpdateAsync(user);

            return mapper.Map<UserViewModel>(await _userManager.FindByIdAsync(model.Id!));
        }

        public async Task AddToRoleAsync(string userId, string roleName)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            if (user is null)
            {
                throw new ArgumentException("User not found");
            }

            await _userManager.AddToRoleAsync(user, roleName);
        }

        public async Task RemoveFromRoleAsync(string userId, string roleName)
        {
            var user = _userManager.Users.FirstOrDefault(u => u.Id == userId);
            if (user is null)
            {
                throw new ArgumentException("User not found");
            }

            await _userManager.RemoveFromRoleAsync(user, roleName);
        }

        public async Task<IEnumerable<UserViewModel>> GetInRoleAsync(string roleName)
        {
            var users = await _userManager.GetUsersInRoleAsync(roleName);

            return mapper.Map<IEnumerable<UserViewModel>>(users);
        }

        private async Task<UserModel> ValidateUserAsync(UserUpdateModel model)
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