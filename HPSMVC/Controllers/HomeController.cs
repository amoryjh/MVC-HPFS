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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HPSMVC.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();
        private HPSMVCEntities db = new HPSMVCEntities();

        // GET: Home
        [Authorize(Roles = "Admin")]
        public ActionResult Admin()
        {
            return View(db.Indices.ToList());
        }

        public ActionResult Index()
        {
            var userRoles = new List<RolesViewModel>();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            //Get all the usernames
            foreach (var user in userStore.Users)
            {
                var r = new RolesViewModel
                {
                    UserName = user.UserName
                };
                userRoles.Add(r);
            }
            //Get all the Roles for our users
            foreach (var user in userRoles)
            {
                user.RoleNames = userManager.GetRoles(userStore.Users.First(s => s.UserName == user.UserName).Id);

                if (user.RoleNames.Contains("Default"))
                {
                    TempData["ValidationMessageIcon"] = "1";
                }
            }

            return View(db.Indices.ToList());
        }

        // GET: Home/Details/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
                    return RedirectToAction("Admin");
                }
                catch
                {
                    TempData["ValidationMessage"] = index.Title += "   Error: Home Slider Not Successfully Added!";
                }
            }

            return View(index);
        }

        // GET: Home/Edit/5
        [Authorize(Roles = "Admin")]
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        //public ActionResult Edit([Bind(Include = "ID,Title,Content,ButtonText,ButtonLink,fileName,fileType,fileContent")] Index index)
        public ActionResult Edit(int? id, string[] chkRemoveFile)//[Bind(Include = "ID,Name,Dated,imageContent,imageMimeType,imageFileName,MovementID,ArtistID")] Painting painting, string chkRemoveImage)
        {
            var index = db.Indices
            .Where(f => f.ID == id)
            .Single();
            if (TryUpdateModel(index, "",
                new string[] { "ID","Title","Content","ButtonText","ButtonLink","fileName","fileType","fileContent","AlertMessage" }))
            {
                if (chkRemoveFile != null)//Remove the File
                {
                    index.fileContent = null;
                    index.fileType = null;
                    index.fileName = null;
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
                    return RedirectToAction("Admin");
                }
                catch
                {
                    TempData["ValidationMessage"] = index.Title += "   Error: Home Slider Not Successfully Edited!";
                }

            }
            return View(index);
        }

        // GET: Home/Delete/5
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
