using API.BusinessLogic.Services;
using API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("api/v1/Admin/users/type")]
    public class AdminOwnerTypeController : ControllerBase
    {
        private readonly IOwnerTypeService _ownerTypeService;

        public AdminOwnerTypeController(IOwnerTypeService ownerTypeService)
        {
            _ownerTypeService = ownerTypeService;
        }

        [HttpPost]
        public IActionResult AddOwnerType(OwnerTypeInsertModel ownerType)
        {
            try
            {
                var result = _ownerTypeService.Create(ownerType);

                return CreatedAtAction(nameof(GetOwnerType), new { id = result.Id }, result);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOwnerType(int id)
        {
            var result = _ownerTypeService.DeleteById(id);

            if (result)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpGet]
        public IActionResult GetOwnerTypes()
        {
            var result = _ownerTypeService.GetAll();

            if (!result.Any())
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetOwnerType(int id)
        {
            var result = _ownerTypeService.GetById(id);

            if (result is null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult UpdateOwnerType(OwnerType ownerType)
        {
            try
            {
                var result = _ownerTypeService.Update(ownerType);

                return Ok(result);
            }
            catch (ArgumentException e)
            {
                return NotFound(e.Message);
            }
        }
    }
}