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
    public class UsuarioManager
    {
        const string UrlAuthenticate = "http://localhost:51221/api/login/authenticate/";
        const string UrlRegister = "http://localhost:51221/api/login/ingresar/";

        public async Task<Usuario> Validar(string username, string password)
        {
            LoginRequest loginRequest = new
                LoginRequest()
            { Username = username, Password = password };

            HttpClient httpClient = new HttpClient();

            var response = await
                httpClient.PostAsync(UrlAuthenticate,
                new StringContent(JsonConvert.SerializeObject(loginRequest),
                Encoding.UTF8, "application/json"));

            return
                JsonConvert.DeserializeObject<Usuario>
                (await response.Content.ReadAsStringAsync());
        }
        public async Task<Usuario> Registrar(Usuario usuario)
        {
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.PostAsync(UrlRegister,
                new StringContent(JsonConvert.SerializeObject(usuario),
                Encoding.UTF8, "application/json"));
            return
                JsonConvert.DeserializeObject<Usuario>(
                    await response.Content.ReadAsStringAsync());
        }
    }
}