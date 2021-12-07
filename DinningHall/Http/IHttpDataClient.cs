using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using DinningHall.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DinningHall.Http
{
    public interface IHttpDataClient
    {
        Task<HttpResponseMessage> SendOrder(OrderDto order);
    }
}
