using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_ProjectSem3.Models;
using E_ProjectSem3.Services;
using LinqKit;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;

namespace E_ProjectSem3.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private MemberService memberService = new MemberService();
        public ActionResult Index(int? page)
        {

            ViewBag.ListCategories = db.Categories.Where(c => c.DeletedAt == null).OrderBy(c => c.Name).ToList();
            ViewBag.DataSliderSmall = db.Recipes.Where(r => r.DeletedAt == null && r.Status == (int)Recipe.RecipeStatus.Active).Take(6).ToList();
            ViewBag.DataSliderBig = db.Recipes.Where(r => r.DeletedAt == null && r.Status == (int)Recipe.RecipeStatus.Active).Take(6).ToList();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //Check expired member ship:
            ViewBag.Expired = "";
            if (Request.IsAuthenticated && (UserManager.IsInRole(User.Identity.GetUserId(), "silver") || UserManager.IsInRole(User.Identity.GetUserId(), "gold")))
            {
                var id = User.Identity.GetUserId();
                var user = UserManager.FindById(id);
                if (memberService.CheckExpiredMember(user) == true)
                {
                    ViewBag.Expired = "Expired";
                }
            }

            var listRecipe = db.Recipes.Where(r => r.DeletedAt == null && r.Status == (int) Recipe.RecipeStatus.Active).OrderBy(r => r.Title).ToList();
            int pageSize = 9;
            int pageNumber = (page ?? 1);
            ViewBag.CurrentPage = page ?? 1;
            ViewBag.PageTotal = Math.Ceiling((double)listRecipe.Count() / pageSize);


            //Vn pay:
            ViewBag.ThanhToan = "";
            Uri myUri = new Uri(Request.Url.ToString());
            string code = HttpUtility.ParseQueryString(myUri.Query).Get("vnp_ResponseCode");
            string BankCode = HttpUtility.ParseQueryString(myUri.Query).Get("vnp_BankCode");
            Debug.WriteLine("OrderId = " + TempData["OrderId"]);
            if (code == "00")
            {
                string orderID = (string)TempData["OrderId"];
                var order = db.OrderInfos.Find(orderID);
                if (order == null)
                {
                    return RedirectToAction("NotFound");
                }
                ViewBag.ThanhToan = order.OrderDescription;
                order.Status = (int)OrderStatus.Paid;
                order.BankCode = BankCode;
                db.OrderInfos.AddOrUpdate(order);

                if (User.Identity.IsAuthenticated)
                {
                    //Luu membership
                    var listMemberType = db.Members.ToList();
                    var memberShip = new Membership();
                    foreach (var memberType in listMemberType)
                    {
                        if (order.Amount == memberType.Price && order.OrderDescription == memberType.MemberType)
                        {
                            memberShip.Member = memberType;
                        }
                    }
                    var id = User.Identity.GetUserId();
                    memberShip.ApplicationUser = UserManager.FindById(id);
                    memberShip.CreatedAt = DateTime.Now;
                    db.Memberships.Add(memberShip);
                    //Add role member:
                    UserManager.AddToRole(id, memberShip.Member.RoleName);
                }

                db.SaveChanges();
            }
            return View(listRecipe.ToPagedList(pageNumber, pageSize));
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
        [AllowAnonymous]
        public ActionResult Recipes(string search, int? page, int? category)
        {
            ViewBag.ListCategories = db.Categories.Where(c => c.DeletedAt == null).OrderBy(c=>c.Name).ToList();
            ViewBag.CurrentCategory = category;
            ViewBag.Search = search;
            ViewBag.DataSlider = db.Recipes.Where(r => r.DeletedAt == null && r.Status == (int) Recipe.RecipeStatus.Active).Take(8).ToList();

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //Check expired member ship:
            ViewBag.Expired = "";
            if (Request.IsAuthenticated && (UserManager.IsInRole(User.Identity.GetUserId(), "silver") || UserManager.IsInRole(User.Identity.GetUserId(), "gold")))
            {
                var id = User.Identity.GetUserId();
                var user = UserManager.FindById(id);
                if (memberService.CheckExpiredMember(user) == true)
                {
                    ViewBag.Expired = "Expired";
                }
            }

            var predicate = PredicateBuilder.New<Recipe>(true);
            if (search != null)
            {
                page = 1;
            }
            var listRecipe = from r in db.Recipes select r;

            if (!String.IsNullOrEmpty(search))
            {
                predicate = predicate.Or(r => r.Title.Contains(search));
            }
            if (category > 0)
            {
                predicate = predicate.Or(r => r.Category.Id == (int)category);
            }

            predicate = predicate.And(c => c.Status == (int)Recipe.RecipeStatus.Active);
            predicate = predicate.And(c => c.DeletedAt == null);
            listRecipe = listRecipe.Where(predicate).OrderBy(r => r.Title);

            int pageSize = 9;
            int pageNumber = (page ?? 1);
            ViewBag.CurrentPage = page ?? 1;
            ViewBag.PageTotal = Math.Ceiling((double)listRecipe.Count() / pageSize);

            Debug.WriteLine(page);
            Debug.WriteLine(Math.Ceiling((double)listRecipe.Count() / pageSize));

            return View(listRecipe.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult RecipeDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null)
            {
                return HttpNotFound();
            }
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //Check expired member ship:
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.Identity.GetUserId();
                var user = UserManager.FindById(userId);
                if (memberService.CheckExpiredMember(user))
                {
                    TempData["Expired"] = "Expired!";
                }
            }
            if (recipe.Type == (int)Recipe.RecipeType.NotFree && (!Request.IsAuthenticated ||(!UserManager.IsInRole(User.Identity.GetUserId(),"silver") 
                                && !UserManager.IsInRole(User.Identity.GetUserId(),"gold") && recipe.ApplicationUser.Id != User.Identity.GetUserId())))
            {
                var message = "Become a member";
                return RedirectToAction("Recipes", "Home",new{msg=message});
            }

            recipe.ViewCount++;
            db.Recipes.AddOrUpdate(recipe);
            db.SaveChanges();

            return View(recipe);
        }

        public ActionResult Categories()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult NotFound()
        {
            return View();
        }
        [Authorize]
        public ActionResult MemberShip()
        {
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //Check expired member ship:
            var id = User.Identity.GetUserId();
            var user = UserManager.FindById(id);
            //if (memberService.CheckExpiredMember(user) == false)
            //{
            //    ViewBag.UserMemberShip = db.Memberships.Where(m => m.ApplicationUser.Id == id).ToList()[0];
            //}

            ViewBag.ListMemberType = db.Members.ToList();
            return View();
        }
    }
}