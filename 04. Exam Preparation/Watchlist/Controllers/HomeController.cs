﻿namespace Watchlist.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("All", "Movies");
        }
    }
}