using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Globalization;
using WebApplication5.Data;
using WebApplication5.Models;

namespace WebApplication5.Areas.Admin.Controllers
{
    [Area("admin")]
    public class AdminController : Controller
    {
        AppDBContext db;
        string[] validformats = new[] { "dd.MM.yyyy" };
        public AdminController(AppDBContext context)
        {
            db = context;
        }
        [Route("admin/[controller]/[action]")]
        [Route("[controller]")]
        [Route("[controller]/[action]")]
        public IActionResult Index()
        {

            return View();
        }
        [Route("[controller]/[action]")]
        public IActionResult List()
        {
           
            return View(db.Games.Include(c => c.Categories).ToList());
        }
        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }
        [Route("[controller]/[action]")]
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
        [Route("[controller]/[action]")]
        [HttpPost]
        public ActionResult Edit(Game game, int[] selectedCategory)
        {
            if (!ModelState.IsValid)
            {


                string errorMessages = "";

                foreach (var item in ModelState)
                {

                    if (item.Value.ValidationState == ModelValidationState.Invalid)
                    {
                        errorMessages = $"{errorMessages}\nПомилка для властивості:{item.Key}:\n";

                        foreach (var error in item.Value.Errors)
                        {
                            errorMessages = $"{errorMessages}{error.ErrorMessage}\n";
                        }
                    }
                }
                return Content(errorMessages);
            }
            ViewBag.Categories = db.Categories.ToList();
            DateTime dateTime;

            Game newgame = db.Games.Include(c => c.Categories).FirstOrDefault(g => g.Id == game.Id);
            CultureInfo provider = CultureInfo.InvariantCulture;

            if (!DateTime.TryParseExact(game.Date_of_public, validformats, provider, DateTimeStyles.None, out dateTime))
            {
                ModelState.AddModelError("Date_of_public", "* Невірний формат дати");
            }
            if (!ModelState.IsValid)
                return View(newgame);

            newgame.Name = game.Name;
            newgame.Price = game.Price;
            newgame.Producer = game.Producer;
            newgame.Date_of_public = game.Date_of_public;
            newgame.LongDesc = game.LongDesc;
            newgame.Photos = game.Photos;
            newgame.Categories.Clear();

            if (selectedCategory != null)
            {

                foreach (var c in db.Categories.Where(co => selectedCategory.Contains(co.Id)))
                {
                    newgame.Categories.Add(c);
                }
            }
            db.Entry(newgame).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("List");
        }

        [Route("[controller]/[action]")]
        [HttpGet]
        public ActionResult Create()
        {

            ViewBag.Categories = db.Categories.ToList();
            return View();
        }
        [Route("[controller]/[action]")]
        [HttpPost]
        public IActionResult Create(Game game, int[] selectedCategory)
        {
            if (!ModelState.IsValid)
            {


                string errorMessages = "";

                foreach (var item in ModelState)
                {

                    if (item.Value.ValidationState == ModelValidationState.Invalid)
                    {
                        errorMessages = $"{errorMessages}\nПомилка для властивості:{item.Key}:\n";

                        foreach (var error in item.Value.Errors)
                        {
                            errorMessages = $"{errorMessages}{error.ErrorMessage}\n";
                        }
                    }
                }
                return Content(errorMessages);
            }
            ViewBag.Categories = db.Categories.ToList();
            DateTime dateTime;
            Game newgame = db.Games.Include(c => c.Categories).FirstOrDefault(g => g.Id == game.Id);

            if (!DateTime.TryParseExact(game.Date_of_public, validformats, CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTime))
            {
                ModelState.AddModelError("Date_of_public", "* Невірний формат дати");
            }
            if (!ModelState.IsValid)
                return View(newgame);
            game.Categories = new List<Category>();

            if (selectedCategory != null)
            {

                foreach (Category c in db.Categories.Include(c => c.Games).Where(co => selectedCategory.Contains(co.Id)))
                {
                    game.Categories.Add(c);
                }
            }
            db.Games.Add(game);
            db.SaveChanges();
            return RedirectToAction("List");
        }
        [Route("[controller]/[action]")]
        [HttpGet]
        public ActionResult Delete(int id)
        {
            Game game = db.Games.Include(c => c.Categories).ToList().FirstOrDefault(g => g.Id == id);
            db.Games.Remove(game);
            db.SaveChanges();
            return RedirectToAction("List");
        }



        [Route("[controller]/[action]")]
        public ActionResult CategoryList()
        {
            return View(db.Categories.Include(c => c.Games).ToList());

        }


        [Route("[controller]/[action]")]
        [HttpGet]
        public ActionResult EditCat(int id = 0)
        {
            Category category = db.Categories.FirstOrDefault(g => g.Id == id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }
        [Route("[controller]/[action]")]
        [HttpPost]
        public ActionResult EditCat(Category category)
        {
            Category newCategory = db.Categories.Include(c=>c.Games).FirstOrDefault(g => g.Id == category.Id);
            newCategory.Name = category.Name;
            db.Entry(newCategory).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("CategoryList");
        }

        [Route("[controller]/[action]")]
        [HttpGet]
        public ActionResult CreatCat()
        {
            return View();
        }
        [Route("[controller]/[action]")]
        [HttpPost]
        public IActionResult CreatCat( string name)
        {
            Category category = new Category();
            category.Name = name;
            db.Categories.Add(category);
            db.SaveChanges();
            return RedirectToAction("CategoryList");
        }
        [Route("[controller]/[action]")]
        [HttpGet]
        public ActionResult DeleteCat(int id)
        {
           Category category = db.Categories.Include(c => c.Games).FirstOrDefault(g => g.Id == id);
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("CategoryList");
        }

        [Route("[controller]/[action]")]
        public IActionResult ListOrders()
        {

            return View(db.Purshes.Include(u => u.User).Include(g => g.GameKeys).ThenInclude(g=>g.Game));
        }

        [Route("[controller]/[action]")]
        public IActionResult DetailsOrder(int Id)
        {
            if (Id <= 0)
            {
                return RedirectToAction("Index");
            }

            return View(db.Purshes.Include(u => u.User).Include(g => g.GameKeys).ThenInclude(k => k.Game).FirstOrDefault(i => i.Id == Id));


        }



    }
}
