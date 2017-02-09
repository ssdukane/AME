using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.Models;
using Microsoft.Extensions.Logging;
using AutoMapper;
using TheWorld.ViewModels;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.Controllers.API
{
   // [Route("api/stops")]
    [Route("/api/trips/{tripName}/stops")]
    public class StopsController : Controller
    {
        private IWorldRepository _repository;
        private ILogger<StopsController> _logger;

        public StopsController(IWorldRepository repository, ILogger<StopsController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get(string tripName)
        {
            try
            {
                var trip = _repository.GetTripByName(tripName);

                return Ok(Mapper.Map<IEnumerable<StopViewModel>>(trip.Stops.OrderBy(s => s.Order).ToList()));
            }
            catch (Exception)
            {

                throw;
            }            
        }

        [HttpPost]
        public async Task<IActionResult> Post(string tripName, [FromBody]StopViewModel vm)
        {
            try
            {
                var newStop = Mapper.Map<Stop>(vm);

                _repository.AddStop(tripName, newStop);

                if (await _repository.SaveChangesAsync())
                { 
                return Created($"/api/trips/{tripName}/stops/{newStop.Name}",
                    Mapper.Map<StopViewModel>(newStop));
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed to save new stop : {0}", ex);
            }

            return BadRequest("Failed to save new stop");
        }
        
    }
}
