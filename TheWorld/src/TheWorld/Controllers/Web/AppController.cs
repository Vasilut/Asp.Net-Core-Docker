using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheWorld.Services;
using TheWorld.ViewModels;

namespace TheWorld.Controllers.Web
{
    public class AppController : Controller
    {
        private IMailService _mailService;
        private IConfigurationRoot _config;

        public AppController(IMailService mailService, IConfigurationRoot config)
        {
            _mailService = mailService;
            _config = config;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Contact(ContactViewModel model)
        {
            if(model.Email.Contains("aol.com"))
            {
                ModelState.AddModelError("Email", "We don't support address with aol");
            }

            if (ModelState.IsValid)
            {
                _mailService.SendMail(_config["MailSettings:ToAdress"], model.Email, "From the world", model.Message);
                ViewBag.UserMessage = "The message has been sent";
                ModelState.Clear();
            }
            return View();
        }

        public IActionResult About()
        {
            return View();
        }
    }
}
