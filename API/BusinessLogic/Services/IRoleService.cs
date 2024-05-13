using Microsoft.AspNetCore.Identity;

namespace API.BusinessLogic.Services
{
    public interface IRoleService 
    {
        Task<IdentityRole> CreateAsync(string roleName);
        IEnumerable<IdentityRole> GetAll();
        Task<IdentityRole?> GetByIdAsync(string id);
        Task<bool> DeleteByIdAsync(string id);
        Task<IdentityRole> UpdateAsync(string id, string newRoleName);
    }
}