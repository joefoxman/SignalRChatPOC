using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SignalRChat.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = " Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Chat()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult GetUserList()
        {
            var users = new List<SignalRChat.Models.User>();
            users.Add(new Models.User { Id = 1, Description = "Alex" });
            users.Add(new Models.User { Id = 2, Description = "Joey" });
            users.Add(new Models.User { Id = 3, Description = "Bob" });
            users.Add(new Models.User { Id = 4, Description = "Kate" });
            users.Add(new Models.User { Id = 5, Description = "Tom" });
            return PartialView("UserList", users);
        }
    }
}