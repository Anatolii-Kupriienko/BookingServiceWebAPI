using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UsersController : ControllerBase
    {
        public UsersController()
        {
        }

        [HttpGet("users")]
        public ActionResult<IEnumerable<string>> GetUsers()
        {
            return new string[] { "user1", "user2" };
        }
    }
}