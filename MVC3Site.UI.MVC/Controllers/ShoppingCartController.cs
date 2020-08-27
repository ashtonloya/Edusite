using MVC3Site.DATA.EF;//Domain (entity) models
using MVC3Site.UI.MVC.Models;//ShoppingCartViewModel
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC3Site.UI.MVC.Controllers
{
    public class ShoppingCartController : Controller
    {
        // GET: ShoppingCart
        public ActionResult Index()
        {
            //Create a local version of the shopping cart from the session (global) version
            //if the value is null or the count is 0, create an empty instance and provide no cart items verbiage.
            var shoppingCart = (Dictionary<int, ShoppingCartViewModel>)Session["cart"];

            if (shoppingCart == null || shoppingCart.Count == 0)
            {
                shoppingCart = new Dictionary<int, ShoppingCartViewModel>();
                ViewBag.Message = "There are no books in your cart.";
            }
            else
            {
                ViewBag.Message = null;
            }

            //return the view with the shopping cart (either full OR empty)
            return View(shoppingCart);
        }

        [HttpPost]
        public ActionResult UpdateCart(int bookID, int qty)
        {
            //retrieve the cart from session and assign it to our local dictionary
            var shoppingCart = (Dictionary<int, ShoppingCartViewModel>)Session["cart"];

            //might add logic to check qty for 0 - then remove from the cart.
            //update the qty in the local storage (dictionary)
            shoppingCart[bookID].Qty = qty;

            //return the local cart to session
            Session["cart"] = shoppingCart;

            //if logic to display a message if they update and no items are in their cart
            if (shoppingCart.Count == 0)
            {
                //if there are no items in dictionary, set the value of the global variable to null 
                //for processing when the runtime calls the index action.
                Session["cart"] = null;
                ViewBag.Message = "There are no books in your cart";
            }
            //The code in the index action will NOT RUN - the cart totals will NOT change
            //in fact it could error becuase no shopping cart dictionary was sent to the view
            //return View("Index");

            //return them to the index running the logic in the Index Action - returns the UPDATED View
            //with NEW information
            return RedirectToAction("Index");
        }

        public ActionResult RemoveFromCart(int id)
        {
            //get cart out of session and  into local dictionary
            var shoppingCart = (Dictionary<int, ShoppingCartViewModel>)Session["cart"];

            //call the remove() from the dictionary class
            shoppingCart.Remove(id);

            //setup the viewbag for no results
            if (shoppingCart.Count == 0)
            {
                ViewBag.Message = "There are no books in your cart.";
                Session["cart"] = null;
            }

            //return the shopping cart view
            return RedirectToAction("Index");
        }
    }
}