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
    public class HomeController : Controller
    {
        private HPSMVCEntities db = new HPSMVCEntities();

        // GET: Home
        public ActionResult Admin()
        {
            return View(db.Indices.ToList());
        }

        public ActionResult Index()
        {
            return View(db.Indices.ToList());
        }

        // GET: Home/Details/5
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

        // GET: Home/Create
        public ActionResult Create()
        {
            ViewBag.FileID = new SelectList(db.Files, "ID", "fileName");
            return View();
        }

        // POST: Home/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Content,ButtonText,ButtonLink,fileName,fileType,fileContent")] Index index)
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
                            index.fileContent = fileData;
                            index.fileType = mimeType;
                            index.fileName = fileName;
                        }
                    }
                }
                try
                {
                    db.Indices.Add(index);
                    db.SaveChanges();
                    TempData["ValidationMessage"] = index.Title += "   Home Slider Successfully Added!";
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["ValidationMessage"] = index.Title += "   Error: Home Slider Not Successfully Added!";
                }
            }

            return View(index);
        }

        // GET: Home/Edit/5
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

        // POST: Home/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Content,ButtonText,ButtonLink,fileName,fileType,fileContent")] Index index)
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
                            index.fileContent = fileData;
                            index.fileType = mimeType;
                            index.fileName = fileName;
                        }
                    }
                }

                try
                {
                    db.Entry(index).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["ValidationMessage"] = index.Title += "   Home Slider Successfully Edited!";
                    return RedirectToAction("Index");
                }
                catch
                {
                    TempData["ValidationMessage"] = index.Title += "   Error: Home Slider Not Successfully Edited!";
                }

            }
            return View(index);
        }

        // GET: Home/Delete/5
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

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Index index = db.Indices.Find(id);
            try
            {
                db.Indices.Remove(index);
                db.SaveChanges();
                TempData["ValidationMessage"] = index.Title += "  Home Slider Successfully Deleted!";
            }
            catch
            {
                TempData["ValidationMessage"] = index.Title += "   Error: Home Slider Not Successfully Deleted!";
            }
           
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
