using Microsoft.AspNetCore.Localization;
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
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
           Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
        private ActionResult HttpNotFound()
        {
            throw new NotImplementedException();
        }


    }
}