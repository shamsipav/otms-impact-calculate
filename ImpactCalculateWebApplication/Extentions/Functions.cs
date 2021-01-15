using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImpactCalculateWebApplication.Extentions
{
    public class Functions
    {
        public static string ZeroDigitToEmpty(int number)
        {
            if (number == 0)
            {
                return "";
            }

            return number.ToString();
        }
    }
}
