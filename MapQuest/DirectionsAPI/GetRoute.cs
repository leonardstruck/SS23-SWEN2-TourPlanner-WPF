using MapQuest.Structs;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Web;

namespace MapQuest.DirectionsAPI
{
    public partial class Directions
    {
        public async Task<GetRouteRes> GetRoute(GetRouteReq req)
        {
            try
            {
                // PREPARE REQUEST
                var query = HttpUtility.ParseQueryString(string.Empty);

                query.Add("key", _apiKey);

                query.Add("from", req.From);
                query.Add("to", req.To);

                query.Add("unit", req.Unit?.ToString() ?? "k");
                query.Add("traffic", req.Traffic?.ToString() ?? "false");
                query.Add("routeType", req.RouteType?.ToString() ?? "fastest");

                var url = $"{_baseUrl}/route?{query}";

                using var client = new HttpClient();
                var response = await client.GetAsync(url);
                var data = await response.Content.ReadAsStringAsync();

                // PARSE RESPONSE
                var rootNode = JsonNode.Parse(data)!;

                // INFO
                var info = rootNode["info"]!;

                var statusCodeNode = info["statuscode"]!;

                var statusCode = int.Parse(statusCodeNode.ToString());

                HandleRouteStatusCode(statusCode);

                GetRouteRes res = new();

                // ROUTE
                var routeNode = rootNode["route"]!;

                // ROUTE -> SESSION ID
                var sessionIdNode = routeNode["sessionId"]!;
                res.SessionId = sessionIdNode.ToString();

                // ROUTE -> BOUNDING BOX
                var boundingBoxNode = routeNode["boundingBox"]!;
                var boundingBoxUlNode = boundingBoxNode["ul"]!;
                var boundingBoxLrNode = boundingBoxNode["lr"]!;

                res.BoundingBox.ul_lat = float.Parse(boundingBoxUlNode["lat"]!.ToString());
                res.BoundingBox.ul_lng = float.Parse(boundingBoxUlNode["lng"]!.ToString());
                res.BoundingBox.lr_lat = float.Parse(boundingBoxLrNode["lat"]!.ToString());
                res.BoundingBox.lr_lng = float.Parse(boundingBoxLrNode["lng"]!.ToString());

                // ROUTE -> TIME
                res.Time = TimeSpan.FromSeconds(long.Parse(routeNode["time"]!.ToString()));

                // ROUTE -> DISTANCE
                res.Distance = double.Parse(routeNode["distance"]!.ToString(), CultureInfo.InvariantCulture);

                // ROUTE -> LOCATIONS
                var locationsNode = routeNode["locations"] as JsonArray ?? throw new Exception("no locations received");

                var from = locationsNode[0]!;
                var to = locationsNode[1]!;

                var fromLatLng = from["latLng"]!;
                var toLatLng = to["latLng"]!;

                res.From = new() { Lat = float.Parse(fromLatLng["lat"]!.ToString()), Lng = float.Parse(fromLatLng["lng"]!.ToString()) };
                res.To = new() { Lat = float.Parse(toLatLng["lat"]!.ToString()), Lng = float.Parse(toLatLng["lng"]!.ToString()) };

                return res;

            } catch (Exception e)
            {
                // Wrap Exception
                throw new GetRouteException($"Failed to generate route: {e.Message}", e);
            }
        }

        public struct GetRouteReq
        {
            public string From;
            public string To;
            public RouteType? RouteType;
            public Unit? Unit;
            public bool? Traffic;
        }

        public struct GetRouteRes
        {
            public string SessionId;
            public BoundingBox BoundingBox;
            public TimeSpan Time;
            public double Distance;
            public LatLng From;
            public LatLng To;
        }

        public enum RouteType
        {
            Fastest,
            Bicycle,
            Pedestrian
        }

        public enum Unit
        {
            K,
            M
        }



        public struct LatLng
        {
            public float Lat;
            public float Lng;
        }
       
    }
    public class GetRouteException : Exception
    {
        public GetRouteException() { }
        public GetRouteException(string message) : base(message) { }
        public GetRouteException(string message, Exception innerException) : base(message, innerException) { }
    }
}
