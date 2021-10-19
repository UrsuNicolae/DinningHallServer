using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DinningHall.DTOs;
using DinningHall.Models;

namespace DinningHall.Profiles
{
    public class TableProfile : Profile
    {
        public TableProfile()
        {
            CreateMap<CreateTableDto, Table>()
                .ForMember(t => t.Order, m => m.MapFrom(src => src.Order));
            CreateMap<Table, TableDto>()
                .ForMember(t => t.Order, m => m.MapFrom(src => src.Order)); ;
        }
    }
}
