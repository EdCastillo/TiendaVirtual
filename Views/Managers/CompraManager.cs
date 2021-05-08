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
    public class CompraManager
    {
        public const string URL= Utilities.API_URL+"api/Compra/";
        const string URLgetByID = Utilities.API_URL+"api/compra/COM_ID?id=";
        const string UrlIngresar = Utilities.API_URL+"api/compra/ingresar/";

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

        public async Task<IEnumerable<Compra>> getAllCompras(string id,string token) {
            HttpClient client = GetClient(token);
            string result=await client.GetStringAsync(URL+id);
            return JsonConvert.DeserializeObject<IEnumerable<Compra>>(result);
        }
        public async Task InsertarAuth(Compra compra,string token) {
            HttpClient client = GetClient(token);
            await client.PostAsync(UrlIngresar, new StringContent(JsonConvert.SerializeObject(compra), Encoding.UTF8, "application/json"));
        }
        public async Task InsertarNoAuth(Compra compra, string token)
        {
            HttpClient client = GetClient(token);
            await client.PostAsync(UrlIngresar, new StringContent(JsonConvert.SerializeObject(compra), Encoding.UTF8, "application/json"));
        }
        public async Task<Compra> GetCompra(string id, string token) {
            HttpClient client = GetClient(token);
            string result = await client.GetStringAsync(URLgetByID+id);
            return JsonConvert.DeserializeObject<Compra>(result);
        }



    }
}