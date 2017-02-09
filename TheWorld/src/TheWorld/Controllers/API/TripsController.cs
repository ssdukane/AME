using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.Models;
using TheWorld.ViewModels;
using AutoMapper;
using Microsoft.Extensions.Logging;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.Controllers.API
{
    //[Route("api/[controller]")]
    [Route("api/trips")]
    public class TripsController : Controller
    {
        private IWorldRepository _repository;
        private ILogger<TripsController> _logger;

        public TripsController(IWorldRepository repository, ILogger<TripsController> logger )
        {
            _repository = repository;
            _logger = logger;
        }

        // GET: api/values
        //[HttpGet("api/trips")]
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var results = _repository.GetAllTrips();
                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(results));
            }
            catch (Exception ex)
            {
                //TODO Logging
                _logger.LogError("Failed to get all trips : {ex}");
                return BadRequest("Errpr occured");
            }
            
        }

        // POST api/values
        //[HttpPost("api/trips")]

        //Direct call
        //[HttpPost]
        //public IActionResult Post([FromBody]TripViewModel theTrip)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var newTrip = Mapper.Map<Trip>(theTrip);                
        //        return Created($"api/trips/{theTrip.Name}", newTrip);
        //    }

        //    return BadRequest("Bad Data");
        //}


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]TripViewModel theTrip)
        {
            if (ModelState.IsValid)
            {
                var newTrip = Mapper.Map<Trip>(theTrip);

                _repository.AddTrip(newTrip);
                if (await _repository.SaveChangesAsync())
                {
                    return Created($"api/trips/{theTrip.Name}", Mapper.Map<TripViewModel>(newTrip));
                }                    
            }

            return BadRequest("Failed to save trip");

            
        }

        //// GET: api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody]string value)
        //{
        //}

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
