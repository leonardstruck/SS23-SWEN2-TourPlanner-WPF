using MapQuest.Structs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MapQuest.StaticMapAPI
{
    public partial class StaticMap
    {
        public async Task<Map> GetMap(GetMapReq req)
        {
            try
            {
                // PREPARE REQUEST
                var query = HttpUtility.ParseQueryString(string.Empty);

                query.Add("key", _apiKey);

                query.Add("boundingBox", $"{req.BoundingBox.ul_lat:N5},{req.BoundingBox.ul_lng:N5},{req.BoundingBox.lr_lat:N5},{req.BoundingBox.lr_lng:N5}");
                query.Add("size", $"{req.Width},{req.Height}");

                if (req.SessionId != null)
                    query.Add("session", req.SessionId);

                var url = $"{_baseUrl}/map?{query}";

                // GET IMAGE
                using var client = new HttpClient();
                var stream = await client.GetStreamAsync(url);

                using var ms = new MemoryStream();
                stream.CopyTo(ms);

                var map = new Map(ms.ToArray());

                return map;
            } catch (Exception e)
            {
                // Wrap Exception
                throw new GetMapException($"Failed to get Map: {e.Message}", e);
            }
        }
    }

    public struct GetMapReq
    {
        public BoundingBox BoundingBox;
        public string? SessionId;
        public int Width;
        public int Height;
    }

    public class GetMapException : Exception
    {
        public GetMapException() { }
        public GetMapException(string message) : base(message) { }
        public GetMapException(string message, Exception innerException) : base(message, innerException) { }
    }
}
