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

                query.Add("boundingBox", $"{req.BoundingBox.ul_lat},{req.BoundingBox.ul_lng},{req.BoundingBox.lr_lat},{req.BoundingBox.lr_lng}");
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
                throw new Exception($"Failed to get Map: {e}");
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
}
