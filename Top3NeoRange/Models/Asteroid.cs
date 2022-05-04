using Newtonsoft.Json.Linq;
using System.ComponentModel.DataAnnotations;

namespace TopNeo.Models
{
    public class Asteroid : objFromJson
    {
        public int Id { get; set; }
        public int NeoReferenceId { get; set; }
        public string Name { get; set; } = "Unnamed";
        [Url]
        public string? NasaNeoUrl { get; set; }
        [Url]
        public string? NasaJplUrl { get; set; }
        public double AbsoluteMagnitudeH { get; set; }

        public double EstimatedDiameterKmMin { get; set; }
        public double EstimatedDiameterKmMax { get; set; }
        public double EstimatedDiameterKm { get { return (EstimatedDiameterKmMin + EstimatedDiameterKmMax) / 2; } }
        public double EstimatedDiameterMtMin { get; set; }
        public double EstimatedDiameterMtMax { get; set; }
        public double EstimatedDiameterMt { get { return (EstimatedDiameterMtMin + EstimatedDiameterMtMax) / 2; } }
        public double EstimatedDiameterMiMin { get; set; }
        public double EstimatedDiameterMiMax { get; set; }
        public double EstimatedDiameterMi { get { return (EstimatedDiameterMiMin + EstimatedDiameterMiMax) / 2; } }
        public double EstimatedDiameterFtMin { get; set; }
        public double EstimatedDiameterFtMax { get; set; }
        public double EstimatedDiameterFt { get { return (EstimatedDiameterFtMin + EstimatedDiameterFtMax) / 2; } }

        public bool IsPotentiallyHazardous { get; set; }
        //Close Approach Data CAD
        public List<AsteroidCad> CadHistory { get; set; }
        public bool IsSentryObject { get; set; }

        public Asteroid(JToken obj)
        {
            CadHistory = new List<AsteroidCad>();
            loadFromJson(obj);
        }

        public override void loadFromJson(JToken obj)
        {
            JToken pivote = obj.First;
            while (pivote != null)
            {
                string pathName = pivote.Path.Split(".").Last();
                switch (pathName)
                {
                    case "links":
                        NasaNeoUrl = pivote.First["self"].ToString();
                        break;
                    case "id":
                        Id = pivote.First.Value<int>();
                        break;
                    case "neo_reference_id":
                        NeoReferenceId = pivote.First.Value<int>();
                        break;
                    case "name":
                        Name = pivote.First.Value<string>();
                        break;
                    case "nasa_jpl_url":
                        NasaJplUrl = pivote.First.Value<string>();
                        break;
                    case "absolute_magnitude_h":
                        AbsoluteMagnitudeH = pivote.First.Value<double>();
                        break;
                    case "estimated_diameter":
                        JObject edObj = pivote.First.Value<JObject>();
                        EstimatedDiameterKmMin = (double)edObj["kilometers"]["estimated_diameter_min"];
                        EstimatedDiameterKmMax = (double)edObj["kilometers"]["estimated_diameter_max"];
                        EstimatedDiameterMtMin = (double)edObj["meters"]["estimated_diameter_min"];
                        EstimatedDiameterMtMax = (double)edObj["meters"]["estimated_diameter_max"];
                        EstimatedDiameterMiMin = (double)edObj["miles"]["estimated_diameter_min"];
                        EstimatedDiameterMiMax = (double)edObj["miles"]["estimated_diameter_max"];
                        EstimatedDiameterFtMin = (double)edObj["feet"]["estimated_diameter_min"];
                        EstimatedDiameterFtMax = (double)edObj["feet"]["estimated_diameter_max"];
                        break;
                    case "is_potentially_hazardous_asteroid":
                        IsPotentiallyHazardous = pivote.First.Value<bool>();
                        break;
                    case "close_approach_data":
                        int cadCount = pivote.First.Count();
                        for (int i = 0; i < cadCount; i++)
                        {
                            AsteroidCad currentcad = new AsteroidCad(pivote.First[0]);
                            CadHistory.Add(currentcad);
                        }
                        break;
                    case "is_sentry_object":
                        IsSentryObject = pivote.First.Value<bool>();
                        break;
                    default:
                        break;
                }
                pivote = pivote.Next;
            }
        }
    }
}
