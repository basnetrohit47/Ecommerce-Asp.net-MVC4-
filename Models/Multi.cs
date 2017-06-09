using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CityHomeNpl.Models
{
    public class Multi
    {
        public IEnumerable<Latest> Latest { get; set; }
        public IEnumerable<feature> Feature { get; set; }
        public IEnumerable<Tblcat> Category { get; set; }
        public IEnumerable<Tblproduct> Details { get; set; }
        public IEnumerable<Tblcat> singlecat { get; set; }

    }
}