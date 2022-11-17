using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;

namespace WebApplication5.Controllers
{
    public class CategoryController : Controller
    {
        AppDBContext db;
        public CategoryController(AppDBContext context)
        {
            db = context;
        }

        // GET: Category
        public ActionResult CategoryList()
        {
            
            return View(db.Categories);
        }

        // GET: Category/Details/5
        public ActionResult Details(int id)
        {
            return View(db.Categories.Include(c => c.Games).FirstOrDefault(g => g.Id == id));
        }

    }
}
