using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Text;
using Newtonsoft.Json.Linq;
using TiendaVirtual.UnifiedModels;
using Newtonsoft.Json;

namespace TiendaVirtual.Controllers
{
    [Authorize]
    public class PaypalController : ApiController
    {
        HttpClient GetClient() {
            HttpClient client = new HttpClient();
            return client; 
        }
        private const string CLIENT = "AZbBVowXzTVVhsbtKCA8Sy3qUAjUOnTxQsbu_BerUgPGT9VrKaH--3BjQ83rIqk9K0XbWrwU9yP8b1Wr";
        private const string SECRET = "EEr8rnx3DOwdrUNtUne64zExlDONomB0ek1t9dBQxAQ-D5CXMsfXsjKHtwAiIaL5xg5__a2a4hELK2qd";
        private string BasicAuth = "Basic "+Convert.ToBase64String(ASCIIEncoding.ASCII.GetBytes(CLIENT + ":" + SECRET));
        private static string APIToken = "notValid";
        private const string PayPal_API_URL = "https://api-m.sandbox.paypal.com/";
        private const string Token_URL = "v1/oauth2/token";
        private const string Order_URL = "v2/checkout/orders";


        [HttpPost]
        public async Task<IHttpActionResult> getPayPalRouteWithAmount(int amount) {
            if (APIToken.Equals("notValid")) {
                APIToken =await getToken();
            }
            if (amount == 0)
            {
                return BadRequest();
            }
            else {
                HttpClient client = GetClient();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + APIToken);
                FullBody body = new FullBody();
                body.intent = "CAPTURE";
                body.purchase_units = new List<PurchaseUnit>();
                body.purchase_units.Add(new PurchaseUnit {amount= new UnifiedModels.Amount { currency_code = "USD", value = amount } });
                body.application_context = new ApplicationContext();
                body.application_context.brand_name = "TiendaVirtual";
                body.application_context.landing_page = "NO_PREFERENCE";
                body.application_context.user_action = "PAY_NOW";
                body.application_context.return_url = "http://localhost:52112/Compra/Exitosa";
                body.application_context.cancel_url= "http://localhost:52112/Compra/Fallida";
                var response = await client.PostAsync(PayPal_API_URL+Order_URL, new StringContent(JsonConvert.SerializeObject(body), System.Text.Encoding.UTF8, "application/json"));
                var validator = response.Content.ReadAsStringAsync().Result;
                var json = (JObject)JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                if (tokenValidatorByJsonResponse(validator))
                {
                    return Ok(searchForApproveURL(json));
                }
                else {
                    APIToken = await getToken();
                    return Ok(searchForApproveURL(json));
                }
                
            }

        }
        private string searchForApproveURL(JObject json) {
            var url = json["links"].Children();
            IEnumerable<URLType> types = JsonConvert.DeserializeObject<List<URLType>>(JsonConvert.SerializeObject(url));
            string result = "";
            foreach (var i in types)
            {
                if (i.rel.Equals("approve"))
                {
                    result = i.href;
                    break;
                }
            }
            return result;
        }

        private bool tokenValidatorByJsonResponse(string json) {
            var jObject = (JObject)JsonConvert.DeserializeObject(json);
            try
            {
                if (jObject["error"]==null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch {
                return false;
            }
        }



        private async Task<string> getToken() {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization",BasicAuth);
            var result =await client.PostAsync((PayPal_API_URL + Token_URL), new StringContent("grant_type=client_credentials", Encoding.UTF8));
            var json = (JObject)JsonConvert.DeserializeObject(result.Content.ReadAsStringAsync().Result);
            var token = json["access_token"];
            return token.ToString();
        }
        

        
    }
}
