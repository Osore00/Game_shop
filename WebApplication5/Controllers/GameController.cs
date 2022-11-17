using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
           
            return View(db.Games.Include(c => c.Categories).ToList().FirstOrDefault(g => g.Id == id));
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
    }
}