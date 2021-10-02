using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdessoRideShare.API.Settings;
using AdessoRideShare.Core.DTOs;
using AdessoRideShare.Core.Entities;
using AdessoRideShare.Core.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AdessoRideShare.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TravelController : CustomBaseController
    {
        private readonly ITravelService _travelPlanService;

        public TravelController(ITravelService travelPlanService)
        {
            _travelPlanService = travelPlanService;
        }
        //BOLUM - 1
        [HttpGet("GetAllTravelPlan")]
        public async Task<IActionResult> GetTravelPlans()
        {
            return ActionResultInstance(await _travelPlanService.GetAllTravelPlan());
        }

        [HttpPost("SaveTravelPlan")]
        public async Task<IActionResult> SaveTravelPlan(TravelPlanDto travelPlan)
        {
            return ActionResultInstance(await _travelPlanService.AddTravelPlanAsync(travelPlan));
        }

        [HttpPut("PublishTravelPlan")]
        public async Task<IActionResult> PublishTravelPlan(Guid id)
        {
            return ActionResultInstance(await _travelPlanService.PublishTravelPlan(id));
        }

        [HttpPut("UnPublishTravelPlan")]
        public async Task<IActionResult> UpdateTravelPlan(Guid id)
        {
            return ActionResultInstance(await _travelPlanService.UnpublishTravelPlan(id));
        }

        [HttpPost("JoinTravelPlan")]
        public async Task<IActionResult> JoinTravelPlan(Guid id)
        {
            return ActionResultInstance(await _travelPlanService.JoinTravelPlan(id));
        }

        [HttpGet("GetAllTravelPlanByFromToWhere")]
        public async Task<IActionResult> SearchTravelPlan(int from,int where)
        {
            return ActionResultInstance(await _travelPlanService.SearchTravelPlan(from,where));
        }

        //BOLUM - 2
        [HttpGet("GetAllTravelPlanWithConnectedCity")]
        public async Task<IActionResult> SearchTravelPlanWithConnectedCity(int from, int where)
        {
            return ActionResultInstance(await _travelPlanService.SearchTravelPlanWithConnectedCity(from, where));
        }


    }
}
