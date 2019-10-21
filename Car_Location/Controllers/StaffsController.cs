using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Car_Location.Models;

namespace Car_Location.Controllers
{
    public class StaffsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Staffs
        public ActionResult Index()
        {
            if (Session["ID_Staff"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            var staffs = db.Staffs.Include(s => s.Agency);
            return View(staffs.ToList());
        }

        // GET: Staffs/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ID_Staff"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // GET: Staffs/Create
        public ActionResult Create()
        {
            if (Session["ID_Staff"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            ViewBag.ID_Agency = new SelectList(db.Agencies, "ID_Agency", "Localization");
            return View();
        }

        // POST: Staffs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterStaff register)
        {
            if (Session["ID_Staff"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            if (ModelState.IsValid)
            {
                SHA256 sha256Hash = SHA256.Create();
                register.PasswordHash = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(register.Password));
                Staff staff = new Staff(register as Staff);

                db.Staffs.Add(staff);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Agency = new SelectList(db.Agencies, "ID_Agency", "Localization", register.ID_Agency);
            return View(register);
        }

        // GET: Staffs/Edit/5
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
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            EditStaff editStaff = new EditStaff(staff);

            ViewBag.ID_Agency = new SelectList(db.Agencies, "ID_Agency", "Localization", editStaff.ID_Agency);
            return View(editStaff);
        }

        // POST: Staffs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditStaff editStaff)
        {
            if (Session["ID_Staff"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            if (ModelState.IsValid)
            {
                if (editStaff.Password != null)
                {
                    SHA256 sha256Hash = SHA256.Create();
                    editStaff.PasswordHash = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(editStaff.Password));
                }

                Staff staff = new Staff(editStaff as Staff);

                db.Entry(staff).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Agency = new SelectList(db.Agencies, "ID_Agency", "Localization", editStaff.ID_Agency);
            return View(editStaff);
        }

        // GET: Staffs/Delete/5
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
            Staff staff = db.Staffs.Find(id);
            if (staff == null)
            {
                return HttpNotFound();
            }
            return View(staff);
        }

        // POST: Staffs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["ID_Staff"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            Staff staff = db.Staffs.Find(id);
            db.Staffs.Remove(staff);
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
