﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Views.Managers
{
    public class PayPalManager
    {
        const string URL = "http://localhost:51221/api/Paypal?amount=";
        HttpClient GetClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Accept", "application/json");
            return client;
        }

        public async Task<string> GetRouteByAmount(string amount) {
            HttpClient client = GetClient();
            var response=await client.PostAsync(URL + amount,null);
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}