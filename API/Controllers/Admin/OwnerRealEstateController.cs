using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admin
{
    [Authorize(Roles = "Owner")]
    [ApiController]
    [Route("api/v1/RealEstate")]
    public class OwnerRealEstateController : ControllerBase
    {
        private readonly IRealEstateService _realEstateService;
        private readonly IUserService _userService;

        public OwnerRealEstateController(IRealEstateService realEstateService, IUserService userService)
        {
            _realEstateService = realEstateService;
            _userService = userService;
        }

        [HttpPost("owned")]
        public async Task<IActionResult> CreateRealEstate([FromBody] RealEstateInsertModel realEstate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await ValidateOwner(realEstate.OwnerId))
            {
                return Unauthorized();
            }

            try
            {
                var createdRealEstate = _realEstateService.Create(realEstate);
                return CreatedAtAction(nameof(CreateRealEstate), new { id = createdRealEstate.Id }, createdRealEstate);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet("owned")]
        public async Task<IActionResult> GetAllRealEstate()
        {
            if (User.Identity == null)
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
                var realEstates = _realEstateService.GetByOwner(user.Id);
                return Ok(realEstates);
            }
            catch (ArgumentException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("owned")]
        public async Task<IActionResult> UpdateRealEstate([FromBody] RealEstateUpdateModel realEstate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await ValidateOwner(realEstate.OwnerId))
            {
                return Unauthorized();
            }
            try
            {
                var updatedRealEstate = _realEstateService.Update(realEstate);
                return Ok(updatedRealEstate);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("owned/{id}")]
        public async Task<IActionResult> DeleteRealEstate(int id)
        {
            var realEstate = _realEstateService.GetById(id);
            if (realEstate is null)
            {
                return StatusCode(304);
            }

            if (!await ValidateOwner(realEstate.Owner.Id!))
            {
                return Unauthorized();
            }

            try
            {
                var result = _realEstateService.DeleteById(id);
                if (!result)
                {
                    return StatusCode(304);
                }
                return NoContent();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut("owned/{id}/free")]
        public async Task<IActionResult> FreeRealEstate(int id)
        {
            var realEstate = _realEstateService.GetById(id);
            if (realEstate is null)
            {
                return NotFound();
            }

            if (!await ValidateOwner(realEstate.Owner.Id!))
            {
                return Unauthorized();
            }

            try
            {
                _realEstateService.FreeRealEsate(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        private async Task<bool> ValidateOwner(string realEsateOwnerId)
        {
            if (User.Identity == null)
            {
                return false;
            }

            var user = await _userService.GetByUsernameAsync(User.Identity.Name!);
            return user != null && user.Id == realEsateOwnerId;
        }
    }
}