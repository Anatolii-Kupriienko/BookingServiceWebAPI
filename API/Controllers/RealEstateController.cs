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

        // TODO: PUT THIS INTO A SEPARATE CONTROLLER
        // [Authorize]
        // [HttpPost]
        // public IActionResult CreateRealEstate([FromBody] RealEstateInsertModel realEstate)
        // {
        //     try
        //     {
        //         var createdRealEstate = _realEstateService.Create(realEstate);
        //         return CreatedAtAction(nameof(GetRealEstateById), new { id = createdRealEstate.Id }, createdRealEstate);
        //     }
        //     catch (ArgumentException e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }

        // [Authorize]
        // [HttpPut]
        // public IActionResult UpdateRealEstate([FromBody] RealEstate realEstate)
        // {
        //     try
        //     {
        //         var updatedRealEstate = _realEstateService.Update(realEstate);
        //         return Ok(updatedRealEstate);
        //     }
        //     catch (ArgumentException e)
        //     {
        //         return BadRequest(e.Message);
        //     }
        // }
    }
}