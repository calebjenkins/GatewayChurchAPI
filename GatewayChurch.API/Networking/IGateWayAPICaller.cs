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
    public interface IGateWayAPICaller
    {
        void CallNetwork(string URL, Action<string, Exception> CallBack);
    }
}
