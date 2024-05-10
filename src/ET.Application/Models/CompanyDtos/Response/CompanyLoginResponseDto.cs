using ET.Application.Models.UserDtos.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Application.Models.CompanyDtos.Response
{
    public class CompanyLoginResponseDto
    {
        public string JwtToken { get; set; }
        public CompanyResponseDto CompanyResponseDto { get; set; }
        public bool IsSuccess { get; set; } = false;
    }
}
