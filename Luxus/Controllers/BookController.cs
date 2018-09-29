﻿using System;
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
    [Authorize]
    public class BookController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Book
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.User);
            return View(books.ToList());
        }

        // GET: Item
        public ViewResult Search(string searchString)
        {
            var books = db.Books.ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(s => (s.Title!=null && s.Title.Contains(searchString))
                                       || (s.Author != null && s.Author.Contains(searchString))).ToList();
            }

            return View("MyBooks", books);
        }

        public ActionResult StatusRead()
        {
            var userID = User.Identity.GetUserId();
            var books = db.Books.Include(b => b.User).Where(x => x.Status == 1 && x.UserID == userID);
            return View(books.ToList());
        }

        public ActionResult StatusToRead()
        {
            var userID = User.Identity.GetUserId();
            var books = db.Books.Include(b => b.User).Where(x => x.Status == 3 && x.UserID == userID);
            return View(books.ToList());
        }

        public ActionResult StatusOnGoing()
        {
            var userID = User.Identity.GetUserId();
            var books = db.Books.Include(b => b.User).Where(x => x.Status == 2 && x.UserID == userID);
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
            ViewBag.EditAccess = (User.Identity.GetUserId() == book.UserID) ? true : false;
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
        public ActionResult Create([Bind(Include = "BookID,Title,Desc,Rating,Author,Status,UserID,Image,Shared,PageNo")] Book book, HttpPostedFileBase file)
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
                return RedirectToAction("MyBooks");
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
        public ActionResult Edit([Bind(Include = "BookID,Title,Desc,Rating,Author,Status,UserID,Image,Shared,PageNo")] Book book, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (file.ContentLength > 0 && file.FileName != "")
                    {
                        string _FileName = Path.GetFileName(file.FileName);
                        string _path = Path.Combine(Server.MapPath("~/Upload"), _FileName);
                        file.SaveAs(_path);
                        book.Image = _FileName;
                    }
                    ViewBag.Message = "File Uploaded Successfully!!";
                }
                catch
                {
                    ViewBag.Message = "File upload failed!!";
                }
                book.Image = book.Image;

                var userID = User.Identity.GetUserId();
                book.UserID = userID;
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
            List<ShBookVM> bookVM = new List<ShBookVM>();
            foreach (Book b in books)
            {
                bookVM.Add(new ShBookVM(b));
            }

            return View(bookVM);
        }

        [HttpPost]
        public JsonResult ShareBooks(string BookID, string action)
        {
            string userId = User.Identity.GetUserId();
            if (!string.IsNullOrEmpty(userId))
            {
                int bookid = Int32.Parse(BookID);
                Book book = db.Books.Where(x => x.UserID == userId && x.BookID == bookid).FirstOrDefault();
                if (action == "add")
                {
                    book.Shared = true;
                }
                else
                {
                    book.Shared = false;
                }
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Success");
            }
            return Json("Invalid User!");
        }

        [HttpPost]
        public JsonResult Rating(string BookID, string Rating)
        {
            string userId = User.Identity.GetUserId();
            if (!string.IsNullOrEmpty(userId))
            {
                int bookid = Int32.Parse(BookID);
                
                Book book = db.Books.Where(x => x.UserID == userId && x.BookID == bookid).FirstOrDefault();
                if (!string.IsNullOrEmpty(Rating))
                {
                    book.Rating = Int32.Parse(Rating);
                }
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return Json("Success");
            }
            return Json("Invalid User!");
        }

        public JsonResult RatingControl(int BookID, int Rating)
        {
            //int bid = Convert.ToInt32(BookID);
            //int rat = Convert.ToInt32(Rating);
            //foreach (Book bo in books)
            //{
            //    db.Entry(bo).State = EntityState.Modified;
            //    db.SaveChanges();
            //}


            return Json("Success!");
        }

        public ActionResult ShareControls()
        {
            string UserName = User.Identity.GetUserName();
            var users = db.Users.Where(x => x.UserName != UserName).ToList();
            List<SharedBooksVM> bol = new List<SharedBooksVM>();
            foreach (ApplicationUser user in users) {
                SharedBooksVM svm = new SharedBooksVM();
                svm.User = user;
                svm.Books = db.Books.Where(x => x.UserID == user.Id).Where(x => x.Shared == true).ToList();
                bol.Add(svm);
            }
            return View(bol);
        }

    }
}



