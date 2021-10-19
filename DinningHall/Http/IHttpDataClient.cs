using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DinningHall.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace DinningHall.Http
{
    public interface IHttpDataClient
    {
        Task SendOrder(OrderDto order);
    }
}
