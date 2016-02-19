using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HPS.ASP.Models;

namespace HPS.ASP.Controllers
{
    public class IndexController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Index/
        public ActionResult Index()
        {
            return View(db.Indices.ToList());
        }

        // GET: /Index/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Index index = db.Indices.Find(id);
            if (index == null)
            {
                return HttpNotFound();
            }
            return View(index);
        }

        // GET: /Index/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Index/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,Title,Content,ButtonText,ButtonLink,Image")] Index index)
        {
            if (ModelState.IsValid)
            {
                db.Indices.Add(index);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(index);
        }

        // GET: /Index/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Index index = db.Indices.Find(id);
            if (index == null)
            {
                return HttpNotFound();
            }
            return View(index);
        }

        // POST: /Index/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,Title,Content,ButtonText,ButtonLink,Image")] Index index)
        {
            if (ModelState.IsValid)
            {
                db.Entry(index).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(index);
        }

        // GET: /Index/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Index index = db.Indices.Find(id);
            if (index == null)
            {
                return HttpNotFound();
            }
            return View(index);
        }

        // POST: /Index/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Index index = db.Indices.Find(id);
            db.Indices.Remove(index);
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
