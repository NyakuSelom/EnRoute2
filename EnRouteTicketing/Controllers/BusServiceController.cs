using EnRouteTicketing.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EnRouteTicketing.Controllers
{
    public class BusServiceController : Controller
    {

        EnRouteTicketingContext db = new EnRouteTicketingContext();
        // GET: BusService
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                 string email = User.Identity.GetUserName();
                ViewBag.mail = email;
                //  var id = from s in db.BusServices where s.Email == email select s.BusServiceID

               

                var record = db.BusServices.FirstOrDefault(x =>x.Email == email);
                if (record != null)
                {
                    ViewBag.ServiceName = record.ServiceName;
                    ViewBag.ContactNo = record.ServiceContactNo;
                    ViewBag.ServiceID = record.BusServiceID;

                    //List<Terminal> terms = db.Terminals.Where(t => t.BusServiceID == record.BusServiceID).ToList();
                    //ViewBag.terminallist = new SelectList(terms, "TerminalID", "TerminalName");
                    //var terminallist = from t in db.Terminals where t.BusServiceID == record.BusServiceID select t;
                    //ViewData["terminalname"] = new SelectList(terminallist, "TerminalID", "TerminalName");



                    //return PartialView("../Bus/_addBus");
                    return View();
                }
            }
            //else
          return RedirectToAction("Index", "Home");

         
           
            
        }

        // GET: BusService/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: BusService/Create
        

        // POST: BusService/Create
        [HttpGet]
        public ActionResult Create( string email, string serviceName, string serviceContactNo, string serviceRegistrationNo, DateTime dateAdded, bool status )
        {
            try
            {

                // TODO: Add insert logic here

                BusService bs = new BusService
                {
                    ServiceName = serviceName,
                    Email = email,
                    ServiceContactNo = serviceContactNo,
                    ServiceRegistrationNo = serviceRegistrationNo,
                    DateAdded = dateAdded,
                    Status = status


                };

                db.BusServices.Add(bs);
                db.SaveChanges();

                db.Entry(bs).GetDatabaseValues();
                
                int id = bs.BusServiceID;
                return RedirectToAction($"Index/{id}");
            }
            catch
            {
                return View();
            }
        }

        // GET: BusService/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: BusService/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: BusService/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: BusService/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
