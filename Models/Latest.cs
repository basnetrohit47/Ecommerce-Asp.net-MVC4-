using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityHomeNpl.Models
{
    public class Latest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string description { get; set; }
        public int? price { get; set; }
        public string image { get; set; }
    }
}