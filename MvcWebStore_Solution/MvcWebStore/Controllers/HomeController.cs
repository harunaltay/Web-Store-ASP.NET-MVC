using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcWebStore.Models;
using MvcWebStore.ViewModels;

namespace MvcWebStore.Controllers
{
    public class HomeController : Controller
    {
        private WebStoreEntities db = new WebStoreEntities();

        public ActionResult Index()
        {
            // We assume new products have higher ProductID.
            List<Product> lastSixProducts = db.Products.OrderByDescending(p => p.ProductID).Take(6).ToList();

            Random random = new Random();
            int skip = random.Next(0, db.Products.Count());
            List<Product> randomThreeProducts = db.Products.OrderBy(p=>p.ProductID).Skip(skip).Take(3).ToList();

            // eski
            //var randomThreeProducts = db.Products.Take(3).ToList();

            var viewModel = new Last6_and_Random3_Products
            {
                Last6Products = lastSixProducts,
                Random3Products = randomThreeProducts
            };

            return View(viewModel);
        }
    }
}