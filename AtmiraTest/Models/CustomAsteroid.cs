using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.Json.Serialization;
using TopNeo.Models;

namespace AtmiraTest.Models
{
    public class CustomAsteroid
    {
        [JsonPropertyName("nombre")]
        public string name { get; set; }
        [JsonPropertyName("diametro")]
        public double diameter { get; set; }
        [JsonPropertyName("velocidad")]
        public double velocity { get; set; }
        public DateTime dtDate { get; set; }
        [JsonPropertyName("fecha")]
        public string date => dtDate.ToString("yyyy-MM-dd");
        [JsonPropertyName("planeta")]
        public string planet { get; set; }

        public CustomAsteroid(string name, double diameter, double velocity, DateTime dtDate, string planet)
        {
            this.name = name;
            this.diameter = diameter;
            this.velocity = velocity;
            this.dtDate = dtDate;
            this.planet = planet;
        }
    }
}
