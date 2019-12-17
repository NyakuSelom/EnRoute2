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
        EnRouteAppContext db = new EnRouteAppContext();

        // GET: Commuter
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated)
            {
                string email = User.Identity.GetUserName();
                ViewBag.mail = email;
                



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
        public ActionResult Review(int id)
        {

            
            var model = (from t in db.Tickets where t.TicketID == id select t).FirstOrDefault();
            return View(model);
        }
        [HttpPost]
        public ActionResult Review(int tickid, int ServiceID, string comment,  int rating)
        {

            if (User.Identity.IsAuthenticated)
            {
                string email = User.Identity.GetUserName();
                ViewBag.mail = email;




                var record = db.Commuters.FirstOrDefault(x => x.Email == email);
                if (record != null)
                {
                    var uid = record.CommuterID;
                    
                    var info = (from r in db.Reviews where (r.TicketID == tickid && r.CommuterID==uid )select r).FirstOrDefault();
                    if (info == null)
                    {
                        Review rv = new Review
                        {
                            CommuterID = uid,
                            TicketID = tickid,
                            ReviewMessage = comment,
                            Rating = rating

                        };
                        db.Reviews.Add(rv);
                        db.SaveChanges();
                    }
                    else
                    {
                        var rid = info.ReviewID;
                        var result = db.Reviews.SingleOrDefault(r => r.ReviewID == rid && r.CommuterID == uid);
                        if (result != null)
                        {
                            result.ReviewMessage = comment;
                            result.Rating = rating;

                            db.SaveChanges();

                        }
                    }
                }
            }
            return Content("Hello Woohoo");
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
            try
            {
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

        }
            catch
            {
                return View();
    }
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

        
        public ActionResult  Schedule()
        {
            if (User.Identity.IsAuthenticated)
            {
                string email = User.Identity.GetUserName();
                ViewBag.mail = email;
                //  var id = from s in db.BusServices where s.Email == email select s.BusServiceID



                var record = db.Commuters.FirstOrDefault(x => x.Email == email);
                if (record != null)
                {
                   
                   var contactNo = record.PhoneNumber;
                    var model = (from t in db.Transactions where t.PhoneNumber == contactNo orderby t.TransactionDate descending select t).ToList();
                    return PartialView(model);
                }


            }

          return PartialView();
        }

        // POST: Commuter/Delete/5
        
        public ActionResult Cancel(int id)
        {

            if (User.Identity.IsAuthenticated)
            {
                string email = User.Identity.GetUserName();
               // ViewBag.mail = email;
                //  var id = from s in db.BusServices where s.Email == email select s.BusServiceID



                var record = db.Commuters.FirstOrDefault(x => x.Email == email);
                if (record != null)
                {

                    var phone = record.PhoneNumber;

                    try
                    {
                        var result = db.Tickets.SingleOrDefault(t => t.TicketID == id);
                        if (result != null)
                        {
                            result.Status = false;
                            db.SaveChanges();

                            Sms newsms = new Sms();


                            string message = $"Your Ticket no {result.TicketNumber} from {result.DepartTerminal.Location} to {result.ArriveTerminal.Location} has been cancelled";
                            newsms.SendSms(phone, message);

                            var result2 = db.Transactions.SingleOrDefault(t => t.TicketID == id && t.PhoneNumber == phone && t.status == true);
                            if (result2 != null)
                            {
                                result2.status = false;
                                db.SaveChanges();
                            }


                            }

                        return RedirectToAction("Index");
                    }
                    catch
                    {
                        return Content("Unsuccessful");
                    }
                }
            }
            return Content("Unsuccessful");
       }
    }
}
