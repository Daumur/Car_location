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
    public class AgenciesController : Controller
    {
        private Model1 db = new Model1();

        // GET: Agencies
        public ActionResult Index()
        {
            return View(db.Agencies.ToList());
        }

        // GET: Agencies/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agency agency = db.Agencies.Find(id);
            if (agency == null)
            {
                return HttpNotFound();
            }
            return View(agency);
        }

        // GET: Agencies/Create
        public ActionResult Create()
        {
            if (Session["ID_Staff"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            return View();
        }

        // POST: Agencies/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Agency,Localization")] Agency agency)
        {
            if (Session["ID_Staff"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            if (ModelState.IsValid)
            {
                db.Agencies.Add(agency);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(agency);
        }

        // GET: Agencies/Edit/5
        public ActionResult Edit(int? id)
        {
            if(Session["ID_Staff"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Agency agency = db.Agencies.Find(id);
            if (agency == null)
            {
                return HttpNotFound();
            }
            return View(agency);
        }

        // POST: Agencies/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Agency,Localization")] Agency agency)
        {
            if (Session["ID_Staff"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            if (ModelState.IsValid)
            {
                db.Entry(agency).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(agency);
        }

        // GET: Agencies/Delete/5
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
            Agency agency = db.Agencies.Find(id);
            if (agency == null)
            {
                return HttpNotFound();
            }
            return View(agency);
        }

        // POST: Agencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["ID_Staff"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            Agency agency = db.Agencies.Find(id);
            db.Agencies.Remove(agency);
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
