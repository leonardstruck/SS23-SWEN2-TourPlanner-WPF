using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MapQuest.DirectionsAPI
{
    public partial class Directions
    {
        public Uri GetUri(GetUriReq req)
        {
            var baseUrl = "https://www.mapquest.com/directions?";
            var query = HttpUtility.ParseQueryString(string.Empty);

            query.Add("saddr", $"{req.StartLat},{req.StartLon} ({req.StartLabel})");
            query.Add("daddr", $"{req.DestLat},{req.DestLon} ({req.DestLabel})");

            return new Uri(baseUrl + query);
        }

        public struct GetUriReq
        {
            public string StartLabel;
            public float StartLat;
            public float StartLon;
            public string DestLabel;
            public float DestLat;
            public float DestLon;
        }
    }
}
