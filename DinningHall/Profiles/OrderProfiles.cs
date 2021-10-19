using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DinningHall.DTOs;
using DinningHall.Models;

namespace DinningHall.Profiles
{
    public class OrderProfiles : Profile
    {
        public OrderProfiles()
        {
            //Source -> Target
            CreateMap<CreateOrderDto, Order>()
                .ForMember(o => o.Foods, m => m.MapFrom(src => src.Foods));
            CreateMap<Order, OrderDto>()
                .ForMember(o => o.Foods, m => m.MapFrom(src => src.Foods));
        }
    }
}
