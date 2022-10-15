using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Diagnostics;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class HomeController : Controller
    {
        AppDBContext db;
        public HomeController(AppDBContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            ViewBag.Title = "Ігри";
            return View(db.Games.Include(c => c.Categories).ToList());
        }



    }
}