using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HPSMVC.DAL;
using HPSMVC.Models;

namespace HPSMVC.Controllers
{
    public class EventsController : Controller
    {
        private HPSMVCEntities db = new HPSMVCEntities();

        public ActionResult Index()
        {

            return View(db.Events.ToList().OrderBy( s=> s.Date));
            
        }

        public ActionResult BoardCal()
        {
            return View(db.Events.ToList().OrderBy(s => s.Date));
        }

        public ActionResult Admin(string sortOrder, string searchString)
        {
            ViewBag.DateSortParm = String.IsNullOrEmpty(sortOrder) ? "date" : "";
            ViewBag.TitleSortParm = sortOrder == "Title" ? "title" : "Title";
            ViewBag.BySortParm = sortOrder == "By" ? "by" : "By";
            ViewBag.ViewerSortParm = sortOrder == "Viewer" ? "viewer" : "Viewer";
            var events = from s in db.Events
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                events = events.Where(s => s.Title.Contains(searchString)
                                       || s.Viewer.Contains(searchString)
                                       || s.By.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title":
                    events = events.OrderBy(s => s.Title);
                    break;
                case "date":
                    events = events.OrderByDescending(s => s.Date);
                    break;
                case "by":
                    events = events.OrderBy(s => s.By);
                    break;
                case "viewer":
                    events = events.OrderBy(s => s.Viewer);
                    break;
                default:
                    events = events.OrderBy(s => s.Date);
                    break;
            }

            return View(events.ToList());
        }


        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Content,Date,By,Viewer,LinkText,Link")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Content,Date,By,Viewer,LinkText,Link")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
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
