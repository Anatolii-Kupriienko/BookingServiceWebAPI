using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class RealEstateController : ControllerBase
    {
        private readonly IRealEstateService _realEstateService;
        private readonly IUserService _userService;

        public RealEstateController(IRealEstateService realEstateService, IUserService userService)
        {
            _realEstateService = realEstateService;
            _userService = userService;
        }        

        [Authorize]
        [HttpGet("vacant")]
        public IActionResult GetVacantRealEstates()
        {
            var realEstates = _realEstateService.GetVacant();
            if (!realEstates.Any())
            {
                return NotFound();
            }

            return Ok(realEstates);
        }

        [Authorize]
        [HttpGet("{id}")]
        public IActionResult GetRealEstateById(int id)
        {
            var realEstate = _realEstateService.GetById(id);
            if (realEstate is null)
            {
                return NotFound();
            }

            return Ok(realEstate);
        }

        [Authorize]
        [HttpPut("{id}/book")]
        public async Task<IActionResult> BookRealEstate(int id)
        {
            if (!ModelState.IsValid || User.Identity is null)
            {
                return Unauthorized();
            }

            var user = await _userService.GetByUsernameAsync(User.Identity.Name!);

            if (user is null)
            {
                return Unauthorized();
            }

            try
            {
                await _realEstateService.BookForUser(id, user.Id);
                return Ok();
            }
            catch (ArgumentException e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}