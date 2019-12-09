
using EnRouteTicketing.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace EnRouteTicketing.Controllers
{
    public class BusController : Controller
    {
        EnRouteTicketingContext db = new EnRouteTicketingContext();

        public ActionResult LoadPartialView()
        {
            if (User.Identity.IsAuthenticated)
            {
                string email = User.Identity.GetUserName();
                var record = db.BusServices.FirstOrDefault(x => x.Email == email);
                if (record != null)
                {

                    var terminallist = from t in db.Terminals where t.BusServiceID == record.BusServiceID select t;
                    ViewData["terminalname"] = new SelectList(terminallist, "TerminalID", "TerminalName");
                }
            }
            return PartialView("_addBus");
        }
        // GET: Bus
        public ActionResult BusList()
        {
            if (User.Identity.IsAuthenticated)
            {  
                string email = User.Identity.GetUserName();
                var record = db.BusServices.FirstOrDefault(x => x.Email == email);
                if (record != null)
                {

                    var buses = db.Buses.Include(b => b.BusService);
                    buses = buses.Where(b => b.BusServiceID == record.BusServiceID);
                   

                    return PartialView("_buslist", buses.ToList());
                }
            }
            return Content("No buses Added");
        }

        // GET: Bus/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Bus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bus/Create
        [HttpPost]
        public ActionResult Create(Bus abus,FormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
            {


                string email = User.Identity.GetUserName();
                ViewBag.mail = email;
                //  var id = from s in db.BusServices where s.Email == email select s.BusServiceID;

                var record = db.BusServices.FirstOrDefault(x => x.Email == email);
                if (record != null)
                {


                    var ServiceID = record.BusServiceID;

                    Bus bb = new Bus()
                    {
                        LicenseNo = abus.LicenseNo,
                        TerminalID = abus.TerminalID,
                        Busmodel =  abus.Busmodel,
                        BusServiceID = ServiceID,
                        NoofSeats = abus.NoofSeats,



                    };
                    db.Buses.Add(bb);
                    db.SaveChanges();

                    //  return Content($"{ServiceID}");



                }
                else
                {
                    return Content("No Record found");
                }

                return RedirectToAction("Index", "BusService");

            }
            
            }
            catch
            {
                return View();
            }
            return Content("No Record found");
        }

        // GET: Bus/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Bus/Edit/5
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

        // GET: Bus/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Bus/Delete/5
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
