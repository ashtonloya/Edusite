using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC3Site.DATA.EF;
using PagedList;
using PagedList.Mvc;
using MVC3Site.UI.MVC.Models;//ShoppingCartViewModel

namespace MVC3Site.UI.MVC.Controllers
{
    //[Authorize(Roles ="Admin")] //--Disallows everyone EXCEPT admin
    //[Authorize]//-No Anonymous users are allowed to ANY of the actions/views
    public class BooksController : Controller
    {
        private BookStorePlusEntities db = new BookStorePlusEntities();

        #region AddToCart (Shopping Cart Functionality)
        [HttpPost]
        public ActionResult AddToCart(int qty, int bookID)
        {
            //Create and Empty shopping cart (local version)
            Dictionary<int, ShoppingCartViewModel> shoppingCart = null;

            //check the session variable that represents a cart with items in it
            //if the global cart has "stuff" in it, then we will assign its value to our local version.
            if (Session["cart"] != null)
            {
                shoppingCart = (Dictionary<int, ShoppingCartViewModel>)Session["cart"];
            }
            //else - create an empty local version
            else
            {
                shoppingCart = new Dictionary<int, ShoppingCartViewModel>();
            }

            //Get the product being added (using the id)
            Book product = db.Books.Where(b => b.BookID == bookID).FirstOrDefault();

            //if the product is null - redirect to the books index
            if (product == null)
            {
                return RedirectToAction("Index");
            }
            //else - Add the product and quantity to the cart
            else
            {
                //Create the ShoppingCartViewModel object
                ShoppingCartViewModel item = new ShoppingCartViewModel(qty, product);

                //if the shoppingcart already has a product with that id, increase the qty (local)
                if (shoppingCart.ContainsKey(product.BookID))
                {
                    shoppingCart[product.BookID].Qty += qty;
                }
                //else add it to the cart (local)
                else
                {
                    shoppingCart.Add(product.BookID, item);
                }

                //update the global cart (session) with the NEW information
                Session["cart"] = shoppingCart;

            }
            //send the user to the shopping cart landing page (index)
            return RedirectToAction("Index", "ShoppingCart");
        }


        #endregion

        //[AllowAnonymous]//ONLY allows Anonymous users no specific roles - If the Admin authorize is up top, then
        //customer Role people will NOT have access
        // GET: Books
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.BookRoyalty).Include(b => b.BookStatus).Include(b => b.Genre).Include(b => b.Publisher);
            return View(books.ToList());
        }

        //New Action to handle paging by itself.
        //Default to page 1 in the parameter set
        public ActionResult BooksPaging(int page=1)
        {
            //set the page size - this could be done with a querystring and let the user select the page size
            //(how many rows in the table per page)
            int pageSize = 7;

            //This is the same query used in the Index Action.
            //for PagedList to work, the OrderBy() (or keyword depending on your syntax) MUST be included in the query
            //The pagedList is dependent on the results being ordered.
            var books = db.Books.Include(b => b.BookRoyalty)
                                .Include(b => b.BookStatus)
                                .Include(b => b.Genre)
                                .Include(b => b.Publisher)
                                .OrderBy(b=>b.BookTitle);//***** ORDERBY for Paging *****


            return View(books.ToPagedList(page,pageSize));
        }
        // GET: Books/Details/5
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

        [Authorize(Roles ="Admin")]
        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.BookID = new SelectList(db.BookRoyalties, "BookID", "BookID");
            ViewBag.BookStatusID = new SelectList(db.BookStatuses, "BookStatusID", "BookStatusName");
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName");
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "PublisherName");
            return View();
        }

        [Authorize(Roles = "Admin")]
        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BookID,ISBN,BookTitle,Description,GenreID,Price,UnitsSold,PublishDate,PublisherID,BookImage,IsSiteFeature,IsGenreFeature,BookStatusID")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.BookID = new SelectList(db.BookRoyalties, "BookID", "BookID", book.BookID);
            ViewBag.BookStatusID = new SelectList(db.BookStatuses, "BookStatusID", "BookStatusName", book.BookStatusID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", book.GenreID);
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "PublisherName", book.PublisherID);
            return View(book);
        }

        [Authorize(Roles = "Admin")]
        // GET: Books/Edit/5
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
            ViewBag.BookID = new SelectList(db.BookRoyalties, "BookID", "BookID", book.BookID);
            ViewBag.BookStatusID = new SelectList(db.BookStatuses, "BookStatusID", "BookStatusName", book.BookStatusID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", book.GenreID);
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "PublisherName", book.PublisherID);
            return View(book);
        }

        [Authorize(Roles = "Admin")]
        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookID,ISBN,BookTitle,Description,GenreID,Price,UnitsSold,PublishDate,PublisherID,BookImage,IsSiteFeature,IsGenreFeature,BookStatusID")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BookID = new SelectList(db.BookRoyalties, "BookID", "BookID", book.BookID);
            ViewBag.BookStatusID = new SelectList(db.BookStatuses, "BookStatusID", "BookStatusName", book.BookStatusID);
            ViewBag.GenreID = new SelectList(db.Genres, "GenreID", "GenreName", book.GenreID);
            ViewBag.PublisherID = new SelectList(db.Publishers, "PublisherID", "PublisherName", book.PublisherID);
            return View(book);
        }

        [Authorize(Roles = "Admin")]
        // GET: Books/Delete/5
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

        [Authorize(Roles = "Admin")]
        // POST: Books/Delete/5
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
    }
}
