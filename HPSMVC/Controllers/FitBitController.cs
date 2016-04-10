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

            ViewData["fitBitProgress"] = fitBitProgress;
            ViewData["fitBitGoal"] = fitBitGoal;

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

            await userManager.UpdateAsync(currentUser);

            var saveUser = userStore.Context;

            await saveUser.SaveChangesAsync();

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

            await userManager.UpdateAsync(currentUser);

            var saveUser = userStore.Context;

            await saveUser.SaveChangesAsync();

            return RedirectToAction("Index");

        }

    }
}