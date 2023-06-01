using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapQuest.DirectionsAPI
{
    public partial class Directions
    {
        internal static void HandleGeneralStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case 0:
                    // Sucessful route call
                    return;
                case 400:
                    throw new Exception("Invalid Request");
                case 401:
                    throw new Exception("Invalid Options");
                case 402:
                    throw new Exception("Invalid Location");
                case 403:
                    throw new Exception("Invalid API-Key");
                case 500:
                    throw new Exception("Unknown error");
                case 600:
                    throw new Exception("Invalid session ID");
            }
        }

        internal static void HandleRouteStatusCode(int statusCode)
        {
            HandleGeneralStatusCode(statusCode);

            switch(statusCode)
            {
                case 0:
                    // Sucessful route call
                    return;
                case 601:
                    throw new Exception("Invalid location specified");
                case 602:
                    throw new Exception("Failed generating route. Check if options are being set in a way that makes routing possible");
                case 603:
                    throw new Exception("No dataset found to calculate the route");
                case 610:
                    throw new Exception("Ambiguities were found in one or more of the locations specified");
            }
        }
    }
}
