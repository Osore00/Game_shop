using Microsoft.AspNetCore.Mvc;
using WebApplication5.Abstract;
using WebApplication5.Models;
using System.Linq;
using System.Text.Json;
using System;
using WebApplication5.Data;
using Microsoft.EntityFrameworkCore;
using WebApplication5.ViewsModels;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;
using System.Drawing;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace WebApplication5.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        AppDBContext db;
        public CartController(AppDBContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.Get<List<CartLine>>("cart") != null)
            {

                return View(HttpContext.Session.Get<List<CartLine>>("cart"));
            }
            return RedirectToAction("Index", "Home");
        }


        public IActionResult RemoveFromCart(int id)
        {

            if (id != null)
            {

                List<CartLine> cart = HttpContext.Session.Get<List<CartLine>>("cart");

                Game game = db.Games.FirstOrDefault(g => g.Id == id);
                CartLine line = cart.FirstOrDefault(g => g.Game.Id == game.Id);

                if (line != null)
                {
                    cart.Remove(line);
                    HttpContext.Session.Set("cart", cart);
                }



            }
            return RedirectToAction("Index", "Cart");
        }
        [AllowAnonymous]
        public IActionResult AddToCart(int id)
        {
            List<CartLine> cart = new List<CartLine>();
            if (HttpContext.Session.Get<List<CartLine>>("cart") != null)
            {
                cart = HttpContext.Session.Get<List<CartLine>>("cart");
            }
            Game game = db.Games.FirstOrDefault(g => g.Id == id);

            if (cart.FirstOrDefault(g => g.Game.Id == game.Id) == null)
            {
                cart.Add(new CartLine { Game = game, Quantity = 1 });
            }
            else
            {
                cart.FirstOrDefault(g => g.Game.Id == game.Id).Quantity += 1;
            }

            HttpContext.Session.Set("cart", cart);
            return RedirectToAction("Game", "Game", new { id = id });
        }
        public IActionResult Order()
        {
            List<CartLine> cart = HttpContext.Session.Get<List<CartLine>>("cart");
            if (cart != null)
            {
                var claimIdent = (ClaimsIdentity)User.Identity;
                var claim = claimIdent.FindFirst(ClaimTypes.NameIdentifier);
                User user = db.Users.FirstOrDefault(u => u.Id == claim.Value);
                UserOdrderCart userOdrderCart = new UserOdrderCart { User = user,cartLines= cart };
                return View(userOdrderCart);
            }
            return RedirectToAction("Index", "Home");
        }
        [HttpPost]
        public IActionResult Order(UserOdrderCart userOdrderCart)
        {
            if (userOdrderCart != null && userOdrderCart.User != null && userOdrderCart.cartLines != null)
            {
                userOdrderCart.User= db.Users.FirstOrDefault(u => u.Id == userOdrderCart.User.Id);
                foreach (var item in userOdrderCart.cartLines)
                {
                    item.Game = db.Games.Include(c => c.Categories).Include(k => k.GameKeys).FirstOrDefault(g => g.Id == item.Game.Id);

                }
                List<GameKeys> AllKeys = db.GameKeys.Include(p => p.Game).ToList();

                Purshes purshes = new Purshes() { User = userOdrderCart.User, Date = DateTime.Now};

                List<GameKeys> pKeys= new List<GameKeys>();
                foreach (var item in userOdrderCart.cartLines)
                {
                    for (int i = 0; i < item.Quantity; i++)
                    {
                        GameKeys key = AllKeys.Where(p => p.Realized == false).Where(p => p.Game.Id == item.Game.Id).FirstOrDefault();
                        key.Realized = true;
                        purshes.GameKeys.Add(key);

                    }

                }

                db.Purshes.Add(purshes);
                db.SaveChanges();




                HttpContext.Session.Set("cart", new List<CartLine>());

                return RedirectToAction("MyOrders", "Cart");
            }
            return RedirectToAction("Index", "Home");

        }
        public IActionResult MyOrders()
        {
                var claimIdent = (ClaimsIdentity)User.Identity;
                var claim = claimIdent.FindFirst(ClaimTypes.NameIdentifier);
                User user = db.Users.FirstOrDefault(u => u.Id == claim.Value);

                return View(db.Purshes.Include(u => u.User).Include(g => g.GameKeys).ThenInclude(g => g.Game).Where(p=>p.User.Id==user.Id));
   
        }
        public IActionResult DetailsOrder(int Id)
        {
            if (Id <= 0)
            {
                return RedirectToAction("Index");
            }

            return View(db.Purshes.Include(u => u.User).Include(g => g.GameKeys).ThenInclude(k => k.Game).FirstOrDefault(i => i.Id == Id));


        }




    }
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            string data = JsonSerializer.Serialize(value);
            session.SetString(key, data);
        }

        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            if (value == null)
            {
                return default;
            }
            var data = JsonSerializer.Deserialize<T>(value);
            return JsonSerializer.Deserialize<T>(value);
        }
    }
}
