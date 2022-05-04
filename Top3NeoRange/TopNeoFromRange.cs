using NasaNeoApiClient;
using Newtonsoft.Json.Linq;
using TopNeo.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TopNeo
{
    public interface ITopNeo
    {
        Task<List<Asteroid>> TopByPlanet(string planet);
    }
    public class TopNeoFromRange : ITopNeo
    {
        private readonly NeoApiClient _nasaNeoApiClient;
        private NeoRequestObj _requestObj;

        public TopNeoFromRange(INeoApiClient apiClient)
        {
            _nasaNeoApiClient = (NeoApiClient) apiClient;
        }

        public async Task<List<Asteroid>> TopByPlanet(string planet)
        {
            var startDate = DateTime.Now.ToString("yyyy-MM-dd");
            //By default the feed endpoint set a week range if endDate is mising
            NeoRequestObj neoReqObj = new NeoRequestObj(JObject.Parse(await _nasaNeoApiClient.getFeedRange(startDate)));
            IEnumerable<Asteroid> neoAll = GetNeoAll().AsQueryable().OrderByDescending(n => n.EstimatedDiameterKm);
            IEnumerable<Asteroid> neoTopPlanet = neoAll.Where(n => n.IsPotentiallyHazardous == true && n.CadHistory.Any(ac => ac.CadOrbitingBody.ToLower() == planet.ToLower())).OrderByDescending(n=> n.EstimatedDiameterKm);
            if(neoTopPlanet.Count() > 2)
            {
                return neoTopPlanet.Take(3).ToList();
            }
            int missingCount = 3 - neoTopPlanet.Count();
            IEnumerable<Asteroid> neoTop = neoAll.Where(n => n.IsPotentiallyHazardous == true).OrderByDescending(n => n.EstimatedDiameterKm);
            if (neoTop.Count() > missingCount-1)
            {
                return neoTopPlanet.Concat(neoTopPlanet.Take(missingCount)).ToList();
            }
            missingCount = 3 - neoTopPlanet.Count()+neoTop.Count();           
            return neoTopPlanet.Concat(neoTopPlanet.Take(missingCount)).Concat(neoAll.Take(missingCount)).ToList();
        }

        private List<Asteroid> GetNeoAll()
        {
            List<Asteroid> neoAll = new List<Asteroid>();
            if (_requestObj != null) {
                foreach (NeoDailyLog dailyLog in _requestObj.DailyLogs)
                {
                    foreach (Asteroid asteroid in dailyLog.asteroids)
                    {
                        neoAll.Add(asteroid);
                    }
                }
            }
            return neoAll;
        }

    }
}