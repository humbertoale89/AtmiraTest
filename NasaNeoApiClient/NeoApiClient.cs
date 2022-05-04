using RestSharp;

namespace NasaNeoApiClient
{
    public interface INeoApiClient
    {
        Task<string> getFeedRange(string begin);
    }
    public class NeoApiClient : INeoApiClient
    {
        private string urlBase = "https://api.nasa.gov/neo/rest/v1/";
        private string apiKey = "DEMO_KEY";
        public NeoApiClient()
        {

        }
        public async Task<string> getFeedRange(string begin)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>();
            headers.Add("startDate", begin);
            //The API sets a week treshold by default if endDate is not present
            //if (!string.IsNullOrEmpty(end))
            //{
            //    headers.Add("endDate", end);
            //}
            string jsonResponse = await requestApi("feed", headers);
            return jsonResponse;
        }

        private async Task<string> requestApi(string endpoint, Dictionary<string,string> paramSet)
        {
            string paramsStr = "";
            foreach (KeyValuePair<string, string> param in paramSet)
            {
                paramsStr += paramsStr.Length > 0 ? "&" : "" + param.Key + "=" + param.Value;
            }
            var apiClient = new RestClient($"{urlBase}{endpoint}?{paramsStr}&api_key={apiKey}");
            RestResponse apiResponse = await apiClient.ExecuteAsync(new RestRequest());
            return apiResponse.Content;
        }
    }
}