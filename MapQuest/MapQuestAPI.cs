using System;

namespace MapQuest
{
    public partial class MapQuestAPI
    {
        private readonly string _apiKey;
        private static readonly string _baseUrl = "https://www.mapquestapi.com";

        // API's
        public readonly DirectionsAPI Directions;

        public MapQuestAPI(string apiKey)
        {
            _apiKey = apiKey;
            Directions = new DirectionsAPI(_baseUrl, apiKey);
        }
    }
}
