using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatewayChurch.API
{
    public static class StringExtensions
    {
        public static bool IsNullOrEmpty(this string value)
        {
            return string.IsNullOrEmpty(value);
        }

        public static byte[] ConvertToAscii(this string value)
        {
            byte[] retval = new byte[value.Length];
            for (int ix = 0; ix < value.Length; ++ix)
            {
                char ch = value[ix];
                if (ch <= 0x7f) retval[ix] = (byte)ch;
                else retval[ix] = (byte)'?';
            }
            return retval;
        }
    }
}
