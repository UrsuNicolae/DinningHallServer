using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DinningHall.DTOs;
using DinningHall.Models;

namespace DinningHall.Profiles
{
    public class WaiterProfile : Profile
    {
        public WaiterProfile()
        {
            CreateMap<CreateWaiterDto, Waiter>();
            CreateMap<Waiter, GetWaiterDto>();
        }
    }
}
