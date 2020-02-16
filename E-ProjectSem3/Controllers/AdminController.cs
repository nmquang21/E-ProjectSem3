using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_ProjectSem3.Models;

namespace E_ProjectSem3.Controllers
{

    public class AdminController : Controller
    {
        // GET: Admin
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Recipes()
        {
            var listRecipe = db.Recipes.Where(r => r.DeletedAt == null).ToList();
            return View(listRecipe);
        }
    }
}