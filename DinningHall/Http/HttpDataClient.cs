using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using DinningHall.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

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

        public Task SendOrder(OrderDto order)
        {
            var httpContent = new StringContent(
                JsonSerializer.Serialize(order),
                Encoding.UTF8,
                "application/json");
            Console.WriteLine($"--> Send order {order.Id} to kitchen");
            return _httpClient.PostAsync($"{_configuration["KitchenUrl"]}", httpContent);
        }
    }
}
