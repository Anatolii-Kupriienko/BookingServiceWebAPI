using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.BusinessLogic.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleService(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task<IdentityRole> CreateAsync(string roleName)
        {
            var result = await roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
            {
                var role = await roleManager.FindByNameAsync(roleName);
                return role!;
            }
            else
            {
                throw new DbUpdateException("Role creation failed");
            }
        }

        public async Task<bool> DeleteByIdAsync(string id)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role is null)
            {
                return false;
            }

            var result = await roleManager.DeleteAsync(role);
            return result.Succeeded;
        }

        public IEnumerable<IdentityRole> GetAll()
        {
            return roleManager.Roles;
        }

        public async Task<IdentityRole?> GetByIdAsync(string id)
        {
            return await roleManager.FindByIdAsync(id);
        }

        public async Task<IdentityRole> UpdateAsync(string id, string newRoleName)
        {
            var role = await roleManager.FindByIdAsync(id);
            if (role is null)
            {
                throw new ArgumentException("Role not found");
            }

            role.Name = newRoleName;
            var result = await roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return role;
            }
            else
            {
                throw new DbUpdateException("Role update failed");
            }
        }
    }
}