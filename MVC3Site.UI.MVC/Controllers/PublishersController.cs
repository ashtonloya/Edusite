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
    public class PublishersController : Controller
    {
        private BookStorePlusEntities db = new BookStorePlusEntities();

        #region AJAX Operations
            #region AJAX Delete
            [AcceptVerbs(HttpVerbs.Post)]
            public JsonResult AjaxDelete(int id)
            {
                //retrieve the publisher from the db
                Publisher pub = db.Publishers.Find(id);

                //remove the publisher from EF
                db.Publishers.Remove(pub);

                //save the changes to the db
                db.SaveChanges();

                //create a message to return (via json) as the status message
                var message = $"Deleted Publisher {pub.PublisherName} from the database!";
                return Json(new {
                    //declare on the fly = value declared above
                    id = id,
                    message = message
                });
            }

        #endregion

            #region AJAX Details
            [HttpGet]
            public PartialViewResult PublisherDetails(int id)
            {
                //retrieve the publisher by the id
                Publisher pub = db.Publishers.Find(id);

                //return a partial view to the browser with the publisher object
                return PartialView(pub);
            }

        #endregion

            #region AJAX Create -  (add)
            //Add a publisher to the database via ajax and return a json result
            [HttpPost]
            [ValidateAntiForgeryToken]
            public JsonResult AjaxCreate(Publisher pub)
            {
                //default the publisher to active (removed the checkbox from the UI create form)
                pub.IsActive = true;

                db.Publishers.Add(pub);
                db.SaveChanges();
                return Json(pub);
            }

        #endregion

        #region Edit - Get (show the form) AND POST (process the form)
        [HttpGet]
        public PartialViewResult PublisherEdit(int id)
        {
            Publisher pub = db.Publishers.Find(id);
            return PartialView(pub);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult AjaxEdit(Publisher pub)
        {
            db.Entry(pub).State = EntityState.Modified;
            db.SaveChanges();
            return Json(pub);
        }

            #endregion
        #endregion


        // GET: Publishers
        public ActionResult Index()
        {
            return View(db.Publishers.ToList());
        }

        // GET: Publishers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = db.Publishers.Find(id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            return View(publisher);
        }

        // GET: Publishers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Publishers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PublisherID,PublisherName,City,State,IsActive")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                db.Publishers.Add(publisher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(publisher);
        }

        // GET: Publishers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = db.Publishers.Find(id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            return View(publisher);
        }

        // POST: Publishers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PublisherID,PublisherName,City,State,IsActive")] Publisher publisher)
        {
            if (ModelState.IsValid)
            {
                db.Entry(publisher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(publisher);
        }

        // GET: Publishers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Publisher publisher = db.Publishers.Find(id);
            if (publisher == null)
            {
                return HttpNotFound();
            }
            return View(publisher);
        }

        // POST: Publishers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Publisher publisher = db.Publishers.Find(id);
            db.Publishers.Remove(publisher);
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
