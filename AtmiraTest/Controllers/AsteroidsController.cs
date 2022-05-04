using Microsoft.AspNetCore.Mvc;
using TopNeo.Models;
using TopNeo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using AtmiraTest.Models;
using System.Text.Json;

namespace AtmiraTest.Controllers
{
    [Route("asteroids")]
    [ApiController]
    public class AsteroidsController : ControllerBase
    {
        private readonly ITopNeo _topNeo ;
        public AsteroidsController(ITopNeo topNeo)
        {
            _topNeo = (TopNeoFromRange) topNeo;
        }
        [HttpGet("{planet}")]
        public async Task<string> Asteroids([FromRoute] string planet)
        {
            List<Asteroid> topList = await _topNeo.TopByPlanet(planet);
            string topJson = JsonSerializer.Serialize( topList.Select(ca => new CustomAsteroid(ca.Name, ca.EstimatedDiameterKm, ca.CadHistory.FirstOrDefault().CadRelativeVelocityKmH, ca.CadHistory.FirstOrDefault().CadDate, ca.CadHistory.FirstOrDefault().CadOrbitingBody)) );
            return topJson;
        }
    }
}