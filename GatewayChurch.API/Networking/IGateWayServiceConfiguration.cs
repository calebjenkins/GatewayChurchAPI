using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GatewayChurch.API.Models;

namespace GatewayChurch.API.Networking
{
    public interface IGateWayServiceConfiguration
    {
        string Years { get; }
        string RecentSermons { get; }
        string Series { get; }
        string Authors { get; }
        string SermonsBySeriesId(string SeriesID);
        string SermonsBySeriesId(string SeriesID, int Count, int Skip);
    }
}
