using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_ProjectSem3.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace E_ProjectSem3.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
       
        // GET: Comments
        public ActionResult Index()
        {
            return View(db.Comments.ToList());
        }
        
        // GET: Comments/Create
        public ActionResult Create(string content, int rate, int recipeId)
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            if (content == null || recipeId == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var userId = User.Identity.GetUserId();
            var existComment = db.Comments.Where(c => c.RecipeId == recipeId && c.UserId == userId).ToList();
            if (existComment.Count == 1)
            {
                return RedirectToAction("RecipeDetail", "Home", new { id = recipeId });
            }
            Comment cm = new Comment();
            cm.Content = content;
            cm.Rate = rate;
            //cm.RecipeId = recipeId;

            var user = UserManager.FindById(userId);
            var recipe = db.Recipes.Find(recipeId);

            cm.UserId = User.Identity.GetUserId();
            cm.ApplicationUser = user;
            Debug.WriteLine(user);
            Debug.WriteLine(cm.ApplicationUser);
            cm.Recipe = recipe;
            cm.Status = (int)Comment.StatusComment.NonActive;
            cm.CreatedAt = DateTime.Now;

            db.Comments.Add(cm);
            db.SaveChanges();


            return RedirectToAction("RecipeDetail","Home",new{id = recipeId});
        }
        public ActionResult CreateAjax(string content, int rate, int recipeId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            if (content == null || recipeId == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var userId = User.Identity.GetUserId();
            var existComment = db.Comments.Where(c => c.RecipeId == recipeId && c.UserId == userId).ToList();
            if (existComment.Count == 1)
            {
                return RedirectToAction("RecipeDetail", "Home", new { id = recipeId });
            }
            Comment cm = new Comment();
            cm.Content = content;
            cm.Rate = rate;
            cm.RecipeId = recipeId;

            var user = UserManager.FindById(userId);
            var recipe = db.Recipes.Find(recipeId);

            cm.UserId = User.Identity.GetUserId();
            cm.ApplicationUser = user;
            cm.Recipe = recipe;
            cm.Status = (int)Comment.StatusComment.NonActive;
            cm.CreatedAt = DateTime.Now;

            db.Comments.Add(cm);
            db.SaveChanges();

            return Json(new { data = "Success" }, JsonRequestBehavior.AllowGet);
        }
    }
}
