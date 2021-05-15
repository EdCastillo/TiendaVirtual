using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using Views.Models;

namespace Views.Managers
{
    public class Utilities
    {
        public const string API_URL= "http://localhost:51221/";
        public static async Task<TipoDeCambio> GetTipoDeCambio() {
            HttpClient client = new HttpClient();
            string response=await client.GetStringAsync("https://tipodecambio.paginasweb.cr/api");
            return JsonConvert.DeserializeObject<TipoDeCambio>(response);
        }
    }
}