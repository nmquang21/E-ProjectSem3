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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace E_ProjectSem3.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private MemberService memberService = new MemberService();
        public ActionResult Index()
        {
            var recipes = db.Recipes;

            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
            //Check expired member ship:
            ViewBag.Expired = "";
            if (User.Identity.IsAuthenticated)
            {
                var id = User.Identity.GetUserId();
                var user = UserManager.FindById(id);
                if (memberService.CheckExpiredMember(user) == true)
                {
                    ViewBag.Expired = "Expired";
                }
            }

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
            return View(recipe);
        }
        public ActionResult AddRecipes()
        {
            ViewBag.Categories = db.Categories.OrderBy(c => c.Name).ToList();
            return View();
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