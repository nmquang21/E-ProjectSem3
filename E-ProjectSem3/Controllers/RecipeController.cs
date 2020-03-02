using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_ProjectSem3.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace E_ProjectSem3.Controllers
{
    public class RecipeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Recipe
        public ActionResult LikeAjax(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            if (!Request.IsAuthenticated)
            {
                return Json(new { data = "NotLogIn" }, JsonRequestBehavior.AllowGet);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null || recipe.DeletedAt != null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var likeCount = db.WishLists.Where(w => w.RecipeId == id).ToList().Count;

            //var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            var userId = User.Identity.GetUserId();
            Debug.WriteLine(db.WishLists.Find(id, userId));
            if (db.WishLists.Find(id, userId) != null)
            {
                return Json(new { data = likeCount }, JsonRequestBehavior.AllowGet);
            }
            WishList like = new WishList();
            like.UserId = User.Identity.GetUserId();
            like.RecipeId = id;
            like.CreatedAt = DateTime.Now;
            try
            {
                db.WishLists.Add(like);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            
            return Json(new { data = likeCount + 1 }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UnLikeAjax(int id)
        {
            if (!Request.IsAuthenticated)
            {
                return Json(new { data = "NotLogIn" }, JsonRequestBehavior.AllowGet);
            }
            db.Configuration.ProxyCreationEnabled = false;
            var userId = User.Identity.GetUserId();
            WishList like = db.WishLists.Find(id, userId);
            if (like == null)
            {
                return RedirectToAction("NotFound", "Home");
            }

            var likeCount = db.WishLists.Where(w => w.RecipeId == id).ToList().Count;

            try
            {
                db.WishLists.Remove(like);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return Json(new { data = likeCount - 1 }, JsonRequestBehavior.AllowGet);

        }
        public ActionResult GetPopularRecipesAjax()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var listRecipe = db.Recipes.Where(r => r.DeletedAt == null && r.Status == (int) Recipe.RecipeStatus.Active)
                .OrderByDescending(r => r.ViewCount).Take(3).ToList();
            return Json(new { data = listRecipe}, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetUsernameAjax()
        {
            db.Configuration.ProxyCreationEnabled = false;
            var userId = User.Identity.GetUserId();
            var user = db.Users.Find(userId);
            if (user == null)
            {
                return Json(new { data = " " }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { data = user.UserName }, JsonRequestBehavior.AllowGet);
        }
    }
}