using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVC3Site.DATA.EF;//Access to all of our EntityModels


namespace MVC3Site.UI.MVC.Models
{
    public class ShoppingCartViewModel
    {
        //fields - N/A

        //properties
        public int Qty { get; set; }

        //The use of a complex datatype as a field/property in another class is called...
        //CONTAINMENT - "HAS A" - every shopping cart object HAS A (contains) a Book object.
        public Book Product { get; set; }

        //ctors
        //Fully Qualified ctor to make it easy to store all information
        public ShoppingCartViewModel(int qty, Book product)
        {
            //Property "is assigned the value of" the ctor parameter
            Qty = qty;
            Product = product;
        }

        //methods - N/A

        //toString() - N/A

    }
}