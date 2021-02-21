using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcWebStore.Models;
using MvcWebStore.ViewModels;

namespace MvcWebStore.Controllers
{
    public class ShoppingCartController : Controller
    {

        WebStoreEntities storeDB = new WebStoreEntities();

        public ActionResult Index()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            var viewModel = new ShoppingCartViewModel
            {
                CartItems = cart.GetCartItems(),
                CartTotal = cart.GetTotal()
            };

            return View(viewModel);
        }

        public ActionResult AddToCart(int id)
        {
            var addedAlbum = storeDB.Products.Single(product => product.ProductID == id);
            var cart = ShoppingCart.GetCart(this.HttpContext);
            cart.AddToCart(addedAlbum);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int id)
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            string albumName = storeDB.Carts.Single(item => item.RecordId == id).Product.ProductName;

            int itemCount = cart.RemoveFromCart(id);

            var results = new ShoppingCartRemoveViewModel
            {
                Message = Server.HtmlEncode(albumName) + " has been removed from your shopping cart.",
                CartTotal = cart.GetTotal(),
                CartCount = cart.GetCount(),
                ItemCount = itemCount,
                DeleteId = id
            };

            return Json(results);
        }

        [ChildActionOnly] 
        public ActionResult CartLink()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewData["CartCount"] = cart.GetCount();

            return PartialView("CartLink");
        }

        [ChildActionOnly]
        public ActionResult CartSummary()
        {
            var cart = ShoppingCart.GetCart(this.HttpContext);

            ViewBag.numberOfItems = cart.GetCount().ToString();
            ViewBag.total = cart.GetTotal().ToString();

            return PartialView("CartSummary");
        }
    }
}