using API.Models;
using API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/v1/RealEstate")]
    public class AdminRealEstateController : ControllerBase
    {
        private readonly IRealEstateService _realEstateService;

        public AdminRealEstateController(IRealEstateService realEstateService)
        {
            _realEstateService = realEstateService;
        }

        [HttpGet]
        public IActionResult GetAllRealEstate()
        {
            var realEstates = _realEstateService.GetAll();
            if (!realEstates.Any())
            {
                return NotFound();
            }

            return Ok(realEstates);
        }

        [HttpPost]
        public IActionResult CreateRealEstate([FromBody] RealEstateInsertModel realEstate)
        {
            try
            {
                var createdRealEstate = _realEstateService.Create(realEstate);
                return CreatedAtAction(nameof(CreateRealEstate), new { id = createdRealEstate.Id }, createdRealEstate);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRealEstate(int id)
        {
            if (!_realEstateService.DeleteById(id))
            {
                return StatusCode(304);
            }

            return NoContent();
        }

        [HttpPut("{id}/free")]
        public IActionResult FreeRealEstate(int id)
        {
            try
            {
                _realEstateService.FreeRealEsate(id);
                return Ok();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateRealEstate([FromBody] RealEstateUpdateModel realEstate)
        {
            try
            {
                var updatedModel = _realEstateService.Update(realEstate);
                return Ok(updatedModel);
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

    }
}