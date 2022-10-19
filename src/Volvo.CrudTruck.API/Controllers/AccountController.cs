using FirebaseAdmin;
using FirebaseAdmin.Messaging;
using Google.Apis.Auth.OAuth2;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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


        /// <summary>
        /// Notify user login endpoint
        /// </summary>
        /// <returns></returns>        
        [HttpPost("notify")]
        
        public async Task<IActionResult> SendNotify([FromBody] string token)
        {
          
            var message = new Message()
            {
                Data = new Dictionary<string, string>()
                {
                    ["FirstName"] = "John",
                    ["LastName"] = "Doe"
                },
                Notification = new Notification
                {
                    Title = "Message Title",
                    Body = "Message Body"
                },

                //Token = token,
                Topic = "news"
            };

            var messaging = FirebaseMessaging.DefaultInstance;
            var result = await messaging.SendAsync(message);
            Console.WriteLine(result); //projects/myapp/messages/2492588335721724324

            return Ok(result);
        }

    }
}
