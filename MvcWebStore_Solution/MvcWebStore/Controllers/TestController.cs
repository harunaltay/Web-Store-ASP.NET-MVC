using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcWebStore.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            //Roles.CreateRole("Administrator");
            //Roles.CreateRole("SalesManager");
            //Roles.CreateRole("Customer");

            //Membership.CreateUser("admin", "admin123", "h.altay@gmail.com");
            //Membership.CreateUser("user", "user123", "h.altay2@gmail.com");

            //Roles.AddUserToRole("admin", "Administrator"); // bende bu çalışmadı! ilkinde
            //Roles.AddUserToRole("user", "Customer"); // bende bu çalışmadı! ilkinde

            return View();
        }
    }
}