using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ET.Application.Utilities
{
    public class ValidatePageableParams
    {
        public int Validate(string num, int defaultValue)
        {
            if(string.IsNullOrEmpty(num)) return defaultValue;

            int result;
            bool isValid = int.TryParse(num, out result);

            // If parsing fails or the result is not positive, return the default value
            if (!isValid || result < 0)
            {
                return defaultValue;
            }

            return result;
        }
    }
}
