using EnRouteTicketing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnRouteTicketing.Controllers
{
    public class HomeController : Controller
    {
        EnRouteTicketingContext db = new EnRouteTicketingContext();

        public ActionResult Index()
        {
            var terminallist = (from t in db.Terminals select t.Location).Distinct();
            ViewData["locationname"] = new SelectList(terminallist);
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