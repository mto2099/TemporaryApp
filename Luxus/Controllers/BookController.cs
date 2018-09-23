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
using System.IO;
using Luxus.ViewModels;

namespace Luxus.Controllers
{
    public class BookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Book
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.User);
            return View(books.ToList());
        }
        public ActionResult StatusRead()
        {
            var books = db.Books.Include(b => b.User).Where(x => x.Status == 1);
            return View(books.ToList());
        }

        public ActionResult StatusToRead()
        {
            var books = db.Books.Include(b => b.User).Where(x => x.Status == 2);
            return View(books.ToList());
        }

        public ActionResult StatusOnGoing()
        {
            var books = db.Books.Include(b => b.User).Where(x => x.Status == 3);
            return View(books.ToList());
        }
        // GET: Book/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Book/Create
        public ActionResult Create()
        {
            //ViewBag.UserID = new SelectList(db.ApplicationUsers, "Id", "FullName");
            return View();
        }

        // POST: Book/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookID,Title,Desc,Rating,Status,UserID,Image,Shared,PageNo")] Book book, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (file.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Upload"), _FileName);
                       // db.Books.Add(new Book { ImageName = Path.GetFileNameWithoutExtension(file.FileName), ImagePath = _path });
                       // db.SaveChanges();
                       file.SaveAs(_path);
                        book.Image = _FileName;
                    }
                    ViewBag.Message = "File Uploaded Successfully!!";
                   // return View();
                }
                catch
                {
                    ViewBag.Message = "File upload failed!!";
                   // return View();
                }

                var userID = User.Identity.GetUserId();
                book.UserID = userID;
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.UserID = new SelectList(db.ApplicationUsers, "Id", "FullName", book.UserID);
            return View(book);
        }

        // GET: Book/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            //ViewBag.UserID = new SelectList(db.ApplicationUsers, "Id", "FullName", book.UserID);
            return View(book);
        }

        // POST: Book/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookID,Title,Desc,Rating,Status,UserID,Image,Shared,PageNo")] Book book, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (file.ContentLength > 0)
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Upload"), _FileName);
                        // db.Books.Add(new Book { ImageName = Path.GetFileNameWithoutExtension(file.FileName), ImagePath = _path });
                        // db.SaveChanges();
                        file.SaveAs(_path);
                        book.Image = _FileName;
                    }
                    ViewBag.Message = "File Uploaded Successfully!!";
                    // return View();
                }
                catch
                {
                    ViewBag.Message = "File upload failed!!";
                    // return View();
                }

                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.UserID = new SelectList(db.ApplicationUsers, "Id", "FullName", book.UserID);
            return View(book);
        }

        // GET: Book/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Book/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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

        public ActionResult MyBooks()
        {
            var userID = User.Identity.GetUserId();
            var books = db.Books.Include(b => b.User).Where(x => x.UserID == userID);
            return View(books.ToList());
        }
        public ActionResult ShareBooks()
        {   
            var userID = User.Identity.GetUserId();

            var books = db.Books.Include(u => u.User).Where(u => u.UserID == userID).ToList();
            
            

            return View(books);
        }
        [HttpPost]
        public ActionResult ShareBooks(List<Book> books)
        {
            foreach (Book bo in books) {
                db.Entry(bo).State = EntityState.Modified;
                db.SaveChanges();
            }
            

            return View(books);
        }


    }
}
