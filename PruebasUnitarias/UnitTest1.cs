using NUnit.Framework;
using TiendaVirtual.Controllers;
using System.Net.Http;
using System.Threading.Tasks;
using Views.Managers;
using Views.Models;
using System;
using System.Text;

namespace PruebasUnitarias
{
    public class Tests
    {
        private const string CLIENT = "AZbBVowXzTVVhsbtKCA8Sy3qUAjUOnTxQsbu_BerUgPGT9VrKaH--3BjQ83rIqk9K0XbWrwU9yP8b1Wr";
        private const string SECRET = "EEr8rnx3DOwdrUNtUne64zExlDONomB0ek1t9dBQxAQ-D5CXMsfXsjKHtwAiIaL5xg5__a2a4hELK2qd";
        private string BasicAuth = "Basic " + Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(CLIENT + ":" + SECRET));
        private const string PayPal_API_URL = "https://api-m.sandbox.paypal.com/";
        private const string Token_URL= "v1/oauth2/token";

        [Test]
        public async Task getToken() {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", "Basic QVpiQlZvd1h6VFZWaHNidEtDQThTeTNxVUFqVU9uVHhRc2J1X0JlclVnUEdUOVZyS2FILS0zQmpRODNySXFrOUswWGJXcndVOXlQOGIxV3I6RUVyOHJueDNET3dkclVOdFVuZTY0ekV4bERPTm9tQjBlazF0OWRCUXhBUS1ENUNYTXNmWHNqS0h0d0FpSWFMNXhnNV9fYTJhNGhFTEsycWQ=");
            client.DefaultRequestHeaders.Add("Host", "api-m.sandbox.paypal.com");
            client.DefaultRequestHeaders.Add("Content-Type", "application/x-www-form-urlencoded");
            client.DefaultRequestHeaders.Add("Content-Length", "29");
            var result=await client.PostAsync((PayPal_API_URL + Token_URL),new StringContent("grant_type=client_credentials", Encoding.UTF8));
        }
    }
}