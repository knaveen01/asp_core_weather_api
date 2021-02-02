using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace GeoWeather.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController: ControllerBase
    {
        private IConfiguration configuration;
        public WeatherController(IConfiguration config)
        {
            configuration = config;
        }
        // GET api/weather
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "No IP is found"};
        }

        // GET api/weather/223.237.233.31
        [HttpGet("{id}")]
        public ActionResult<string> Get(string id)
        {
            string weatherApiHost = configuration.GetSection("MyConfig").GetSection("WeatherApiHost").Value;
            string accessKey = configuration.GetSection("MyConfig").GetSection("AccessKey").Value;
            var ApiUrl = weatherApiHost+"access_key="+ accessKey + "&query="+ id;
            var responseString = "";
            var request = (HttpWebRequest)WebRequest.Create(ApiUrl);
            request.Method = "GET";
            request.ContentType = "application/json";

            using (var response1 = request.GetResponse())
            {
                using (var reader = new StreamReader(response1.GetResponseStream()))
                {
                    responseString = reader.ReadToEnd();
                }
            }
            return responseString;
        }
    }
}
