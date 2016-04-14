using Newtonsoft.Json;
using SJKP.TogTid.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace SJKP.TogTid
{
    public class RejseplanApi
    {
        private const string baseUrl = "http://xmlopen.rejseplanen.dk/bin/rest.exe";
        HttpClient client;
        public RejseplanApi()
        {
            this.client = new HttpClient();
        }

        public async Task<List<Stoplocation>> GetLocation(string input)
        {
            

            var res = await client.GetStringAsync(baseUrl + $"/location?input={input}&format=json");

            var model = JsonConvert.DeserializeObject<LocationModel>(res);

            return model.LocationList.StopLocation.ToList();

        }

        public async Task<List<Departure>> GetDepartures(int locationId, DateTime? date = null)
        {
            var url = $"/departureBoard?id={locationId}&format=json";
            if (date != null)
                url += $"&date={date.Value.ToString("dd.MM.yyyy")}&time={date.Value.ToString("HH.mm")}";
            

            var res = await client.GetStringAsync(baseUrl + url);

            var model = JsonConvert.DeserializeObject<DepartureModel>(res);

            return model.DepartureBoard.Departure.ToList();
        }
    }
}