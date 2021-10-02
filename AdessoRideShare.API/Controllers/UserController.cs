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
    public class UserController : CustomBaseController
    {
        private readonly IService<User, UserDto> _userService;

        public UserController(IService<User, UserDto> userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return ActionResultInstance(await _userService.GetAllAsync());
        }
    }
}
