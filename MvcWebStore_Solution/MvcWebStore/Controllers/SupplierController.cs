using MvcWebStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcWebStore.Controllers
{
    public class SupplierController : Controller
    {
        private WebStoreEntities db = new WebStoreEntities();

        [ChildActionOnly]
        public ActionResult SupplierMenu()
        {
            var suppliers = db.Suppliers.ToList();
            return PartialView(suppliers);
        }


        public ActionResult ProductBySupplier(string Supplier)
        {
            var supplierModel = 
                db.Suppliers.Include("Products").Single(c => c.CompanyName == Supplier);

            return View(supplierModel);
        }
    }
}