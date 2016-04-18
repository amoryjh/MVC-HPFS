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
    public class CareController : Controller
    {
        private HPSMVCEntities db = new HPSMVCEntities();

        public ActionResult Index()
        {
            return View(db.Programs.ToList());
        }

        // GET: Care
        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            return View(db.Programs.ToList());
        }

        // GET: Care/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Care/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Title,Content,fileName,fileType,fileContent")] Program program)
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
                            program.fileContent = fileData;
                            program.fileType = mimeType;
                            program.fileName = fileName;
                        }
                    }
                }
                try
                {
                    db.Programs.Add(program);
                    db.SaveChanges();
                    TempData["ValidationMessage"] = program.Title += "   Program Successfully Added!";
                    return RedirectToAction("Admin");
                }
                catch
                {
                    TempData["ValidationMessage"] = program.Title += "   Error: Program Not Successfully Added!";
                }
                
            }

            return View(program);
        }

        // GET: Care/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Program program = db.Programs.Find(id);
            if (program == null)
            {
                return HttpNotFound();
            }
            return View(program);
        }

        // POST: Care/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        //public ActionResult Edit([Bind(Include = "ID,Title,Content,fileName,fileType,fileContent")] Program program)
        public ActionResult Edit(int? id, string[] chkRemoveFile)//[Bind(Include = "ID,Name,Dated,imageContent,imageMimeType,imageFileName,MovementID,ArtistID")] Painting painting, string chkRemoveImage)
        {
            var program = db.Programs
            .Where(f => f.ID == id)
            .Single();
            if (TryUpdateModel(program, "",
                new string[] { "ID", "Title", "Content", "fileName", "fileType", "fileContent" }))
            {
                if (chkRemoveFile != null)//Remove the File
                {
                    program.fileContent = null;
                    program.fileType = null;
                    program.fileName = null;
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
                        if (mimeType.Contains("image") && fName == "FileUpImage")
                        {
                            program.fileContent = fileData;
                            program.fileType = mimeType;
                            program.fileName = fileName;
                        }
                    }
                }
                try
                {
                    db.Entry(program).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["ValidationMessage"] = program.Title += "   Program Successfully Edited!";
                    return RedirectToAction("Admin");
                }
                catch
                {
                    TempData["ValidationMessage"] = program.Title += "   Error: Program Not Successfully Edited!";
                }

            }
            return View(program);
        }

        // GET: Care/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Program program = db.Programs.Find(id);
            if (program == null)
            {
                return HttpNotFound();
            }
            return View(program);
        }

        // POST: Care/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Program program = db.Programs.Find(id);
            try
            {
                db.Programs.Remove(program);
                db.SaveChanges();
                TempData["ValidationMessage"] = program.Title += "   Program Successfully Deleted!";
            }
            catch
            {
                TempData["ValidationMessage"] = program.Title += "   Error: Program Not Successfully Deleted!";
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
