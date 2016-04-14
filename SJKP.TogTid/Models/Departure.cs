using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SJKP.TogTid.Models
{

    public class DepartureModel
    {
        public Departureboard DepartureBoard { get; set; }
    }

    public class Departureboard
    {
        public string noNamespaceSchemaLocation { get; set; }
        public Departure[] Departure { get; set; }
    }

    public class Departure
    {
        public string name { get; set; }
        public string type { get; set; }
        public string stop { get; set; }
        public string time { get; set; }
        public string date { get; set; }
        public string messages { get; set; }
        public string rtTime { get; set; }
        public string rtDate { get; set; }
        public string finalStop { get; set; }
        public string direction { get; set; }
        public Journeydetailref JourneyDetailRef { get; set; }
        public string rtTrack { get; set; }

        public override string ToString()
        {
            return $"{name} {type} {stop} {date} {time} {direction} {rtTrack ?? String.Empty}";
        }
    }

    public class Journeydetailref
    {
        public string _ref { get; set; }
    }


}