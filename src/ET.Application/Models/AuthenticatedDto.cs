using ET.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Application.Models
{
    public class AuthenticatedDto
    {
        public Guid Id { get; set; }
        public UserRole Role { get; set; }
        public string JwtToken { get; set; }
        public bool IsAuthenticated { get; set; } = false;
    }
}
