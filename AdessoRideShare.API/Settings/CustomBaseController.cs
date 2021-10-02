using System;
using AdessoRideShare.Shared.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AdessoRideShare.API.Settings
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult ActionResultInstance<T>(Response<T> response) where T : class
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
