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
            var listRecipe = db.Recipes.Where(r => r.DeletedAt == null && r.Status == 1).ToList();
            return View(listRecipe);
        }
        public ActionResult RecipeNotApprove()
        {
            var listRecipe = db.Recipes.Where(r => r.DeletedAt == null && r.Status == 0).ToList();
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
        public ActionResult ApproveRecipe(string approve_id, int recipe_id)
        {
            var recipes = db.Recipes.FirstOrDefault(c => c.ApproveId == approve_id && c.Id == recipe_id);
            recipes.Status = 1;
            db.SaveChanges();
            var listRecipe = db.Recipes.Where(r => r.DeletedAt == null && r.Status == 0).ToList();
            return View("RecipeNotApprove", listRecipe);
        }
        //public ActionResult ApproveNotRecipe(string approve_id, int recipe_id)
        //{
        //    var recipes = db.Recipes.FirstOrDefault(c => c.ApproveId == approve_id && c.Id == recipe_id);
        //    recipes.Status = 0;
        //    db.SaveChanges();
        //    var listRecipe = db.Recipes.Where(r => r.DeletedAt == null && r.Status == 0).ToList();
        //    return View("Recipes", listRecipe);
        //}
        public ActionResult StatisticsVip() {
            var vips = db.OrderInfos.Where(v => v.Status == (int)OrderStatus.Paid).OrderBy(v => v.CreatedAt).Take(10).ToList();
            return View(vips);
        }
        public ActionResult Test(string start, string end)
        {
            var startTime = DateTime.Now;
            startTime = startTime.AddYears(-1);
            try
            {
                startTime = DateTime.Parse(start);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            startTime = new DateTime(startTime.Year, startTime.Month, startTime.Day, 0, 0, 0, 0);

            var endTime = DateTime.Now;
            try
            {
                endTime = DateTime.Parse(end);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            endTime = new DateTime(endTime.Year, endTime.Month, endTime.Day, 23, 59, 59, 0);

            var vips = db.OrderInfos.Where(v => v.Status == (int)OrderStatus.Paid && (v.CreatedAt >= startTime && v.CreatedAt <= endTime)).OrderBy(v => v.CreatedAt).ToList();

            return new JsonResult()
            {
                Data = vips.Select(v => new { 
                    username = v.ApplicationUser.FirstName+" "+ v.ApplicationUser.LastName,
                    amount = v.Amount,
                    type = v.OrderDescription,
                    created_at = v.CreatedAt.ToShortDateString(),
                }),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}