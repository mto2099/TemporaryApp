using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Luxus.Models;
using Microsoft.AspNet.Identity;

namespace Luxus.Controllers
{
    public class RentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rent
        public ActionResult Index()
        {
            var userID = User.Identity.GetUserId();
            var rents = db.Rents.Include(b => b.User).Where(x =>x.UserID == userID);
            return View(rents.ToList());
        }

        // GET: Rent/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rent rent = db.Rents.Find(id);
            if (rent == null)
            {
                return HttpNotFound();
            }
            return View(rent);
        }

        // GET: Rent/Create
        public ActionResult Create()
        {
            string UserId = User.Identity.GetUserId();
            //ViewBag.BookID = new SelectList(db.Books, "BookID", "Title");
            ViewBag.BookID = new SelectList(db.Books.Where(x => x.UserID == UserId), "BookID", "Title");
            return View();
        }

        // POST: Rent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,BookID,StartDate,EndDate,RecipientName")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                string userid = User.Identity.GetUserId();
                rent.UserID = userid;
                db.Rents.Add(rent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(rent);
        }

        // GET: Rent/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rent rent = db.Rents.Find(id);
            if (rent == null)
            {
                return HttpNotFound();
            }
            return View(rent);
        }

        // POST: Rent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,BookID,StartDate,EndDate,RecipientName")] Rent rent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rent);
        }

        // GET: Rent/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rent rent = db.Rents.Find(id);
            if (rent == null)
            {
                return HttpNotFound();
            }
            return View(rent);
        }

        // POST: Rent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Rent rent = db.Rents.Find(id);
            db.Rents.Remove(rent);
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
