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
using System.IO;

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
                                       || s.Viewer.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "title":
                    events = events.OrderBy(s => s.Title);
                    break;
                case "date":
                    events = events.OrderByDescending(s => s.Date);
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
        public ActionResult Create([Bind(Include = "ID,Title,Content,Time,Date,Viewer,LinkText,Link,fileName,fileType,fileContent")] Event @event)
        {
            if (ModelState.IsValid)
            {
                foreach (string fName in Request.Files)
              {
                HttpPostedFileBase f = Request.Files[fName];
                string mimeType = f.ContentType;
                int fileLength = f.ContentLength;
                if (!(mimeType == "" || fileLength == 0))
                {
                  string fileName = Path.GetFileName(f.FileName);
                  Stream fileStream = Request.Files[fName].InputStream;
                  byte[] fileData = new Byte[fileLength];
                  fileStream.Read(fileData, 0, fileLength);

                  @event.fileContent = fileData;
                  @event.fileType = mimeType;
                  @event.fileName = fileName;

                
               }
            }
            try
            {
                db.Events.Add(@event);
                db.SaveChanges();
                TempData["ValidationMessage"] = @event.Title += "   Event Successfully Added!";
                return RedirectToAction("Admin");
            }
            catch
            {
                TempData["ValidationMessage"] = @event.Title += "   Error: Event Not Successfully Added!";
            }
                
        }

            return View(@event);
                
        }

        public FileContentResult Download(int id)
        {
            var theFile = db.Events.Where(f => f.ID == id).SingleOrDefault();
            return File(theFile.fileContent, theFile.fileType, theFile.fileName);
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
        //public ActionResult Edit([Bind(Include = "ID,Title,Content,Time,Date,Viewer,LinkText,Link,fileName,fileType,fileContent")] Event @event)
        public ActionResult Edit(int? id, string[] chkRemoveFile,Event @event)//[Bind(Include = "ID,Name,Dated,imageContent,imageMimeType,imageFileName,MovementID,ArtistID")] Painting painting, string chkRemoveImage)
        
        {
            var EventFileToUpdate = db.Files
            .Where(f => f.ID == id)
            .Single();
            if (TryUpdateModel(EventFileToUpdate, "",
                new string[] { "ID", "Filename", "fileType", "FileContent", "Date", "Event @event" }))
            {
                if (chkRemoveFile != null)//Remove the File
                {
                    EventFileToUpdate.fileContent = null;
                    EventFileToUpdate.fileType = null;
                    EventFileToUpdate.fileName = null;
                }
                foreach (string fName in Request.Files)
                {
                    HttpPostedFileBase f = Request.Files[fName];
                    string mimeType = f.ContentType;
                    int fileLength = f.ContentLength;
                    if (!(mimeType == "" || fileLength == 0))
                    {
                        string fileName = Path.GetFileName(f.FileName);
                        Stream fileStream = Request.Files[fName].InputStream;
                        byte[] fileData = new Byte[fileLength];
                        fileStream.Read(fileData, 0, fileLength);

                        EventFileToUpdate.fileContent = fileData;
                        EventFileToUpdate.fileType = mimeType;
                        EventFileToUpdate.fileName = fileName;

                    }
                }
                try
                {
                    db.Entry(EventFileToUpdate).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["ValidationMessage"] = @event.Title += "   Event Successfully Edited!";
                    return RedirectToAction("Admin");
                }
                catch
                {
                    TempData["ValidationMessage"] = @event.Title += "   Error: Event Not Successfully Edited!";
                }

                
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
            try
            {
                db.Events.Remove(@event);
                db.SaveChanges();
                TempData["ValidationMessage"] = @event.Title += "   Event Successfully Deleted!";
            }
            catch
            {
                TempData["ValidationMessage"] = @event.Title += "   Error: Event Not Successfully Deleted!";
            }
            
            return RedirectToAction("Admin");
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
