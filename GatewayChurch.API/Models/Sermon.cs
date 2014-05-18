using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GatewayChurch.API.Models
{
    public class Sermon
    {
        public string Id { get; set; }
        public string AudioPath { get; set; }
        public int AudioLength { get; set; }
        public string Author { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public string ImageId { get; set; }
        public string Title { get; set; }
        public string VideoPath { get; set; }
        public int VideoLength { get; set; }
    }
}
