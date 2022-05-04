using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace TopNeo.Models
{
    public class NeoReqNavigator : objFromJson
    {
        [Url]
        public string linkSelf { get; set; }
        [Url]
        public string linkNext { get; set; }
        [Url]
        public string linkPrev { get; set; }

        public NeoReqNavigator(JToken obj)
        {
            loadFromJson(obj);
        }

        public override void loadFromJson(JToken obj)
        {
            this.linkPrev = (string)obj["prev"];
            this.linkSelf = (string)obj["self"];
            this.linkNext = (string)obj["next"];
        }
    }
}
