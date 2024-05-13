using API.BusinessLogic.Services;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Admin
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/v1/RealEstate/type")]
    public class RealEstateTypeController : ControllerBase
    {
        private readonly IRealEstateTypeService _realEstateTypeService;

        public RealEstateTypeController(IRealEstateTypeService realEstateTypeService)
        {
            _realEstateTypeService = realEstateTypeService;
        }

        [HttpPost]
        public IActionResult CreateRealEstateType(RealEstateTypeInsertModel realEstateType)
        {
            if (ModelState.IsValid && realEstateType is not null)
            {
                try
                {
                    var result = _realEstateTypeService.Create(realEstateType);
                    return CreatedAtAction(nameof(CreateRealEstateType), result);
                }
                catch
                {
                    return BadRequest();
                }
            }

            return BadRequest();
        }

        [HttpGet]
        public IActionResult GetAllRealEstateTypes()
        {
            var result = _realEstateTypeService.GetAll();
            if (!result.Any())
            {
                return NotFound();
            }
            return Ok(result);
        }

    }
}