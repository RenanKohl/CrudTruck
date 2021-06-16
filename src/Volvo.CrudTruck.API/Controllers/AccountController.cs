using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volvo.CrudTruck.Application.Interfaces;
using Volvo.CrudTruck.Application.Models;

namespace Volvo.CrudTruck.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// User login endpoint
        /// </summary>
        /// <returns></returns>        
        [HttpPost("Login")]
        [ProducesResponseType(typeof(BaseModel<UserModel.Response>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseModel<UserModel.Response>), StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Login([FromBody]UserModel.Request request)
        {
            return Ok(await _userService.Login(request));
        }
    }
}
