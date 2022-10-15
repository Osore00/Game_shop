using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Diagnostics;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class GameController : Controller
    {
        AppDBContext db;
        public GameController(AppDBContext context)
        {
            db = context;
        }

        public IActionResult Game(int id)
        {
            Game game = db.Games.Include(c => c.Categories).ToList().FirstOrDefault(g => g.Id == id);
            return View(game);
        }



    }
}