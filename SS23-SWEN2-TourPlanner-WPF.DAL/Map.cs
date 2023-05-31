using SS23_SWEN2_TourPlanner_WPF.Models;
using System.IO;
using System.Net.Http;
using System;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using System.Globalization;
using System.Configuration;

namespace SS23_SWEN2_TourPlanner.DAL;

public class Map
{
    private Tour tour;
    //ToDo load from config
    private string key = ConfigurationManager.ConnectionStrings["MapQuestApiKey"].ConnectionString;

    public Map(Tour tour)
    {
        this.tour = tour;
    }

    public async Task<Tour> CreateMap()
    {
        var path = System.IO.Path.Join(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "TourPlanner", "Maps");
        System.IO.Directory.CreateDirectory(path);


        tour.Image = System.IO.Path.Join(path, $"{Guid.NewGuid().ToString()}.png");

        string url = $"https://www.mapquestapi.com/directions/v2/route?key={key}&from={tour.From}&to={tour.To}&unit=k&traffic=false&routeType={translateTransportType(tour)}";
        using var client = new HttpClient();
        var response = await client.GetAsync(url);
        var content = await response.Content.ReadAsStringAsync();
        var rootNode = JsonNode.Parse(content);

        var statusCode = rootNode["info"]["statuscode"];

        if(statusCode == null || statusCode.ToString() != "0")
        {
            throw new Exception("Keine Map :(");
        }

        var sessionId = rootNode["route"]["sessionId"].ToString();
        var boundingBox = rootNode["route"]["boundingBox"];
        var ul_lat = boundingBox["ul"]["lat"].ToString();
        var ul_lng = boundingBox["ul"]["lng"].ToString();
        var lr_lat = boundingBox["lr"]["lat"].ToString();
        var lr_lng = boundingBox["lr"]["lng"].ToString();

        tour.Time = TimeSpan.FromSeconds((long)(rootNode["route"]["time"]));
        tour.Distance = Double.Parse(rootNode["route"]["distance"].ToString(), CultureInfo.InvariantCulture);

        url = $"https://www.mapquestapi.com/staticmap/v5/map?key={key}&boundingBox={ul_lat},{ul_lng},{lr_lat},{lr_lng}&size=800,600&session={sessionId}";

        var stream = await client.GetStreamAsync(url);
        await using var fileStream = new FileStream($"{tour.Image}", FileMode.Create, FileAccess.Write);
        stream.CopyTo(fileStream);

        return tour;
    }

    private string translateTransportType(Tour tour)
    {
        switch (tour.TransportType)
        {
            case TourTransportType.Auto:
                return "fastest"; 
            case TourTransportType.Bicycle:
                return "bicycle";
            case TourTransportType.Walking:
                return "pedestrian";
            default:
                return "fastest";
        }
    }
}