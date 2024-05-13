using API.BusinessLogic.Services;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        public AdminController(IUserService userService, IRoleService roleService)
        {
            _userService = userService;
            _roleService = roleService;
        }

        [HttpGet("users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetAllAsync();

            if (!users.Any())
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpGet("users/{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userService.GetByIdAsync(id);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpDelete("users/{id}")]
        public async Task<IActionResult> DeleteById(string id)
        {
            try
            {
                var resultSuccess = await _userService.DeleteByIdAsync(id);
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

        [HttpPut("users/{id}")]
        public async Task<IActionResult> UpdateUser(UserUpdateModel model)
        {
            if (ModelState.IsValid && model is not null)
            {
                try
                {
                    var updatedModel = await _userService.UpdateAsync(model);

                    return Ok(updatedModel);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return BadRequest();
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

        [HttpPost("users/{id}/roles/{roleName}")]
        public async Task<IActionResult> AddRoleToUser(string id, string roleName)
        {
            try
            {
                await _userService.AddToRoleAsync(id, roleName);

                return NoContent();
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(304);
            }
        }

        [HttpDelete("users/{id}/roles/{roleName}")]
        public async Task<IActionResult> RemoveRoleFromUser(string id, string roleName)
        {
            try
            {
                await _userService.RemoveFromRoleAsync(id, roleName);

                return NoContent();
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
            catch (Exception)
            {
                return StatusCode(304);
            }
        }

        [HttpGet("users/roles/{roleName}/users")]
        public async Task<IActionResult> GetUsersByRole(string roleName)
        {
            var users = await _userService.GetInRoleAsync(roleName);

            if (!users.Any())
            {
                return NotFound();
            }

            return Ok(users);
        }

    }
}