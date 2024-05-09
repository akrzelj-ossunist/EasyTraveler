using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Application.Models.UserDtos.Response
{
    public class LoginResponseDto
    {
        public string JwtToken { get; set; }
        public UserResponseDto UserResponseDto { get; set; }
    }
}
