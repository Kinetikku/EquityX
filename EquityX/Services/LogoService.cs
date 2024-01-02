using EquityX.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquityX.Services
{
    public class LogoService
    {
        private string API_KEY = "/FHzaV5laksOgMwP6ppXxg==UYG7acgP3LwOvOVD";
        public string GetLogo(string shortCode)
        {
            var client = new HttpClient();
            var requestStock = new HttpRequestMessage(HttpMethod.Get, "https://api.api-ninjas.com/v1/logo?ticker=" + shortCode);

            requestStock.Headers.Add("X-API-KEY", API_KEY);

            var taskStock = client.SendAsync(requestStock);

            var responseStock = taskStock.Result;

            responseStock.EnsureSuccessStatusCode();

            string responseBodyStock = responseStock.Content.ReadAsStringAsync().Result;

            try
            {
                var logoResponses = JsonConvert.DeserializeObject<List<LogoResponse>>(responseBodyStock);
                if (logoResponses != null && logoResponses.Any())
                {
                    return logoResponses[0].image;
                }
                else
                {
                    return null;
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine("JSON Error: " + ex.Message);
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("General Error: " + ex.Message);
                return null;
            }
        }
    }
}
