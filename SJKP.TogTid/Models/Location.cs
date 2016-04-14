using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SJKP.TogTid.Models
{    
    public class LocationModel
    {
        public Locationlist LocationList { get; set; }
    }

    public class Locationlist
    {
        public string noNamespaceSchemaLocation { get; set; }
        public Stoplocation[] StopLocation { get; set; }
        public Coordlocation[] CoordLocation { get; set; }
    }

    public class Stoplocation
    {
        public string name { get; set; }
        public string x { get; set; }
        public string y { get; set; }
        public string id { get; set; }
    }

    public class Coordlocation
    {
        public string name { get; set; }
        public string x { get; set; }
        public string y { get; set; }
        public string type { get; set; }
    }

}