using EquityX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration.Json;
using System.Text.Json;

namespace EquityX.Services
{
    public class WebDataManager
    {
        private string API_KEY = "2mCfUvwif57N1dHbgATVU8rMyvTZI6vT9kP6O2U9";

        /// <summary>
        /// Gets all the stock & crypto information relating to defined short names, then stores the data into a local json
        /// </summary>
        public void GetQuote()
        {
            var combinedList = new List<object>();

            var client = new HttpClient();
            var requestStock = new HttpRequestMessage(HttpMethod.Get, "https://yfapi.net/v6/finance/quote?region=US&lang=en&symbols=AAPL,TSLA,GOOGL,MSFT,AMZN,META,NFLX,NVDA,INTC,CRM");
            var requestCrypto = new HttpRequestMessage(HttpMethod.Get, "https://yfapi.net/v6/finance/quote?region=US&lang=en&symbols=LTC-USD,ADA-USD,XLM-USD,XLQ-USD,XFI-USD,XBI-USD,ILV-USD,ABNB-USD,HUSH-USD,BTC-USD");

            requestStock.Headers.Add("X-API-KEY", API_KEY);
            requestCrypto.Headers.Add("X-API-KEY", API_KEY);

            var taskStock = client.SendAsync(requestStock);
            var taskCrypto = client.SendAsync(requestCrypto);

            var responseStock = taskStock.Result;
            var responseCrypto = taskCrypto.Result;

            responseStock.EnsureSuccessStatusCode();
            responseCrypto.EnsureSuccessStatusCode();

            string responseBodyStock = responseStock.Content.ReadAsStringAsync().Result;
            string responseBodyCrypto = responseCrypto.Content.ReadAsStringAsync().Result;

            try
            {
                LogoService logoService = new LogoService();

                Root myDeserializedClassStock = JsonConvert.DeserializeObject<Root>(responseBodyStock);

                foreach (var item in myDeserializedClassStock.quoteResponse.result)
                {
                    string images_url = logoService.GetLogo(item.symbol);

                    var stockJson = new
                    {
                        Type = "Stock",
                        LogoCode = item.symbol,
                        CompanyName = item.shortName,
                        SharePrice = item.regularMarketPrice,
                        GainPercentage = Math.Round(item.regularMarketChangePercent, 2),
                        Image = images_url,
                    };

                    combinedList.Add(stockJson);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }
            try
            {
                Root myDeserializedClassCrypto = JsonConvert.DeserializeObject<Root>(responseBodyCrypto);

                foreach (var item in myDeserializedClassCrypto.quoteResponse.result)
                {
                    var cryptoJson = new
                    {
                        Type = "Crypto",
                        LogoCode = item.symbol,
                        CompanyName = item.shortName,
                        CoinPrice = item.regularMarketPrice,
                        GainPercentage = Math.Round(item.regularMarketChangePercent, 2)
                    };

                    combinedList.Add(cryptoJson);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }

            string combinedJson = JsonConvert.SerializeObject(combinedList);

            string filePath = Path.Combine(FileSystem.AppDataDirectory, "data.json");

            File.WriteAllTextAsync(filePath, combinedJson);
        }

        public string GetLatestStockInfo(string shortCode)
        {
            var combinedList = new List<object>();

            var client = new HttpClient();
            var requestStock = new HttpRequestMessage(HttpMethod.Get, "https://yfapi.net/v6/finance/quote?region=US&lang=en&symbols=" + shortCode);

            requestStock.Headers.Add("X-API-KEY", API_KEY);

            var taskStock = client.SendAsync(requestStock);

            var responseStock = taskStock.Result;

            responseStock.EnsureSuccessStatusCode();

            string responseBodyStock = responseStock.Content.ReadAsStringAsync().Result;

            try
            {
                Root myDeserializedClassStock = JsonConvert.DeserializeObject<Root>(responseBodyStock);

                foreach (var item in myDeserializedClassStock.quoteResponse.result)
                {
                    var stockJson = new
                    {
                        Type = "Stock",
                        LogoCode = item.symbol,
                        CompanyName = item.shortName,
                        SharePrice = item.regularMarketPrice,
                        GainPercentage = Math.Round(item.regularMarketChangePercent, 2)
                    };

                    combinedList.Add(stockJson);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }

            return JsonConvert.SerializeObject(combinedList);
        }

        public string GetLatestCryptoInfo(string shortCode)
        {
            var combinedList = new List<object>();

            var client = new HttpClient();
            var requestStock = new HttpRequestMessage(HttpMethod.Get, "https://yfapi.net/v6/finance/quote?region=US&lang=en&symbols=" + shortCode);

            requestStock.Headers.Add("X-API-KEY", API_KEY);

            var taskStock = client.SendAsync(requestStock);

            var responseStock = taskStock.Result;

            responseStock.EnsureSuccessStatusCode();

            string responseBodyStock = responseStock.Content.ReadAsStringAsync().Result;

            try
            {
                Root myDeserializedClassStock = JsonConvert.DeserializeObject<Root>(responseBodyStock);

                foreach (var item in myDeserializedClassStock.quoteResponse.result)
                {
                    var stockJson = new
                    {
                        Type = "Crypto",
                        LogoCode = item.symbol,
                        CompanyName = item.shortName,
                        SharePrice = item.regularMarketPrice,
                        GainPercentage = Math.Round(item.regularMarketChangePercent, 2)
                    };

                    combinedList.Add(stockJson);
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
            }

            return JsonConvert.SerializeObject(combinedList);
        }
    }
}
