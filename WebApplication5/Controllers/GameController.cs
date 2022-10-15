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

        //[HttpGet]
        //public ActionResult Edit(int id = 0)
        //{
        //    Game game = db.Games.Include(c => c.Categories).ToList().FirstOrDefault(g => g.Id == id);
        //    if (game == null)
        //    {
        //        return HttpNotFound();
        //    }
           
        //    return View(game);
        //}

        //private ActionResult HttpNotFound()
        //{
        //    throw new NotImplementedException();
        //}

        //[HttpPost]
        //public ActionResult Edit(Student student, int[] selectedCourses)
        //{
        //    Student newStudent = db.Students.Find(student.Id);
        //    newStudent.Name = student.Name;
        //    newStudent.Surname = student.Surname;
        //    newStudent.Courses.Clear();
        //    if (selectedCourses != null)
        //    {
        //        //отримуємо вибрані курси
        //        foreach (var c in db.Courses.Where(co =>
        //       selectedCourses.Contains(co.Id)))
        //        {
        //            91
        //        newStudent.Courses.Add(c);
        //        }
        //    }
        //    db.Entry(newStudent).State = EntityState.Modified;
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}




    }
}