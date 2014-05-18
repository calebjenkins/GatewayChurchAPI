using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GatewayChurch.API.Models
{
    public class Series
    {   
        public string Id { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public int Count { get; set; }
    }
}
