using DinningHall.DTOs;
using Microsoft.Extensions.Configuration;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DinningHall.Models;

namespace DinningHall.Http
{
    public class HttpDataClient : IHttpDataClient
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public HttpDataClient(
            HttpClient httpClient,
            IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<HttpResponseMessage> SendOrder(object order, Guid orderId)
        {
            var httpContent = new StringContent(
                JsonConvert.SerializeObject(order, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                }),
                Encoding.UTF8,
                "application/json");
            Console.WriteLine($"--> Send order {orderId} to kitchen");
            var url = $"{_configuration["KitchenUrl"]}";
            return await _httpClient.PostAsync(url, httpContent);
        }
    }
}
