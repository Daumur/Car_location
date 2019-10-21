using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Car_Location.Models;

namespace Car_Location.Controllers
{
    public class Hire_AgreementController : Controller
    {
        private Model1 db = new Model1();

        // GET: Hire_Agreement
        public ActionResult Index()
        {
            if (Session["ID_Staff"] == null && Session["ID_Customer"] == null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            else if(Session["ID_Staff"] != null) { 
                var hire_Agreement1 = db.Hire_Agreement.Include(h => h.Car).Include(h => h.Customer);
                return View(hire_Agreement1.ToList());
            }
           
            int ID_Customer = Convert.ToInt32(Session["ID_Customer"]);
            var hire_Agreement = db.Hire_Agreement.Where(h => h.ID_Customer == ID_Customer).Include(h => h.Car).Include(h => h.Customer);
            return View(hire_Agreement.ToList());
            
        }

        // GET: Hire_Agreement/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Hire_Agreement hire_Agreement = db.Hire_Agreement.Find(id);
            if (hire_Agreement == null)
            {
                return HttpNotFound();
            }

            if (Session["ID_Staff"] == null && Convert.ToInt32(Session["ID_Customer"]) != hire_Agreement.ID_Customer)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }

           
            
            return View(hire_Agreement);
        }

        // GET: Hire_Agreement/Create
        public ActionResult Create()
        {
            if (Session["ID_Staff"] == null && Session["ID_Customer"] == null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            ViewBag.ID_Car = new SelectList(db.Cars, "ID_Car", "Model.Brand");
            ViewBag.ID_Customer = new SelectList(db.Customers, "ID_Customer", "Full_Name");
            return View();
        }

        // POST: Hire_Agreement/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Hire_Agreement,Rental_Start_Date,Rental_End_Date,Rental_Mileage,ID_Customer,ID_Car")] Hire_Agreement hire_Agreement)
        {
            if (Session["ID_Staff"] == null && Session["ID_Customer"] == null)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }

            if (ModelState.IsValid)
            {
                db.Hire_Agreement.Add(hire_Agreement);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Car = new SelectList(db.Cars, "ID_Car", "Model.Brand", hire_Agreement.ID_Car);
            ViewBag.ID_Customer = new SelectList(db.Customers, "ID_Customer", "Full_Name", hire_Agreement.ID_Customer);
            return View(hire_Agreement);
        }

        // GET: Hire_Agreement/Edit/5
        public ActionResult Edit(int? id)
        {
           
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hire_Agreement hire_Agreement = db.Hire_Agreement.Find(id);
            if (hire_Agreement == null)
            {
                return HttpNotFound();
            }

            if (Session["ID_Staff"] == null && Convert.ToInt32(Session["ID_Customer"]) != hire_Agreement.ID_Customer)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            ViewBag.ID_Car = new SelectList(db.Cars, "ID_Car", "Model.Brand", hire_Agreement.ID_Car);
            ViewBag.ID_Customer = new SelectList(db.Customers, "ID_Customer", "Full_Name", hire_Agreement.ID_Customer);
            return View(hire_Agreement);
        }

        // POST: Hire_Agreement/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Hire_Agreement,Rental_Start_Date,Rental_End_Date,Rental_Mileage,ID_Customer,ID_Car")] Hire_Agreement hire_Agreement)
        {
            if (Session["ID_Staff"] == null && Convert.ToInt32(Session["ID_Customer"]) != hire_Agreement.ID_Customer)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            if (ModelState.IsValid)
            {
                db.Entry(hire_Agreement).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }


            ViewBag.ID_Car = new SelectList(db.Cars, "ID_Car", "Model.Brand", hire_Agreement.ID_Car);
            ViewBag.ID_Customer = new SelectList(db.Customers, "ID_Customer", "Full_Name", hire_Agreement.ID_Customer);
            return View(hire_Agreement);
        }

        // GET: Hire_Agreement/Delete/5
        public ActionResult Delete(int? id)
        {
          
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hire_Agreement hire_Agreement = db.Hire_Agreement.Find(id);
            if (hire_Agreement == null)
            {
                return HttpNotFound();
            }
            if (Session["ID_Staff"] == null && Convert.ToInt32(Session["ID_Customer"]) != hire_Agreement.ID_Customer)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }
            return View(hire_Agreement);
        }

        // POST: Hire_Agreement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            
            Hire_Agreement hire_Agreement = db.Hire_Agreement.Find(id);
            if (hire_Agreement == null)
            {
                return HttpNotFound();
            }
            if (Session["ID_Staff"] == null && Convert.ToInt32(Session["ID_Customer"]) != hire_Agreement.ID_Customer)
            {
                Response.Redirect(Request.UrlReferrer.ToString());
            }

            db.Hire_Agreement.Remove(hire_Agreement);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
