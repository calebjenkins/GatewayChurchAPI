using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GatewayChurch.API.Models;
using System.Net;
using System.IO;
using System.Threading;

namespace GatewayChurch.API.Networking
{
    public class CallBackWrapper
    {
        private CallBackWrapper() {}

        public CallBackWrapper(Action<string, Exception> CallBack)
        {
            this.CallBack = CallBack;
        }
        public Action<string, Exception> CallBack { get; set; }
        public HttpWebRequest WebRequest { get; set; }
    }
}
