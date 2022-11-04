using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class TrucksController : ControllerBase
    {
        private readonly ITruckService _truckService;

        public TrucksController(ITruckService truckService)
        {
            _truckService = truckService;
        }

        /// <summary>
        /// Search all trucks in database
        /// </summary>
        /// <returns></returns>        
        [HttpGet]
        [ProducesResponseType(typeof(BaseModel<IEnumerable<TruckModel.Response>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseModel<IEnumerable<TruckModel.Response>>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _truckService.GetAll());
        }

        /// <summary>
        /// Search a truck in database by Id
        /// </summary>
        /// <returns></returns>        
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(BaseModel<IEnumerable<TruckModel.Response>>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseModel<IEnumerable<TruckModel.Response>>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await _truckService.GetById(id));
        }

        /// <summary>
        /// Insert a new truck in database
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(BaseModel<TruckModel.Response>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseModel<TruckModel.Response>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Insert([FromBody] TruckModel.Request request)
        {
            return Ok(await _truckService.Insert(request));
        }

        /// <summary>
        /// Update an existing truck in database
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(BaseModel<TruckModel.Response>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseModel<TruckModel.Response>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Update(int id, [FromBody] TruckModel.Request request)
        {
            return Ok(await _truckService.Update(id, request));
        }

        /// <summary>
        /// Delete an existing truck in database
        /// </summary>
        /// <returns></returns>        
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(BaseModel<TruckModel.Response>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseModel<TruckModel.Response>), StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _truckService.Delete(id));
        }
    }
}
