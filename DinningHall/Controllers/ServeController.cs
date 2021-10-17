using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using Dinning_Hall.Domain;
using Microsoft.AspNetCore.Html;
using Newtonsoft.Json;

namespace Dinning_Hall.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ServeController : ControllerBase
    {
        private static readonly HttpClient _client = new HttpClient();
        private static readonly Waiter[] waiters = new Waiter[]
        {
            new Waiter(), new Waiter()
        };
        private readonly ILogger<ServeController> _logger;

        private IEnumerable<Table> tables;

        public ServeController(ILogger<ServeController>logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task GetTables()
        {
            var response = await _client.GetAsync("https://localhost:5001/api/Serve/Tables");
            var nrOfTables = Convert.ToInt32(await response.Content.ReadAsStringAsync());

            var tbs = new List<Table>();
            for (int i = 0; i < nrOfTables; i++)
            {
                tbs.Add(new Table());
            }
            tables = tbs;
            _logger.LogInformation($"Nr of table received {nrOfTables}");
            SendOrder();
        }

        [HttpPost]
        public void SendOrder()
        {
            while (true)
            {
                if (waiters.Any(w => w.IsFree))
                {
                    var waiter = waiters.FirstOrDefault(w => w.IsFree);
                    var tb = waiter?.FindTableToServe(tables);
                    var request = new HttpRequestMessage()
                    {
                        RequestUri = new Uri("https://localhost:5001/api/Serve/Order"),
                        Method = HttpMethod.Post,
                    };
                    request.Headers.Add("accept", "text/plain");
                    request.Content =
                        new StringContent(
                            "{\r\n  \"foods\": [\r\n    {\r\n      \"name\": \"string\",\r\n      \"preparationTime\": {},\r\n      \"complexity\": 0,\r\n      \"cookingApparatus\": 0\r\n    }\r\n  ]\r\n}", Encoding.UTF8, "application/json");

                    Task.Factory.StartNew(() =>
                        {
                            var response = _client.SendAsync(request).ConfigureAwait(false);
                            _logger.LogInformation(response.GetAwaiter().GetResult().Content.ReadAsStringAsync().Result);
                        });
                }
            }

        }
    }
}
