using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using System.Diagnostics;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class GameController : Controller
    {
        AppDBContent db;
        public GameController(AppDBContent context)
        {
            db = context;
        }

        public IActionResult Game(int id)
        {
            Game game = db.Games.FirstOrDefault(g => g.Id == id);
            return View(game);
        }



    }
}