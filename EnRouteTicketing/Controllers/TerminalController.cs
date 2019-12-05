using EnRouteTicketing.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EnrouteBusTicketing.Controllers
{
    public class TerminalController : Controller
    {
        EnRouteTicketingContext db = new EnRouteTicketingContext();


        public ActionResult LoadPartialView()
        {

            List<string> Locationslisted = new List<string>() { "Accra", "Kasoa", "Tema", "CapeCoast", "Secondi", "Takoradi", "Techiman", "Koforidua", "Sunyayni", "Kumasi", "Obuasi", "Tamale", "Yendi", "Bolga", "Wa", "Navrongo" };
            Locationslisted.Sort();

            ViewBag.locationlist = new SelectList(Locationslisted);
            return PartialView("_AddTerminal");
        }

        // GET: Terminal
        public ActionResult Index()
        {
            return View();
        }

        // GET: Terminal/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Terminal/Create
        public ActionResult Create()
        {
           

                    ViewBag.mypartial = "_AddTerminal";
                    return View("Index");
    
        }

        // POST: Terminal/Create
        [HttpPost]
        public ActionResult Create( Terminal term ,FormCollection collection)
        {

            //return Content(term.Location);
            List<string> Locationslisted = new List<string>() { "Accra", "Kasoa", "Tema", "CapeCoast", "Secondi", "Takoradi", "Techiman", "Koforidua", "Sunyayni", "Kumasi", "Obuasi", "Tamale", "Yendi", "Bolga", "Wa", "Navrongo" };
            Locationslisted.Sort();
           

            ViewBag.locationlist = new SelectList(Locationslisted);
            try
            {
                // TODO: Add insert logic here

                if (User.Identity.IsAuthenticated)
                {

                    
                    string email = User.Identity.GetUserName();
                    ViewBag.mail = email;
                    //  var id = from s in db.BusServices where s.Email == email select s.BusServiceID;

                    var record = db.BusServices.FirstOrDefault(x => x.Email == email);
                    if (record != null)
                    {


                        var ServiceID = record.BusServiceID;

                        //return Content($"{ServiceID}");
                        Terminal tt = new Terminal
                        {

                            TerminalName =  term.TerminalName,
                            Location = term.Location,
                            BusServiceID = ServiceID
                            
                        };

                        db.Terminals.Add(tt);
                        db.SaveChanges();


                    }
                else
                {
                    return Content("No Record found");
                }

            }

            return RedirectToAction("Index", "BusService");
        }
            catch
            {
            return View("Index","Home");
        }

    }

        // GET: Terminal/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Terminal/Edit/5
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

        // GET: Terminal/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Terminal/Delete/5
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
