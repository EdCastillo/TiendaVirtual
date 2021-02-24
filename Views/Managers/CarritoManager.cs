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
    public class CarritoManager
    {
        const string URL = "http://localhost:51221/api/PCAR/";
        const string UrlIngresar = "http://localhost:51221/api/pcar/ingresar/";
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
        public async Task<IEnumerable<Carrito>> ObtenerProductos(string token,string id)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL);
            return JsonConvert.DeserializeObject<IEnumerable<Carrito>>(resultado);
        }

        //Cambiar hacia traer lista de carrito
        public async Task<Carrito> ObtenerCarrito(string codigo)
        {
            HttpClient client = GetClientNO();
            string resultado = await client.GetStringAsync(URL + codigo);
            return JsonConvert.DeserializeObject<Carrito>(resultado);
        }
        public async Task<Carrito> Ingresar(Producto_Carrito producto, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PostAsync(UrlIngresar,
                new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Carrito>(await response.Content.ReadAsStringAsync());
        }
        public async Task<Carrito> Actualizar(Carrito carrito, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PutAsync(URL,
                new StringContent(JsonConvert.SerializeObject(carrito), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Carrito>(await response.Content.ReadAsStringAsync());
        }
        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.DeleteAsync(URL + codigo);
            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }

    }
}