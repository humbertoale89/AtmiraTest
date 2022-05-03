using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace AtmiraTest.Controllers
{
    [Route("asteroids")]
    [ApiController]
    public class AsteroidsController : ControllerBase
    {
        [HttpGet("{planet}")]
        public async Task<IActionResult> Asteroids([FromRoute] string planet)
        {
            var startDate = DateTime.Now.ToString("yyyy-MM-dd");
            var client = new RestClient($"https://api.nasa.gov/neo/rest/v1/feed?start_date={startDate}&api_key=DEMO_KEY");
            RestResponse response = await client.ExecuteAsync(new RestRequest());
            string jsonObj = response.Content;
            var deserializedObj = JObject.Parse(jsonObj);
            return Ok();            
        }
    }
}