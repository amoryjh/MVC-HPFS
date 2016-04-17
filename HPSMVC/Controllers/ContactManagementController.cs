using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HPSMVC.Models;
using HPSMVC.DAL;

namespace HPSMVC.Controllers
{
    public class ContactManagementController : Controller
    {
        private HPSMVCEntities db = new HPSMVCEntities();

        // GET: /ContactManagement/
        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            return View(db.Contacts.ToList());
        }

        public ActionResult Index()
        {
            return View(db.Contacts.ToList());
        }

        // GET: /ContactManagement/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /ContactManagement/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Address,City,Province,PostalCode,Telephone,Fax,Hours,Message")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                db.Contacts.Add(contact);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contact);
        }

        // GET: /ContactManagement/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: /ContactManagement/Edit/5
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Address,City,Province,PostalCode,Telephone,Fax,Hours,Message")] Contact contact)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(contact).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["ValidationMessage"] = "Contact Information Successfully Edited!";
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["ValidationMessage"] = "Error: Contact Information Not Successfully Edited!";
                    return RedirectToAction("Admin");
                }
            }
            return View(contact);
        }

        // GET: /ContactManagement/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contact contact = db.Contacts.Find(id);
            if (contact == null)
            {
                return HttpNotFound();
            }
            return View(contact);
        }

        // POST: /ContactManagement/Delete/5
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contact contact = db.Contacts.Find(id);
            db.Contacts.Remove(contact);
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
