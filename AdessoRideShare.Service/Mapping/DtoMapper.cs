using System;
using AdessoRideShare.Core.DTOs;
using AdessoRideShare.Core.Entities;
using AutoMapper;

namespace AdessoRideShare.Service.Mapping
{
    public class DtoMapper : Profile
    {
        public DtoMapper()
        {
            CreateMap<TravelPlan, TravelPlanDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
