using GatewayChurch.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GatewayChurch.API.Networking
{
    public interface IGatewayService
    {
        void GetAuthors(Action<ICollection<Speaker>, Exception>CallBack);
        void GetYears(Action<ICollection<string>, Exception> CallBack);
        void GetSeries(Action<ICollection<Series>, Exception> CallBack);
        void GetLastestSermons(Action<ICollection<Sermon>, Exception> CallBack);
        void GetSermonBySeries(string seriesID, Action<ICollection<Sermon>, Exception> CallBack);
    }
}
