using EnRouteTicketing.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnRouteTicketing.Controllers
{
    
    public class CommuterController : Controller
    {
        EnRouteTicketingContext db = new EnRouteTicketingContext();

        // GET: Commuter
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string email = User.Identity.GetUserName();
                ViewBag.mail = email;
                //  var id = from s in db.BusServices where s.Email == email select s.BusServiceID



                var record = db.Commuters.FirstOrDefault(x => x.Email == email);
                if (record != null)
                {
                    ViewBag.commuterName = record.UserName;
                    ViewBag.contactNo = record.PhoneNumber;

                    //List<Terminal> terms = db.Terminals.Where(t => t.BusServiceID == record.BusServiceID).ToList();
                    //ViewBag.terminallist = new SelectList(terms, "TerminalID", "TerminalName");
                    //var terminallist = from t in db.Terminals where t.BusServiceID == record.BusServiceID select t;
                    //ViewData["terminalname"] = new SelectList(terminallist, "TerminalID", "TerminalName");



                    //return PartialView("../Bus/_addBus");
                    return View();
                }

            }
            return RedirectToAction("Index", "Home");
        }

        // GET: Commuter/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Commuter/Create
        
        public ActionResult CompleteRegistration()
        {
            return View();
        }

        // POST: Commuter/Create
        [HttpPost]
        public ActionResult CompleteRegistration(Commuter passenger, FormCollection collection)
        {
            //try
            //{
                if (User.Identity.IsAuthenticated)
                {


                    string email = User.Identity.GetUserName();
                                     
                   

                        Commuter com = new Commuter()
                        {
                            Email = email,
                            UserName = passenger.UserName,
                            PhoneNumber = passenger.PhoneNumber,
                            


                        };
                        db.Commuters.Add(com);
                        db.SaveChanges();

                        //  return Content($"{ServiceID}");


                

                    return RedirectToAction("Index", "Commuter");

            }

            //}
            //catch
            //{
            //    return View();
            //}
            return Content("No Record found");
        }

        // GET: Commuter/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Commuter/Edit/5
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

        // GET: Commuter/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Commuter/Delete/5
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
