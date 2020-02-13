using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_ProjectSem3.Models;

namespace E_ProjectSem3.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            var recipes = db.Recipes;
            return View(recipes.ToList());
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Recipes()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult RecipeDetail()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult AddRecipes()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Categories()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}