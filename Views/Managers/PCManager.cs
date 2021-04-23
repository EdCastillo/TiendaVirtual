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
    public class PCManager
    {
        const string URL = "http://localhost:51221/api/PC/";

        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }
        HttpClient GetClientNO()
        {
            HttpClient client = new HttpClient();
            return client;
        }
        public async Task<IEnumerable<Producto_Compra>> getAllItemsByCompraID(string id, string token)
        {
            HttpClient client = GetClient(token);
            string result = await client.GetStringAsync(URL + id);
            return JsonConvert.DeserializeObject<IEnumerable<Producto_Compra>>(result);
        }

    }
}