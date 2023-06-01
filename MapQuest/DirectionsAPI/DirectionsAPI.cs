using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapQuest
{
    public partial class DirectionsAPI
    {
        private string _apiKey;
        private string _baseUrl;

        public DirectionsAPI(string baseUrl, string apiKey)
        {
            _apiKey = apiKey;
            _baseUrl = baseUrl + "/directions/v2/";
        }
    }
}
