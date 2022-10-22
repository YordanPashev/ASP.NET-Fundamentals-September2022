using Microsoft.AspNetCore.Mvc;
using MVC_Intro_Demo.Models;
using System.Diagnostics;

namespace MVC_Intro_Demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            this.ViewBag.Message = "Hello World!";
            return this.View();
        }

        [HttpGet]
        public IActionResult About()
        {
            this.ViewBag.Message = "This is an ASP.NET Core MVC app.";
            return this.View();
        }

        [HttpGet]
        public IActionResult Numbers()
        {
            this.ViewBag.Message = "Nums 1 ... 50";
            return this.View();
        }

        public IActionResult NumbersToN(int count = 3)
        {
            this.ViewBag.Count = count;
            return this.View();
        }

        [HttpGet]
        public IActionResult Privacy()
        {
            return this.View();
        }


        [HttpGet]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}