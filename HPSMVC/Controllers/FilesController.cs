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
    public class FilesController : Controller
    {
        private HPSMVCEntities db = new HPSMVCEntities();

        // GET: Files
        public ActionResult Index(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Date" ? "date_desc" : "Date";
            var Files = from s in db.Files
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                Files = Files.Where(s => s.Category.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    Files = Files.OrderByDescending(s => s.Category);
                    break;
                case "Date":
                    Files = Files.OrderBy(s => s.Date);
                    break;
                default:
                    Files = Files.OrderBy(s => s.Category);
                    break;
            }
            return View(Files.ToList());
        }
        

        // GET: Files/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HPSMVC.Models.File file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // GET: Files/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Files/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,fileName,fileType,fileContent,Date,Category")] HPSMVC.Models.File file)
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
                  if (mimeType.Contains("image") && fName == "FileUpImage")
                  {
                    file.fileContent = fileData;
                    file.fileType = mimeType;
                    file.fileName = fileName;
                  }
                }
              }
                db.Files.Add(file);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(file);
        }

        // GET: Files/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HPSMVC.Models.File file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: Files/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,fileName,fileType,fileContent,Date,Category")] HPSMVC.Models.File file)
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
                        if (mimeType.Contains("image") && fName == "FileUpImage")
                        {
                            file.fileContent = fileData;
                            file.fileType = mimeType;
                            file.fileName = fileName;
                        }
                    }
                }
                db.Entry(file).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(file);
        }

        // GET: Files/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HPSMVC.Models.File file = db.Files.Find(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
          HPSMVC.Models.File file = db.Files.Find(id);
            db.Files.Remove(file);
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
