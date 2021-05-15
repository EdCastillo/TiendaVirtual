using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Views.Models;

namespace Views.Managers
{
    public class PCManager
    {
        const string URL = Utilities.API_URL+"api/PC/";
        const string URLInsertar = Utilities.API_URL + "api/pc/insertar";

        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }
        public async Task Insertar(Producto_Compra producto_Compra,string token) {
            HttpClient client = GetClient(token);
            await client.PostAsync(URLInsertar, new StringContent(JsonConvert.SerializeObject(producto_Compra), Encoding.UTF8,"application/json"));
        }
        public async Task<IEnumerable<Producto_Compra>> getAllItemsByCompraID(string id, string token)
        {
            HttpClient client = GetClient(token);
            string result = await client.GetStringAsync(URL + id);
            return JsonConvert.DeserializeObject<IEnumerable<Producto_Compra>>(result);
        }

    }
}