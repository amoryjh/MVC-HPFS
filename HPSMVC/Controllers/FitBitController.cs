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
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HPSMVC.Controllers
{
    public class FitbitController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        // GET: Fitbit
        public ActionResult Index()
        {
            
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var currentUser = userManager.FindById(User.Identity.GetUserId());

            var fitBitProgress = currentUser.FitBitProgress.ToString();

            TempData["fitBitProgress"] = fitBitProgress;

            return View();

        }
    }
}