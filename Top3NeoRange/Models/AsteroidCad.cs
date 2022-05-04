using Newtonsoft.Json.Linq;

namespace TopNeo.Models
{
    public class AsteroidCad : objFromJson
    {
        public DateTime CadDate { get; set; }
        public double CadRelativeVelocityKmS { get; set; }
        public double CadRelativeVelocityKmH { get; set; }
        public double CadRelativeVelocityMiH { get; set; }
        public double CadMissDistanceAs { get; set; }
        public double CadMissDistanceLu { get; set; }
        public double CadMissDistanceKm { get; set; }
        public double CadMissDistanceMi { get; set; }
        public string CadOrbitingBody { get; set; }

        public AsteroidCad(JToken obj)
        {
            loadFromJson(obj);
        }

        public override void loadFromJson(JToken obj)
        {
            CadDate = TimeStampToDateTime((double)obj["epoch_date_close_approach"]);
            CadRelativeVelocityKmS = (double)obj["relative_velocity"]["kilometers_per_second"];
            CadRelativeVelocityKmH = (double)obj["relative_velocity"]["kilometers_per_hour"];
            CadRelativeVelocityMiH = (double)obj["relative_velocity"]["miles_per_hour"];
            CadMissDistanceAs = (double)obj["miss_distance"]["astronomical"];
            CadMissDistanceLu = (double)obj["miss_distance"]["lunar"];
            CadMissDistanceKm = (double)obj["miss_distance"]["kilometers"];
            CadMissDistanceMi = (double)obj["miss_distance"]["miles"];
            CadOrbitingBody = (string)obj["orbiting_body"];
        }

        private static DateTime TimeStampToDateTime(double TimeStamp)
        {
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddMilliseconds(TimeStamp).ToLocalTime();
            return dtDateTime;
        }
    }
}
