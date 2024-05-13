using API.DbAccess.Models;
using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly SignInManager<UserModel> _signInManager;

        public AccountController(IUserService userService, SignInManager<UserModel> signInManager)
        {
            _userService = userService;
            _signInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login(UserLoginModel user)
        {
            if (ModelState.IsValid && user is not null)
            {
                await _signInManager.SignOutAsync();

                var signInResult = await _signInManager.PasswordSignInAsync(user.Username, user.Password, false, false);

                if (signInResult.Succeeded)
                {
                    return Ok();
                }

                return Unauthorized();
            }

            return BadRequest();
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register(UserRegisterModel user)
        {
            if (ModelState.IsValid && user is not null)
            {
                try
                {
                    var registerResult = await _userService.RegisterAsync(user);

                    return CreatedAtAction(nameof(Register), registerResult);
                }
                catch (Exception e) // this isn't the best way to handle exceptions in a production application
                {
                    return BadRequest(e.Message);
                }
            }

            return BadRequest();
        }

        [Authorize]
        [HttpPost]
        [Route("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }
        
        [Authorize]
        [HttpGet]
        [Route("me")]
        public async Task<IActionResult> GetMe()
        {
            var user = await _userService.GetByUsernameAsync(User.Identity!.Name!);
            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Update(UserUpdateModel user)
        {
            if (ModelState.IsValid && user is not null)
            {
                var existingUser = await _userService.GetByUsernameAsync(User.Identity!.Name!);
                if (existingUser is null || existingUser.Id != user.Id)
                {
                    return Unauthorized();
                }

                try
                {
                    var updatedUser = await _userService.UpdateAsync(user); // user is signed out after updating so we need to sign in again
                    await _signInManager.PasswordSignInAsync(updatedUser.Username, user.Password, false, false);
                    return Ok(updatedUser);
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return BadRequest();
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (ModelState.IsValid && !string.IsNullOrWhiteSpace(id))
            {
                var user = await _userService.GetByIdAsync(id);
                if (user is null || user.Id != id)
                {
                    return Unauthorized();
                }

                try
                {
                    await _signInManager.SignOutAsync();
                    var resultSuccess = await _userService.DeleteByIdAsync(id);

                    if (resultSuccess)
                    {
                        return Ok();
                    }
                }
                catch (Exception e)
                {
                    return BadRequest(e.Message);
                }
            }

            return BadRequest();
        }
    }
}