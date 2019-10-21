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
    public class CarsController : Controller
    {
        private Model1 db = new Model1();

        // GET: Cars
        public ActionResult Index()
        {
            var cars = db.Cars.Include(c => c.Agency).Include(c => c.Model);
            return View(cars.ToList());
        }

        // GET: Cars/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // GET: Cars/Create
        public ActionResult Create()
        {
            if (Session["ID_Staff"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            ViewBag.ID_Agency = new SelectList(db.Agencies, "ID_Agency", "Localization");
            ViewBag.ID_Model = new SelectList(db.Models, "ID_Model", "Brand");

            return View();
        }

        // POST: Cars/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Car,Engine,Category,Capacity,Mileage_Current,Date_MOT_due,ID_Agency,ID_Model")] Car car)
        {
            if (Session["ID_Staff"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            if (ModelState.IsValid)
            {
                HttpPostedFileBase Image = Request.Files["UploadedFile"] as HttpPostedFileBase;
                if (Image != null && Image.ContentLength > 0)
                {
                    int cpt = 0;
                    string ext = System.IO.Path.GetExtension(Image.FileName);
                    string serveurPath = "../Assets/car/1" + ext;

                    while (System.IO.File.Exists(Server.MapPath(serveurPath)))
                    {
                        serveurPath = "../Assets/car/" + cpt.ToString() + "" + ext;
                        cpt++;
                    }

                    Image.SaveAs(Server.MapPath(serveurPath));
                    car.ImgPath = serveurPath;
                }

                db.Cars.Add(car);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ID_Agency = new SelectList(db.Agencies, "ID_Agency", "Localization", car.ID_Agency);
            ViewBag.ID_Model = new SelectList(db.Models, "ID_Model", "Brand", car.ID_Model);
            return View(car);
        }

        // GET: Cars/Edit/5
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
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID_Agency = new SelectList(db.Agencies, "ID_Agency", "Localization", car.ID_Agency);
            ViewBag.ID_Model = new SelectList(db.Models, "ID_Model", "Brand", car.ID_Model);
            return View(car);
        }

        // POST: Cars/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Car car)
        {
            if (Session["ID_Staff"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            if (ModelState.IsValid)
            {
                HttpPostedFileBase Image = Request.Files["UploadedFile"] as HttpPostedFileBase;
                if (Image != null && Image.ContentLength > 0)
                {
                    int cpt = 0;
                    string ext = System.IO.Path.GetExtension(Image.FileName);
                    string serveurPath = "../../Assets/car/1" + ext;

                    while (System.IO.File.Exists(Server.MapPath(serveurPath)))
                    {
                        serveurPath = "../../Assets/car/" + cpt.ToString() + "" + ext;
                        cpt++;
                    }

                    Image.SaveAs(Server.MapPath(serveurPath));
                    car.ImgPath = serveurPath;
                }

                db.Entry(car).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ID_Agency = new SelectList(db.Agencies, "ID_Agency", "Localization", car.ID_Agency);
            ViewBag.ID_Model = new SelectList(db.Models, "ID_Model", "Brand", car.ID_Model);
            return View(car);
        }

        // GET: Cars/Delete/5
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
            Car car = db.Cars.Find(id);
            if (car == null)
            {
                return HttpNotFound();
            }
            return View(car);
        }

        // POST: Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["ID_Staff"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            Car car = db.Cars.Find(id);
            db.Cars.Remove(car);
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
