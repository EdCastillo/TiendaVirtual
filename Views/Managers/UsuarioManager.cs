using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Views.Models;

namespace Views.Managers
{
    public class UsuarioManager
    {
        const string UrlAuthenticate = Utilities.API_URL + "api/login/authenticate/";
        const string UrlRegister = Utilities.API_URL + "api/login/ingresar/";

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