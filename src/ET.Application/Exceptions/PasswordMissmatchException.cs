using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Application.Exceptions
{
    public class PasswordMissmatchException: Exception
    {
        public PasswordMissmatchException(string message) : base(message) { }
    }
}
