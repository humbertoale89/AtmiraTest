using Newtonsoft.Json.Linq;

namespace TopNeo.Models
{
    public abstract class objFromJson
    {
        public virtual void loadFromJson(JToken obj) { }
    }
}
