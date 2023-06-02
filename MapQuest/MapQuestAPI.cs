using System;
using MapQuest.StaticMapAPI;
using MapQuest.DirectionsAPI;

namespace MapQuest
{
    public partial class MapQuestAPI
    {
        private readonly string _apiKey;
        private static readonly string _baseUrl = "https://www.mapquestapi.com";

        // API's
        public readonly Directions Directions;
        public readonly StaticMap StaticMap;

        public MapQuestAPI(string apiKey)
        {
            _apiKey = apiKey;
            Directions = new Directions(_baseUrl, apiKey);
            StaticMap = new StaticMap(_baseUrl, apiKey);
        }
    }
}
