using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using System.Diagnostics;
using System.Numerics;
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

        public ActionResult CategoryList(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Category category = db.Categories.Include(c => c.Games).ToList().FirstOrDefault(g => g.Id == id);
            return View(category);

        }

        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public ActionResult Edit(int id = 0)
        {
            Game game = db.Games.Include(c => c.Categories).ToList().FirstOrDefault(g => g.Id == id);
            if (game == null)
            {
                return HttpNotFound();
            }
            ViewBag.Categories = db.Categories.ToList();
            return View(game);
        }
        [HttpPost]
        public ActionResult Edit(Game game)
        {
            Game newgame = db.Games.Include(c => c.Categories).ToList().FirstOrDefault(g => g.Id == game.Id);
            newgame.Name = game.Name;
            newgame.Price = game.Price;
            newgame.Producer = game.Producer;
            newgame.Date_of_public = game.Date_of_public;
            newgame.LongDesc = game.LongDesc;
            newgame.Photos = game.Photos;
            newgame.Available = true;
            newgame.Categories.Clear();
            var form = Request.Form;
            int [] selectedCategory = form["selectedСategory"].Select(s => int.Parse(s)).ToArray();

            if (selectedCategory != null)
            {
                
                //отримуємо вибрані курси
                foreach (var c in db.Categories.Where(co =>
               selectedCategory.Contains(co.Id)))
                {
                newgame.Categories.Add(c);
                }
            }
            db.Entry(newgame).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult Create(int id = 0)
        {
            
            ViewBag.Categories = db.Categories.Include(c => c.Games).ToList();
            return View();
        }
        [HttpPost]
        public ActionResult Create(Game game)
        {

            //var form = Request.Form;
            //int[] selectedCategory = form["selectedСategory"].Select(s => int.Parse(s)).ToArray();

            //if (selectedCategory != null)
            //{

            //    //отримуємо вибрані курси
            //    foreach (Category c in db.Categories.Include(c => c.Games).ToList().Where(co =>
            //   selectedCategory.Contains(co.Id)))
            //    {
            //        game.Categories.Add(c);
            //    }
            //}
            db.Games.Add(game);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Delete(int id )
        {
            Game game = db.Games.Include(c => c.Categories).ToList().FirstOrDefault(g => g.Id == id);
            db.Games.Remove(game);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}