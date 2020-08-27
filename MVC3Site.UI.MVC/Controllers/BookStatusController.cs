using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC3Site.DATA.EF;

namespace MVC3Site.UI.MVC.Controllers
{
    [Authorize(Roles ="Admin")] //--The link is hidden to ALL users except admin, so lock down ALL controller actions/views
    //by using the Authorize Attribute at the Controller level. This will secure ALL Actions in the controller.
    //See Books Controller for Multiple Role Access
    public class BookStatusController : Controller
    {
        private BookStorePlusEntities db = new BookStorePlusEntities();

        // GET: BookStatus
        public ActionResult Index()
        {
            return View(db.BookStatuses.ToList());
        }

        // GET: BookStatus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookStatus bookStatus = db.BookStatuses.Find(id);
            if (bookStatus == null)
            {
                return HttpNotFound();
            }
            return View(bookStatus);
        }

        // GET: BookStatus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BookStatus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookStatusID,BookStatusName,Notes")] BookStatus bookStatus)
        {
            if (ModelState.IsValid)
            {
                db.BookStatuses.Add(bookStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bookStatus);
        }

        // GET: BookStatus/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookStatus bookStatus = db.BookStatuses.Find(id);
            if (bookStatus == null)
            {
                return HttpNotFound();
            }
            return View(bookStatus);
        }

        // POST: BookStatus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookStatusID,BookStatusName,Notes")] BookStatus bookStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookStatus);
        }

        // GET: BookStatus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BookStatus bookStatus = db.BookStatuses.Find(id);
            if (bookStatus == null)
            {
                return HttpNotFound();
            }
            return View(bookStatus);
        }

        // POST: BookStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BookStatus bookStatus = db.BookStatuses.Find(id);
            db.BookStatuses.Remove(bookStatus);
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
