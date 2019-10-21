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
    public class CustomersController : Controller
    {
        private Model1 db = new Model1();

        // GET: Customers
        public ActionResult Index()
        {
            if (Session["ID_Staff"] == null)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["ID_Staff"] == null && Convert.ToInt32(Session["ID_Customer"]) != id)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login login)
        {
            if (ModelState.IsValid)
            {
                Session["ID_Customer"] = login.ID_Customer;
                Session["ID_Staff"] = login.ID_Staff;
                return RedirectToAction("Index");
            }

            return View(login);
        }

        // GET: Disconnect
        public ActionResult Disconnect()
        {
            Session["ID_Customer"] = null;
            Session["ID_Staff"] = null;
            return RedirectToAction("Index", "Home");
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RegisterCustomer register)
        {
            if (ModelState.IsValid)
            {
                SHA256 sha256Hash = SHA256.Create();
                register.PasswordHash = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(register.Password));
                Customer customer = new Customer(register as Customer);

                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(register);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                id = Convert.ToInt32(Session["ID_Customer"]);
            }
            if (Session["ID_Staff"] == null && Convert.ToInt32(Session["ID_Customer"]) != id)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if(customer == null)
            {
                return HttpNotFound();
            }

            EditCustomer editCustomer = new EditCustomer(customer);
            return View(editCustomer);
        }

        // POST: Customers/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCustomer editCustomer)
        {
            if (Session["ID_Staff"] == null && Convert.ToInt32(Session["ID_Customer"]) != editCustomer.ID_Customer)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            if (ModelState.IsValid)
            {
                if (editCustomer.Password != null)
                {
                    SHA256 sha256Hash = SHA256.Create();
                    editCustomer.PasswordHash = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(editCustomer.Password));
                }
                
                Customer customer = new Customer(editCustomer as Customer);
                db.Entry(customer).State = EntityState.Modified;

                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(editCustomer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["ID_Staff"] == null && Convert.ToInt32(Session["ID_Customer"]) != id)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["ID_Staff"] == null && Convert.ToInt32(Session["ID_Customer"]) != id)
            {
                return Redirect(Request.UrlReferrer.ToString());
            }
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
