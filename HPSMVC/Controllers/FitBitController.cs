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
        private HPSMVCEntities db = new HPSMVCEntities();

        // GET: Fitbit
        [Authorize]
        public ActionResult Index()
        {
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            var currentUser = userManager.FindById(User.Identity.GetUserId());

            if (currentUser.dateStartFitBit == null || currentUser.dateEndFitBit == null)
            {
                currentUser.dateStartFitBit = "";
                currentUser.dateEndFitBit = "";
            }

            var currentDate = DateTime.Now.Date.ToShortDateString();

            if(currentUser.dateEndFitBit.Contains(currentDate))
            {
                TempData["SubmitFitBit"] = "1";
            }

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
        [Authorize]
        public ActionResult Manage()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminStats(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name" : "";
            ViewBag.DateStartSortParm = sortOrder == "Start Date" ? "date" : "Start Date";
            ViewBag.GoalSortParm = sortOrder == "Goal" ? "goal" : "Goal";
            ViewBag.ProgressSortParm = sortOrder == "Progress" ? "progress" : "Progress";

            var FitBits = from s in db.FitBits
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                FitBits = FitBits.Where(s => s.User.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name":
                    FitBits = FitBits.OrderByDescending(s => s.User);
                    break;
                case "date":
                    FitBits = FitBits.OrderByDescending(s => s.dateStart);
                    break;
                case "goal":
                    FitBits = FitBits.OrderBy(s => s.Goal);
                    break;
                case "progress":
                    FitBits = FitBits.OrderBy(s => s.Progress);
                    break;
                default:
                    FitBits = FitBits.OrderBy(s => s.User);
                    break;
            }


            return View(FitBits.ToList());
        }

        [Authorize]
        public ActionResult UserStats()
        {
            return View(db.FitBits.ToList());
        }

        [Authorize]
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

        [Authorize]
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

        [Authorize]
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

        [Authorize]
        public async Task<ActionResult> Create()
        {
            FitBit fitbit = new FitBit();
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);

            try
            {
                var currentUser = userManager.FindById(User.Identity.GetUserId());
                var progress = Convert.ToDouble(currentUser.FitBitProgress);
                var goal = Convert.ToDouble(currentUser.FitBitGoal);
                var dateStart = Convert.ToDateTime(currentUser.dateStartFitBit.ToString());
                var dateEnd = Convert.ToDateTime(currentUser.dateEndFitBit.ToString());
                var percentEarned = (progress / goal) * 100;

                int percentEarnedShort = Convert.ToInt32(percentEarned);

                dateStart.ToShortDateString();
                dateEnd.ToShortDateString();

                fitbit.User = currentUser.UserName;
                fitbit.Progress = progress.ToString();
                fitbit.Goal = goal.ToString();
                fitbit.dateStart = dateStart;
                fitbit.dateEnd = dateEnd;
                fitbit.percentageEarned = percentEarnedShort.ToString();

                db.FitBits.Add(fitbit);
                db.SaveChanges();

                TempData["ValidationMessage"] = "Stats Submitted for " + " " + dateStart + " - " + dateEnd;

                var fitBitProgressNew = 0;
                currentUser.FitBitProgress = fitBitProgressNew;

                try
                {
                    await userManager.UpdateAsync(currentUser);
                    var saveUser = userStore.Context;
                    await saveUser.SaveChangesAsync();
                }

                catch
                {
                    TempData["ValidationMessage"] = "Error: Stats Not Submitted!";
                    return RedirectToAction("Index", "Fitbit");
                } 

                return RedirectToAction("Manage", "Fitbit");
            }
            catch
            {
                TempData["ValidationMessage"] = "Error: Stats Not Submitted!";
                return RedirectToAction("Index", "Fitbit");
            }
        }

    }
}