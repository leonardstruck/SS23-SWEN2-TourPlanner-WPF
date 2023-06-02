using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapQuest.StaticMapAPI
{
    public partial class StaticMap
    {
        private readonly string _apiKey;
        private readonly string _baseUrl;

        public StaticMap(string baseUrl, string apiKey)
        {
            _apiKey = apiKey;
            _baseUrl = baseUrl + "/staticmap/v5/";
        }
    }
}
