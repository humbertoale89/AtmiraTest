using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NasaNeoApiClient;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace AtmiraTest.Controllers
{
    [Route("asteroids")]
    [ApiController]
    public class AsteroidsController : ControllerBase
    {
        private readonly NeoApiClient _nasaNeoApiClient;
        public AsteroidsController(INeoApiClient apiClient)
        {
            _nasaNeoApiClient = (NeoApiClient) apiClient;
        }
        [HttpGet("{planet}")]
        public async Task<IActionResult> Asteroids([FromRoute] string planet)
        {
            var startDate = DateTime.Now.ToString("yyyy-MM-dd");
            string jsonObj = await _nasaNeoApiClient.getFeedRange(startDate);
            //var client = new RestClient($"https://api.nasa.gov/neo/rest/v1/feed?start_date={startDate}&api_key=DEMO_KEY");
            //RestResponse response = await client.ExecuteAsync(new RestRequest());
            //string jsonObj = response.Content;
            var deserializedObj = JObject.Parse(jsonObj);
            return Ok();
        }
    }
}