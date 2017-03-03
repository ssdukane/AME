using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.Models;
using TheWorld.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;
        private IWorldRepository _repository;

        //private WorldContext _context;

        public AppController(IMailService mailService, IConfigurationRoot config, IWorldRepository repository)
        {
            _mailService = mailService;
            _config = config;
            //_context = context;
            _repository = repository;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            //var data = _context.Trips.ToList();
            
            // var data = _repository.GetAllTrips();
            //return View(data);

            return View("Index");
        }        
        
        public IActionResult Trips()
        {            
            var trips = _repository.GetAllTrips();

            return View(trips);
        }

        public IActionResult Contact()
        {
            //throw new InvalidOperationException("Bad thing happened with good developer");
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if (model.Email.Contains("aol.com"))
            {
                ModelState.AddModelError("ModelOnly", "We don't support aol address");
            }
            if (ModelState.IsValid)
            {
                _mailService.SendMail(model.Email, _config["MailSettings:FromAddress"], "From THE World.com", model.Message);
                ModelState.Clear();
                ViewBag.UserMessage = "Message Sent";
            }       
            return View();
        }
        public IActionResult About()
        {
            return View();
        }
    }
}
