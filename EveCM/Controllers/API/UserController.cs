using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EveCM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EveCM.Controllers.API
{
    [Authorize]
    [Route("api/User")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;

        public UserController(UserManager<ApplicationUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }
        
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            ApplicationUser currentUser = _userManager.GetUserAsync(User).Result;
            IEnumerable<string> roles = _userManager.GetRolesAsync(currentUser).Result;

            ApplicationUserDto userViewData = Mapper.Map<ApplicationUserDto>(currentUser);
            userViewData.Roles = roles;

            return Ok(userViewData);
        }
    }
}