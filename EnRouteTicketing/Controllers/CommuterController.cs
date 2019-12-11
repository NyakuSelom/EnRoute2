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
            return View();
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


                

                    return RedirectToAction("Commuter", "Index");

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
