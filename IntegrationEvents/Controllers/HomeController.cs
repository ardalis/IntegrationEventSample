using System;
using System.Web;
using System.Web.Mvc;

namespace IntegrationEvents.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(string userName = null)
        {
            if (userName == null) userName = Guid.NewGuid().ToString();
            ViewBag.Title = "Home Page";
            Response.SetCookie(CreateUserCookie(userName));

            return View();
        }

        private HttpCookie CreateUserCookie(string userName)
        {
            HttpCookie StudentCookies = new HttpCookie("UserCookie");
            StudentCookies.Value = userName + ":" + DateTime.Now.ToString();
            StudentCookies.Expires = DateTime.Now.AddHours(1);
            return StudentCookies;
        }
    }
}
