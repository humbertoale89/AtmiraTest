using Newtonsoft.Json.Linq;

namespace TopNeo.Models
{
    public class NeoDailyLog : objFromJson
    {
        public DateTime day { get; set; }
        public List<Asteroid> asteroids { get; set; }

        public NeoDailyLog(JToken obj)
        {
            asteroids = new List<Asteroid>();
            loadFromJson(obj);
        }

        public override void loadFromJson(JToken obj)
        {
            day = DateTime.Parse(((JProperty)obj).Name);
            var asteroidsJson = obj.First.ToList();
            foreach (var asteroid in asteroidsJson)
            {
                Asteroid currentAsteroid = new Asteroid(asteroid);
                asteroids.Add(currentAsteroid);
            }
        }
    }
}
