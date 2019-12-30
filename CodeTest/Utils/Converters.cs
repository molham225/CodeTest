using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeTest.Utils
{
    public class Converters
    {
        public static int ToInt(object val) {
            int result = 0;
            if (val != null) {
                int.TryParse(val.ToString(), out result);
            }
            return result;
        }

        public static string ToString(object val)
        {
            string result = "";
            if (val != null)
            {
                result = val.ToString().Trim();
            }
            return result;
        }

    }
}
