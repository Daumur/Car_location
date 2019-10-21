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
    public class ReturnsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Returns
        public ActionResult Index()
        {
            if (Session["ID_Staff"] == null && Session["ID_Customer"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            else if (Session["ID_Staff"] != null)
            {
                var returns1 = db.Returns.Include(c => c.Agency).Include(c => c.Hire_Agreement);
                return View(returns1.ToList());
            }

            int ID_Customer = Convert.ToInt32(Session["ID_Customer"]);
            var returns = db.Returns.Include(c => c.Agency).Include(c => c.Hire_Agreement).Where(c=> c.Hire_Agreement.ID_Customer == ID_Customer);
            return View(returns.ToList());
        }

        // GET: Returns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Return @return = db.Returns.Find(id);
            if (@return == null)
            {
                return HttpNotFound();
            }

            if (Session["ID_Staff"] == null && Convert.ToInt32(Session["ID_Customer"]) != @return.Hire_Agreement.ID_Customer)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }

            return View(@return);
        }

        // GET: Returns/Create
        public ActionResult Create()
        {
            if (Session["ID_Staff"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            ViewBag.ID_New_Agency = new SelectList(db.Agencies, "ID_Agency", "Localization");
            ViewBag.ID_Hire_Agreement = new SelectList(db.Hire_Agreement, "ID_Hire_Agreement", "Info");
            return View();
        }

        // POST: Returns/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Return,ID_New_Agency,Date_Checked,New_Mileage_Current,New_Fault_Description,ID_Hire_Agreement")] Return @return)
        {
            if (Session["ID_Staff"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            if (ModelState.IsValid)
            {
                db.Returns.Add(@return);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_New_Agency = new SelectList(db.Agencies, "ID_Agency", "Localization", @return.ID_New_Agency);
            ViewBag.ID_Hire_Agreement = new SelectList(db.Hire_Agreement, "ID_Hire_Agreement", "Info", @return.ID_Hire_Agreement);
            return View(@return);
        }

        // GET: Returns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["ID_Staff"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Return @return = db.Returns.Find(id);
            if (@return == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_New_Agency = new SelectList(db.Agencies, "ID_Agency", "Localization", @return.ID_New_Agency);
            ViewBag.ID_Hire_Agreement = new SelectList(db.Hire_Agreement, "ID_Hire_Agreement", "Info", @return.ID_Hire_Agreement);
            return View(@return);
        }

        // POST: Returns/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Return,ID_New_Agency,Date_Checked,New_Mileage_Current,New_Fault_Description,ID_Hire_Agreement")] Return @return)
        {
            if (Session["ID_Staff"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            if (ModelState.IsValid)
            {
                db.Entry(@return).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_New_Agency = new SelectList(db.Agencies, "ID_Agency", "Localization", @return.ID_New_Agency);
            ViewBag.ID_Hire_Agreement = new SelectList(db.Hire_Agreement, "ID_Hire_Agreement", "Info", @return.ID_Hire_Agreement);
            return View(@return);
        }

        // GET: Returns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["ID_Staff"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Return @return = db.Returns.Find(id);
            if (@return == null)
            {
                return HttpNotFound();
            }
            return View(@return);
        }

        // POST: Returns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["ID_Staff"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            Return @return = db.Returns.Find(id);
            db.Returns.Remove(@return);
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
