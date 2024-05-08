using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Application.Exceptions
{
    public class EmailMissmatchException : Exception
    {
        public EmailMissmatchException(string message) : base(message) { }
    }
}
