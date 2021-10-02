using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdessoRideShare.Core.DTOs;
using AdessoRideShare.Shared.DTOs;

namespace AdessoRideShare.Core.Services
{
    public interface ITravelService
    {
        Task<Response<TravelPlanDto>> AddTravelPlanAsync(TravelPlanDto entity);
        Task<Response<TravelPlanDto>> PublishTravelPlan(Guid id);
        Task<Response<TravelPlanDto>> UnpublishTravelPlan(Guid id);
        Task<Response<IEnumerable<TravelPlanDto>>> SearchTravelPlan(int from,int where);
        Task<Response<IEnumerable<TravelPlanDto>>> SearchTravelPlanWithConnectedCity(int from,int where);
        Task<Response<TravelPlanDto>> JoinTravelPlan(Guid id);
        Task<Response<IEnumerable<TravelPlanDto>>> GetAllTravelPlan();
    }
}
