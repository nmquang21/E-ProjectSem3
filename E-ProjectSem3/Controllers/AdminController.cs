using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_ProjectSem3.Models;

namespace E_ProjectSem3.Controllers
{
    [Authorize(Roles = "admin")]
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

        public ActionResult GetChartDataRevenue(string start, string end)
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

            var data = db.OrderInfos.Where(o => (o.DeletedAt == null) && (o.CreatedAt >= startTime && o.CreatedAt <= endTime))
                .GroupBy(
                    o => new
                    {
                        Year = o.CreatedAt.Year,
                        Month = o.CreatedAt.Month,
                    }
                ).Select(g => new
                {
                    Date = g.FirstOrDefault().CreatedAt,
                    Total = g.Sum(o => (double?)o.Amount) ?? 0,
                }).OrderBy(o => o.Date).ToList();

            return new JsonResult()
            {
                Data = data.Select(o => new
                {
                    Date = o.Date.ToString("d"),
                    Total = o.Total,
                }),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        //var listRecipe = db.Recipes.Where(r => r.DeletedAt == null && r.Status == 1).ToList();
        //return View(listRecipe);

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
        //public ActionResult ApproveRecipe(string approve_id, int recipe_id)
        //{
        //    var recipes = db.Recipes.FirstOrDefault(c => c.ApproveId == approve_id && c.Id == recipe_id);
        //    recipes.Status = 1;
        //    db.SaveChanges();
        //    var listRecipe = db.Recipes.Where(r => r.DeletedAt == null && r.Status == 0).ToList();
        //    return View("RecipeNotApprove", listRecipe);
        //}
        //public ActionResult ApproveNotRecipe(string approve_id, int recipe_id)
        //{
        //    var recipes = db.Recipes.FirstOrDefault(c => c.ApproveId == approve_id && c.Id == recipe_id);
        //    recipes.Status = 0;
        //    db.SaveChanges();
        //    var listRecipe = db.Recipes.Where(r => r.DeletedAt == null && r.Status == 0).ToList();
        //    return View("Recipes", listRecipe);
        //}
        public ActionResult ApproveRecipe( int recipe_id)
        {
            var recipes = db.Recipes.FirstOrDefault(c=>c.Id == recipe_id);
            recipes.Status = 1;
            db.SaveChanges();
            var listRecipe = db.Recipes.Where(r => r.DeletedAt == null && r.Status == 0).ToList();
            return View("RecipeNotApprove", listRecipe);
        }
        public ActionResult ApproveNotRecipe(string approve_id, int recipe_id)
        {
            var recipes = db.Recipes.FirstOrDefault(c => c.ApproveId == approve_id && c.Id == recipe_id);
            recipes.Status = 0;
            db.SaveChanges();
            var listRecipe = db.Recipes.Where(r => r.DeletedAt == null && r.Status == 0).ToList();
            return View("Recipes", listRecipe);
        }
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
            
            
            var dataVip = vips.Select(v => new {
                username = v.ApplicationUser.FirstName + " " + v.ApplicationUser.LastName,
                amount = v.Amount,
                type = v.OrderDescription,
                created_at = v.CreatedAt.ToShortDateString(),

            });
            var charts = db.OrderInfos.Where(o => (o.DeletedAt == null) && (o.CreatedAt >= startTime && o.CreatedAt <= endTime))
               .GroupBy(
                   o => new
                   {
                       Year = o.CreatedAt.Year,
                       Month = o.CreatedAt.Month,
                   }
               ).ToList();

            //var dataChart = charts.Select(o => new
            ////{
            ////    Date = o.Date.ToString("d"),
            ////    gold = o.Total,
            ////    silver = o.
            //});

            return new JsonResult()
            {
                Data = new {
                    dataVip = dataVip,
                    //dataChart = dataChart,
                },
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult User()
        {
            var listUser = db.Users.ToList();
            return View(listUser);

        }

        //Contest
        [HttpGet]
        public ActionResult AddContest()
        {
            return View("~/Views/Admin/Contest/AddContest.cshtml");
        }
        [HttpGet]
        public ActionResult ListContest()
        {
            var contests = db.Contests.ToList();
            return View("~/Views/Admin/Contest/ListContest.cshtml",contests);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult SaveContest(Contest contest, ICollection<Prize> prizes)
        {
            contest.CreatedAt = DateTime.Now;
            contest.StartDate = Convert.ToDateTime(contest.StartDate);
            contest.EndDate = Convert.ToDateTime(contest.EndDate);
            db.Contests.Add(contest);
            try
            {
                db.SaveChanges();
                TempData["AddSuccess"] = "Success";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            return RedirectToAction("ListContest");
            
        }
        public ActionResult ContestDetail(int id)
        {
            var ContestRecipes = db.Contests.Find(id).ContestRecipes.ToList();
            var Contest = db.Contests.Find(id);
            var Prizes = db.Contests.Find(id).Prizes.ToList();
            ViewBag.Contest = Contest;
            ViewBag.Prizes = Prizes;
            return View("~/Views/Admin/Contest/ContestDetail.cshtml", ContestRecipes);
        }
        
       
    }
}