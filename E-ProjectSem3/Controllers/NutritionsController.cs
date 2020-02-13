using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_ProjectSem3.Models;

namespace E_ProjectSem3.Controllers
{
    public class NutritionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Nutritions
        public ActionResult Index()
        {
            return View(db.Nutritions.ToList());
        }

        // GET: Nutritions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutrition nutrition = db.Nutritions.Find(id);
            if (nutrition == null)
            {
                return HttpNotFound();
            }
            return View(nutrition);
        }

        // GET: Nutritions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Nutritions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NutritionId,Name,Value,CreatedAt,UpdatedAt,DeletedAt")] Nutrition nutrition)
        {
            if (ModelState.IsValid)
            {
                db.Nutritions.Add(nutrition);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(nutrition);
        }

        // GET: Nutritions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutrition nutrition = db.Nutritions.Find(id);
            if (nutrition == null)
            {
                return HttpNotFound();
            }
            return View(nutrition);
        }

        // POST: Nutritions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NutritionId,Name,Value,CreatedAt,UpdatedAt,DeletedAt")] Nutrition nutrition)
        {
            if (ModelState.IsValid)
            {
                db.Entry(nutrition).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(nutrition);
        }

        // GET: Nutritions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nutrition nutrition = db.Nutritions.Find(id);
            if (nutrition == null)
            {
                return HttpNotFound();
            }
            return View(nutrition);
        }

        // POST: Nutritions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Nutrition nutrition = db.Nutritions.Find(id);
            db.Nutritions.Remove(nutrition);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
