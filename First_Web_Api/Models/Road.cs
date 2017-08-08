using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace First_Web_Api.Models
{
    public class Road
    {
        public string roadName { get; set; }

        public string account { get; set; }

        public string introduction { get; set; }


    }

    public class ReturnRoad
    {
        public List<Dictionary<string, string>> returnMessage { get; set; }
    }

    public class ReturnFinalRoad
    {
        public string roadID { get; set; }
    }

    public class ReturnImageNameByRoadID
    {
        public List<string> returnImageName { get; set; }
    }
    
}