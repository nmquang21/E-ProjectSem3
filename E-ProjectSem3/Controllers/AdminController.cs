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
        public ActionResult CommentNotApprove()
        {
            var listComment = db.Comments.Where(r => r.DeletedAt == null && r.Status == 0 ).ToList();
            return View(listComment);
        }
        public ActionResult CommentApproved()
        {
            var listComment = db.Comments.Where(r => r.DeletedAt == null && r.Status == 1).ToList();
            return View(listComment);
        }
        public ActionResult ApproveComment(string user_id, int recipe_id)
        {
            var comment = db.Comments.FirstOrDefault(c => c.UserId == user_id && c.RecipeId == recipe_id);
            comment.Status = 1;
            db.SaveChanges();
            var listComment = db.Comments.Where(r => r.DeletedAt == null && r.Status == 0).ToList();
            return View("CommentNotApprove", listComment);
        }
    }
}