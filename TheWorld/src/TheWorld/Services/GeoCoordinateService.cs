using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace TheWorld.Services
{
    public class GeoCoordinateService
    {
        private IConfigurationRoot _config;
        private ILogger<GeoCoordinateService> _logger;

        public GeoCoordinateService(ILogger<GeoCoordinateService> logger,
            IConfigurationRoot config)
        {
            _logger = logger;
            _config = config;
        }

        public async Task<GeoCoordsResult> GetCoordinatesAsync(string name)
        {
            var result = new GeoCoordsResult()
            {
                Success = false,
                Message = "Failed to get Coordinates",
            };           
           
            var apiKey = _config["Keys:Bingkey"];
            var encodeName = WebUtility.UrlEncode(name);
            var url = $"http://dev.virtualearth.net/REST/v1/Locations?q={encodeName}&key={apiKey}";

            var client = new HttpClient();

            var json = await client.GetStringAsync(url);
            json = await client.GetStringAsync(url);

            var results = JObject.Parse(json);
            var resources = results["resourceSets"][0]["resources"];

            if(!results["resourceSets"][0]["resources"].HasValues)
            {
                result.Message = $"Could not find'{name}' as a location";
            }
            else
            {
                var confidence = (string)resources[0]["confidence"];
                if(confidence != "High")
                {
                    result.Message = $"Could not find a confident match for'{name}' as a location";
                }
                else
                {
                    var coords = resources[0]["geocodePoints"][0]["coordinates"];
                    result.Latitude = (double)coords[0];
                    result.Longitude = (double)coords[1];
                    result.Success = true;
                    result.Message = "Success";
                }
            }

            return result;
        }
    }
}
