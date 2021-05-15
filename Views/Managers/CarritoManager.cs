using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Views.Models;

namespace Views.Managers
{
    public class CarritoManager
    {

        const string URL = Utilities.API_URL+"api/PCAR/";
        const string UrlIngresar = Utilities.API_URL+"api/pcar/insertar/";
        const string DeleteAllByUserIdURL = Utilities.API_URL + "api/pcar/deleteByUser?id=";
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
        
        //Cambiar hacia traer lista de carrito
        public async Task<IEnumerable<Producto_Carrito>> ObtenerCarrito(string codigo,string token)
        {
            HttpClient client = GetClient(token);
            string resultado = await client.GetStringAsync(URL + codigo);
            return JsonConvert.DeserializeObject<IEnumerable<Producto_Carrito>>(resultado);
        }
        public async Task<Producto_Carrito> Ingresar(Producto_Carrito producto, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PostAsync(UrlIngresar,new StringContent(JsonConvert.SerializeObject(producto), Encoding.UTF8,"application/json"));
            return JsonConvert.DeserializeObject<Producto_Carrito>(await response.Content.ReadAsStringAsync());
        }
        public async Task<Carrito> Actualizar(Carrito carrito, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.PutAsync(URL,
                new StringContent(JsonConvert.SerializeObject(carrito), Encoding.UTF8,
                "application/json"));
            return JsonConvert.DeserializeObject<Carrito>(await response.Content.ReadAsStringAsync());
        }
        public async Task DeleteAllByUserId(string id,string token) {
            HttpClient client = GetClient(token);
            await client.DeleteAsync(DeleteAllByUserIdURL + id);
        }


        public async Task<string> Eliminar(string codigo, string token)
        {
            HttpClient client = GetClient(token);
            var response = await client.DeleteAsync(URL + codigo);
            return JsonConvert.DeserializeObject<string>(await response.Content.ReadAsStringAsync());
        }

    }
}