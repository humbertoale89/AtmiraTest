using Newtonsoft.Json.Linq;

namespace TopNeo.Models
{
    public class NeoRequestObj : objFromJson
    {
        public NeoReqNavigator navigator { get; set; }
        public int elementCount { get; set; }
        public List<NeoDailyLog> DailyLogs { get; set; }

        public NeoRequestObj(JObject obj)
        {
            navigator = new NeoReqNavigator(obj["links"]);
            DailyLogs = new List<NeoDailyLog>();
            loadFromJson(obj);
        }

        public override void loadFromJson(JToken obj)
        {
            navigator = new NeoReqNavigator(obj.SelectToken("links"));
            elementCount = (int)obj.SelectToken("element_count");
            JToken neoData = ((JObject)obj).Descendants()
                .Where(x => x.Type == JTokenType.Property && ((JProperty)x).Name == "near_earth_objects")
                .Select(y => ((JProperty)y).Value)
                .FirstOrDefault();
            foreach (JToken day in neoData)
            {
                NeoDailyLog currentDay = new NeoDailyLog(day);
                currentDay.day = DateTime.Parse(day.Path.Split(".").Last());
                DailyLogs.Add(currentDay);
            }
        }
    }
}
