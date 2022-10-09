using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Diagnostics;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {
        AppDBContent db;
        public HomeController(AppDBContent context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Ігри";
            return View(db.Games);
        }



    }
}