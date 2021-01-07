using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ViewsTiendaVirtual.Models;

namespace ViewsTiendaVirtual.Controllers
{
    public class ProductoManager
    {
        const string URL = "http://localhost:51221/api/Producto/";
        const string UrlIngresar = "http://localhost:51221/api/producto/ingresar/";
        HttpClient GetClient(string token)
        {
            HttpClient client = new HttpClient();

            client.DefaultRequestHeaders.Add("Authorization", token);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }
        public async Task<IEnumerable<Producto>> ObtenerProductos(string token)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL);
            return JsonConvert.DeserializeObject<IEnumerable<Producto>>(resultado);
        }

        public async Task<IEnumerable<Producto>> ObtenerProducto(string token, string codigo)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL + codigo);
            return JsonConvert.DeserializeObject<IEnumerable<Producto>>(resultado);
        }
        public async Task<Producto> Ingresar(Producto producto, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PostAsync(UrlIngresar,
                new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Producto>(await response.Content.ReadAsStringAsync());
        }
        public async Task<Producto> Actualizar(Producto producto, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PutAsync(URL,
                new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Producto>(await response.Content.ReadAsStringAsync());
        }
        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.DeleteAsync(URL + codigo);
            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }
    }
}