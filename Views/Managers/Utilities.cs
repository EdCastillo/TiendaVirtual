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
        private const string LOCAL_API_ROUTE = "http://localhost:51221/";
        private const string HOST_API_ROUTE = "http://eduardoleocr-002-site1.ctempurl.com/";
        public const string API_URL= LOCAL_API_ROUTE;


        public static async Task<TipoDeCambio> GetTipoDeCambio() {
            HttpClient client = new HttpClient();
            string response=await client.GetStringAsync("https://tipodecambio.paginasweb.cr/api");
            return JsonConvert.DeserializeObject<TipoDeCambio>(response);
        }
    }
}