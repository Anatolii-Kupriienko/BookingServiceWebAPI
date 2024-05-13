using API.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/Admin")]
    public class AdminRoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public AdminRoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet("users/roles")]
        public IActionResult GetRoles()
        {
            var roles = _roleService.GetAll();

            if (!roles.Any())
            {
                return NotFound();
            }

            return Ok(roles);
        }

        [HttpGet("users/roles/{id}")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            var role = await _roleService.GetByIdAsync(id);

            if (role is null)
            {
                return NotFound();
            }

            return Ok(role);
        }

        [HttpPost("users/roles")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest();
            }

            try
            {
                var role = await _roleService.CreateAsync(roleName);

                return CreatedAtAction(nameof(GetRoles), new { id = role.Id }, role);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("users/roles/{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            try
            {
                var resultSuccess = await _roleService.DeleteByIdAsync(id);
                if (resultSuccess)
                {
                    return NoContent();
                }

                return StatusCode(304);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut("users/roles/{id}")]
        public async Task<IActionResult> UpdateRole(string id, [FromBody] string newRoleName)
        {
            if (string.IsNullOrWhiteSpace(newRoleName))
            {
                return BadRequest();
            }

            try
            {
                var updatedRole = await _roleService.UpdateAsync(id, newRoleName);

                return Ok(updatedRole);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}