using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapQuest.DirectionsAPI
{
    public partial class Directions
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public Directions(string baseUrl, string apiKey)
        {
            _apiKey = apiKey;
            _baseUrl = baseUrl + "/directions/v2/";
        }
    }
}
