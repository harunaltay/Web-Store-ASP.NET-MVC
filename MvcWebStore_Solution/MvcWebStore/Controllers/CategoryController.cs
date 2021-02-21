using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcWebStore.Models;

namespace MvcWebStore.Controllers
{
    public class CategoryController : Controller
    {
        private WebStoreEntities db = new WebStoreEntities();

        [ChildActionOnly]
        public ActionResult CategoryMenu()
        {
            var categories = db.Categories.ToList();
            return PartialView(categories);
        }


        public ActionResult ProductByCategory(string Category)
        {
            var categoryModel = 
                db.Categories.Include("Products").Single(c => c.CategoryName == Category);

            return View(categoryModel);
        }
    }
}
