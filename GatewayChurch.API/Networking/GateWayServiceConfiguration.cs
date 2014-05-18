using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GatewayChurch.API.Models;

namespace GatewayChurch.API.Networking
{
    public class GateWayServiceConfiguration : IGateWayServiceConfiguration
    {
        private string baseAddress = "http://api.gatewaychurchapp.com/iphone.php/sermons/";
        public string Years { get { return baseAddress + "getYears"; } }
        public string RecentSermons { get { return baseAddress + "sermons?&count=100&offset=0";  } }
        public string Series { get { return baseAddress + "sections"; } }
        public string Authors { get { return baseAddress + "authors"; } }
        
        public string SermonsBySeriesId (string SeriesID)
        {
            return SermonsBySeriesId(SeriesID, 100, 0);
        }
        public string SermonsBySeriesId (string SeriesID, int Count, int Skip)
        {
            return String.Format("{0}section/{1}&count={2}&offset={3}", baseAddress, SeriesID, Count, Skip);
        }
    }
}
