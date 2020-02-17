using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_ProjectSem3.Models;
using Microsoft.AspNet.Identity;

namespace E_ProjectSem3.Controllers
{
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Comments
        public ActionResult Index()
        {
            return View(db.Comments.ToList());
        }
        [Authorize]
        // GET: Comments/Create
        public ActionResult Create(string content, int rate, int recipeId)
        {
            if (content == null || recipeId == null)
            {
                return RedirectToAction("NotFound", "Home");
            }
            Comment cm = new Comment();
            cm.Content = content;
            cm.Rate = rate;
            cm.RecipeId = recipeId;
            cm.UserId = User.Identity.GetUserId();

            return RedirectToAction("RecipeDetail","Home");
        }

    }
}
