using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatewayChurch.API
{
    public static class ObjectExtentions
    {
        public static void IfNotNullThenDo(this object value, Action action)
        {
            if (null != value)
            {
                action.Invoke();
            }
        }
        public static bool IsNull(this object value)
        {
            return null == value;
        }
        public static bool IsNotNull(this object value)
        {
            return null != value;
        }

        public static void IfNotNullDispose(this IDisposable value)
        {
            value.IfNotNullThenDo(() => { value.Dispose(); });
        }
    }
}
