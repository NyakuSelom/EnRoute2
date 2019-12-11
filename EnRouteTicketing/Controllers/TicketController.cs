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
    public class TicketController : Controller
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

                    var buslist = from b in db.Buses where b.BusServiceID == record.BusServiceID select b;
                    ViewData["licenseno"] = new SelectList(buslist, "BusID", "LicenseNo");
                }
            }
            return PartialView("_generateTicket");
        }

        // GET: Ticket
        public ActionResult TicketFinder(string phone, string Location, string Destination, string TravelDate)
        {

            if (TravelDate == "") { 

                int tryid = 1;
                var tryq = from t in db.Tickets where t.DepartTerminalID == tryid select new { t.BusID, t.BusServiceID, t.DepartTerminalID, t.ArriveTerminalID, t.Price, t.TravelDate, t.DepartureTime };
                var fromID = (from t in db.Terminals where t.Location == Location select t.TerminalID).ToList();
                var toID = (from t in db.Terminals where t.Location == Destination select t.TerminalID).ToList();
                
                List<Ticket> availabletickets = new List<Ticket>();
                foreach (var sta in fromID)
                {
                    foreach (var des in toID)
                    {
                        //var ticketgroup = (from t in db.Tickets where t.DepartTerminalID == sta && t.ArriveTerminalID == des && t.Status == false select t).FirstOrDefault();
                        //if (ticketgroup != null)
                        //{
                        //    availabletickets.Add(ticketgroup);
                        //}

                        var ticketgroup = (from t in db.Tickets where t.DepartTerminalID == sta && t.ArriveTerminalID == des && t.Status == false select t).ToList();
                        var avlist = (ticketgroup.GroupBy(t =>new { t.TravelDate,t.BusID}).Select(t => t.OrderByDescending(b => b.DepartureTime).FirstOrDefault())).ToList();
                        if (ticketgroup != null)
                        {
                            foreach (var item in avlist)
                            {
                                availabletickets.Add(item);
                            }
                           
                        }
                    }
                }

               

               // return PartialView("_testPartial");

                return PartialView("_tickefinder", availabletickets);
            }
            return PartialView("_testPartial");
        }

        // GET: Ticket/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Ticket/Create
        public ActionResult Create()
        {
            return View();
        }

        public int RandomGen()
        {
            Random random = new Random();
            return random.Next(10000,99999);
        }

        // POST: Ticket/Create
        [HttpPost]
        public ActionResult Create(Ticket gen,FormCollection collection)
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {


                    string email = User.Identity.GetUserName();



                    var record = db.BusServices.FirstOrDefault(x => x.Email == email);
                    if (record != null)
                    {


                        var ServiceID = record.BusServiceID;
                        var nooftickets = (from b in db.Buses where b.BusID == gen.BusID select b.NoofSeats).SingleOrDefault();

                        var from = (from t in db.Terminals where t.TerminalID == gen.DepartTerminalID select t.Location).SingleOrDefault();
                        var to = (from t in db.Terminals where t.TerminalID == gen.ArriveTerminalID select t.Location).SingleOrDefault();


                        for (int i = 0; i < nooftickets; i++)
                        {
                            Ticket tk = new Ticket
                            {

                                TicketNumber = $"{gen.TravelDate.ToString("dd.MM.yyyy")}-{from.Substring(0, 3)}-to-{to.Substring(0, 3)}-{i + 1}-{RandomGen()}",
                                SeatNo = i + 1,
                                TravelDate = gen.TravelDate,
                                DepartureTime = gen.DepartureTime,
                                Price = gen.Price,
                                Status = gen.Status,
                                BusServiceID = ServiceID,
                                BusID = gen.BusID,
                                DepartTerminalID =gen.DepartTerminalID,
                                ArriveTerminalID = gen.ArriveTerminalID,


                                //return Content($"-{gen.TravelDate.ToString()}-{from.Substring(0, 3)}-to-{to.Substring(0, 3)}-{i + 1}");
                            };
                            db.Tickets.Add(tk);
                            db.SaveChanges();
                        }
                       
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
                return Content("Unsucessful");
    }
}

        // GET: Ticket/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Ticket/Edit/5
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

        // GET: Ticket/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Ticket/Delete/5
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
