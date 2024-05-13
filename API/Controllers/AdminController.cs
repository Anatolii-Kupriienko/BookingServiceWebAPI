using System.Runtime.CompilerServices;
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

        public AdminController(IUserService userService)
        {
            _userService = userService;
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

        [HttpPut("users")]
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

        [HttpPost("users/{id}/roles/{roleName}")]
        public async Task<IActionResult> AddUserToRole(string id, string roleName)
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
        public async Task<IActionResult> RemoveUserFromRole(string id, string roleName)
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