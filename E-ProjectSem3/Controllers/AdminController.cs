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
        
        public ActionResult Recipes( string start, string end)
        {
            var _start = DateTime.Parse(start);
                //var listRecipe = db.Recipes.Where(r => r.DeletedAt == null).ToList();
            //return View(listRecipe);
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

            var data = db.OrderInfos.Where(o => (Convert.ToDateTime(o.DeletedAt) == null) && (Convert.ToDateTime(o.CreatedAt) >= startTime && Convert.ToDateTime(o.CreatedAt) <= endTime))
                .GroupBy(
                    o => new
                    {
                        Year = Convert.ToDateTime(o.CreatedAt).Year,
                        Month = Convert.ToDateTime(o.CreatedAt).Month,
                    }
                ).Select(g => new
                {
                    Date = Convert.ToDateTime(g.FirstOrDefault().CreatedAt),
                    Money = g.Sum(o => (double?)o.Amount) ?? 0,
                }).OrderBy(o => o.Date).ToList();

            return new JsonResult()
            {
                Data = data.Select(o => new
                {
                    Date = o.Date.ToString("d"),
                    Money = o.Money,
                }),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
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

            var data = db.OrderInfos.Where(o => (Convert.ToDateTime(o.DeletedAt) == null) && (Convert.ToDateTime(o.CreatedAt) >= startTime && Convert.ToDateTime(o.CreatedAt) <= endTime))
                .GroupBy(
                    o => new
                    {
                        Year = Convert.ToDateTime(o.CreatedAt).Year,
                        Month = Convert.ToDateTime(o.CreatedAt).Month,
                    }
                ).Select(g => new
                {
                    Date = Convert.ToDateTime(g.FirstOrDefault().CreatedAt),
                    Money = g.Sum(o => (double?)o.Amount) ?? 0,
                }).OrderBy(o => o.Date).ToList();

            return new JsonResult()
            {
                Data = data.Select(o => new
                {
                    Date = o.Date.ToString("d"),
                    Money = o.Money,
                }),
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}