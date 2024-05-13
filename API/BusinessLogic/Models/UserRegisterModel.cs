using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;

namespace API.Models
{
    public class UserRegisterModel : UserLoginModel
    {
        public string Email { get; set; } = string.Empty;
    }
}