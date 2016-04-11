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
using System.Threading.Tasks;

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
            var fitBitGoal = currentUser.FitBitGoal.ToString();
            var fitBitDateStart = currentUser.dateStartFitBit.ToString();
            var fitBitDateEnd = currentUser.dateEndFitBit.ToString();

            ViewData["fitBitProgress"] = fitBitProgress;
            ViewData["fitBitGoal"] = fitBitGoal;
            ViewData["fitBitDateStart"] = fitBitDateStart;
            ViewData["fitBitDateEnd"] = fitBitDateEnd;

            return View();
        }

        public ActionResult Manage()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> addProgressFitBit(string addProgress)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var currentUser = userManager.FindById(User.Identity.GetUserId());

            var fitBitProgressOriginal = currentUser.FitBitProgress;
            var fitBitAdd = Convert.ToInt32(addProgress);

            var fitBitProgressNew = (fitBitProgressOriginal += fitBitAdd);

            currentUser.FitBitProgress = fitBitProgressNew;

            try
            {
                await userManager.UpdateAsync(currentUser);
                var saveUser = userStore.Context;
                await saveUser.SaveChangesAsync();

                TempData["ValidationMessage"] = "Progress Added!";
            }

            catch
            {
                TempData["ValidationMessage"] = "Error: Progress Not Added!";
            } 

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> updateGoalFitBit(string updateGoal)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var currentUser = userManager.FindById(User.Identity.GetUserId());

            var fitBitUpdateGoal = Convert.ToInt32(updateGoal);

            currentUser.FitBitGoal = fitBitUpdateGoal;

            try
            {
                await userManager.UpdateAsync(currentUser);
                var saveUser = userStore.Context;
                await saveUser.SaveChangesAsync();

                TempData["ValidationMessage"] = "Goal Updated To: " + " " + fitBitUpdateGoal;
            }

            catch
            {
                TempData["ValidationMessage"] = "Error: Goal Not Updated!";
            }
            

            return RedirectToAction("Index");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> updateDateFitBit(DateTime dateStart)
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var currentUser = userManager.FindById(User.Identity.GetUserId());

            var dateStartUpdate = dateStart.Date;
            
            //dateEnd is automatically 7 days after date start
            var dateEndUpdate = dateStartUpdate.Date.AddDays(7);

            currentUser.dateStartFitBit = dateStartUpdate.ToShortDateString();
            currentUser.dateEndFitBit = dateEndUpdate.ToShortDateString();

            try
            {
                await userManager.UpdateAsync(currentUser);
                var saveUser = userStore.Context;
                await saveUser.SaveChangesAsync();

                TempData["ValidationMessage"] = "Date Start Set To: " + " " + dateStartUpdate.ToShortDateString();
            }

            catch
            {
                TempData["ValidationMessage"] = "Error: Date Start Not Set!";
            }
            

            return RedirectToAction("Index");         
        }

    }
}