using EnRouteTicketing.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnRouteTicketing.Controllers
{
    public class HomeController : Controller
    {
        EnRouteAppContext db = new EnRouteAppContext();

        public ActionResult Index()
        {
            var terminallist = (from t in db.Terminals select t.Location).Distinct();
            ViewData["locationname"] = new SelectList(terminallist);

            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole("Commuter"))
                {
                    string email = User.Identity.GetUserName();
                    var record = db.Commuters.FirstOrDefault(x => x.Email == email);

                    if (record != null)
                    {
                        ViewBag.phone = record.PhoneNumber;
                    }
                }
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}