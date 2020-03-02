using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
        private List<int> _listSeen = new List<int>();
        public async Task<ActionResult> Index(int? page)
        {
            ViewBag.ListCategories = db.Categories.Where(c => c.Icon != null && c.DeletedAt == null).OrderBy(c => c.Name).ToList();
            ViewBag.DataSliderSmall = db.Recipes.Where(r => r.DeletedAt == null && r.Status == (int)Recipe.RecipeStatus.Active).Take(6).ToList();
            ViewBag.DataSliderBig = db.Recipes.Where(r => r.DeletedAt == null && r.Status == (int)Recipe.RecipeStatus.Active && (r.Id >= 9 && r.Id <= 12)).ToList();
            ViewBag.ContestHappenning = db.Contests.Where(c => c.StartDate < DateTime.Now && c.EndDate > DateTime.Now).ToList();
            ViewBag.ContestUpcoming = db.Contests.Where(c => c.StartDate > DateTime.Now && c.EndDate > DateTime.Now).ToList();
            ViewBag.ContestTookOut = db.Contests.Where(c => c.StartDate < DateTime.Now && c.EndDate < DateTime.Now).ToList();

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
            int pageSize = 6;
            int pageNumber = (page ?? 1);
            ViewBag.CurrentPage = page ?? 1;
            ViewBag.PageTotal = Math.Ceiling((double)listRecipe.Count() / pageSize);
            ViewBag.ThanhToan = await VnPaySuccess();

            return View(listRecipe.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Recipes(string search, int? page, int? category)
        {
            ViewBag.ListCategories = db.Categories.Where(c => c.Icon != null && c.DeletedAt == null).OrderBy(c => c.Name).ToList();
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
        public async Task<ActionResult> RecipeDetail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Recipe recipe = db.Recipes.Find(id);
            if (recipe == null || recipe.DeletedAt != null || recipe.Status != (int)Recipe.RecipeStatus.Active)
            {
                return RedirectToAction("NotFound");
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

            //View count check session:
            await Task.Run(() => ViewCount(recipe));

            ViewBag.ListComment = recipe.Comments.Where(c => c.DeletedAt == null && c.Status == (int)Comment.StatusComment.Active).OrderBy(c=>c.CreatedAt).ToList();
            return View(recipe);
        }

        public ActionResult Categories()
        {
            return View(db.Categories.Where(c=>c.Description == "Main" && c.DeletedAt == null).ToList());
        }
        public ActionResult NotFound()
        {
            return View();
        }
        [Authorize]
        public ActionResult MemberShip()
        {
            ViewBag.ListCategories = db.Categories.Where(c => c.Icon != null && c.DeletedAt == null).OrderBy(c => c.Name).ToList();
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

        public void ViewCount(Recipe recipe)
        {
            List<int> _listSeenSession = Session["RepicesSeen"] as List<int>;
            if (_listSeenSession == null || !_listSeenSession.Contains(recipe.Id))
            {
                recipe.ViewCount++;
                db.Recipes.AddOrUpdate(recipe);
                db.SaveChanges();
                _listSeen.Add(recipe.Id);
                Session["RepicesSeen"] = _listSeen;
            }
        }

        public async Task<bool> VnPaySuccess()
        {
            if (Request.QueryString.Count > 0 && Request.QueryString["vnp_SecureHash"] != null)
            {
                string vnp_HashSecret = "XAUJIMFNKYUUWWNWOLLNIHJCUGLOIGEF"; //Secret key
                var vnpayData = Request.QueryString;
                VnPayLibrary vnpay = new VnPayLibrary();

                foreach (string s in vnpayData)
                {
                    //get all querystring data
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        vnpay.AddResponseData(s, vnpayData[s]);
                    }
                }
                //Lay danh sach tham so tra ve tu VNPAY
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));
                string orderId = vnpay.GetResponseData("vnp_TxnRef");
                string BankCode = vnpay.GetResponseData("vnp_BankCode");
                string vnp_ResponseCode = vnpay.GetResponseData("vnp_ResponseCode");
                String vnp_SecureHash = Request.QueryString["vnp_SecureHash"];
                bool checkSignature = vnpay.ValidateSignature(vnp_SecureHash, vnp_HashSecret);

                if (checkSignature)
                {
                    //Cap nhat ket qua GD
                    OrderInfo order = db.OrderInfos.Find(orderId);
                    if (order != null)
                    {
                        if (order.Status == (int)OrderStatus.Pending)
                        {
                            if (vnp_ResponseCode == "00")
                            {
                                //Thanh toan thanh cong
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
                                return true;
                            }
                            else
                            {
                                //Thanh toan khong thanh cong. Ma loi: vnp_ResponseCode
                                order.Status = (int)OrderStatus.Error;
                                db.OrderInfos.AddOrUpdate(order);
                                db.SaveChanges();
                                return false;
                            }
                        }
                    }
                }
            }
            return false;
        }

        
        public void UPdateDatabase()
        {
            Recipe recipe = new Recipe();
            recipe.Title = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
            recipe.Type = 2;

            ContestRecipe contestRecipe = new ContestRecipe();
            Contest contest = db.Contests.Find(1);


            contestRecipe.Contest = contest;
            contestRecipe.Recipe = recipe;

            db.ContestRecipes.Add(contestRecipe);
            db.SaveChanges();
        }
    }
   
}