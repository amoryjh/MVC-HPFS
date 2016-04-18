using HPSMVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
//[Everyone] The user controller is used to manage all users on the website 
namespace HPSMVC.Controllers
{
    public class UserController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        //Get the Manage user page 
        [Authorize(Roles = "Admin")]
        public ActionResult ManageUser()
        {
            return View();
        }

        //Get the manage user password page 
        [Authorize(Roles = "Admin")]
        public ActionResult ManageUserPassword()
        {
            return View();
        }

        //Get the manage user delete page 
        [Authorize(Roles = "Admin")]
        public ActionResult ManageUserDelete()
        {
            return View();
        }

        //Get the manage role page 
        [Authorize(Roles = "Admin")]
        public ActionResult ManageRole()
        {
            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr =>
            new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View();
        }

        //Get the index of the manage user page 
        [Authorize]
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

            var numberOfDefault = 0;

            foreach (var user in userRoles)
            {
                user.RoleNames = userManager.GetRoles(userStore.Users.First(s => s.UserName == user.UserName).Id);

                if(user.RoleNames.Contains("Default"))
                {
                    numberOfDefault++;
                    TempData["ValidationMessageDefaultUser"] = numberOfDefault + " " + "New User(s) Have Registered";
                    TempData["ValidationMessageIcon"] = "1";
                }
            }

            return View(userRoles);
        }

        //Add roles to users 
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoleAddToUser(string UserName, string RoleName)
        {
            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            try
            {
                manager.AddToRole(user.Id, RoleName);
                TempData["ValidationMessage"] = ("Success: " + " " + UserName + " " + "Was Added to the " + " " + RoleName + " " + "Role");
            }
            catch
            {
                TempData["ValidationMessage"] = ("Error: " + " " + UserName + " " +  "Was Not Successfully Added to the " + " " + RoleName + " " + "Role");
            }

            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageRole");
        }
        
        //Get roles for user 
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult GetRoles(string UserName)
        {
 
            if (!string.IsNullOrWhiteSpace(UserName))
            {
                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

                try
                {
                    ViewBag.RolesForThisUser = manager.GetRoles(user.Id);
                }
                catch
                {
                    TempData["ValidationMessage"] = ("Error: " + " " + UserName + " " + "is not a user");
                }   
            }

            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageRole");
        }

        //Delete roles for users 
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteRoleForUser(string UserName, string RoleName)
        {
            try
            {
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));

                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                if (manager.IsInRole(user.Id, RoleName))
                {
                    manager.RemoveFromRole(user.Id, RoleName);
                    TempData["ValidationMessage"] = ("Success: " + " " + UserName + " " + "Was Removed From the " + " " + RoleName + " " + "Role");
                }
                else
                {
                    TempData["ValidationMessage"] = ("Error: " + " " + UserName + " " + "Was Not Successfully Removed From the " + " " + RoleName + " " + "Role");
                }
            }
            catch
            {
                TempData["ValidationMessage"] = ("Error: Something Went Wrong");
            }
            // prepopulat roles for the view dropdown
            var list = context.Roles.OrderBy(r => r.Name).ToList().Select(rr => new SelectListItem { Value = rr.Name.ToString(), Text = rr.Name }).ToList();
            ViewBag.Roles = list;

            return View("ManageRole");
        }

        //delete user 
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("DeleteUser")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteUser(string Username)
        {
            try
            {
                var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                ApplicationUser user = context.Users.Where(u => u.UserName.Equals(Username, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

                string userID = user.Id;

                if (ModelState.IsValid)
                {
                    if (userID == null)
                    {
                        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    }

                    var userDel = await manager.FindByIdAsync(userID);
                    var logins = user.Logins;

                    foreach (var login in logins.ToList())
                    {
                        await manager.RemoveLoginAsync(login.UserId, new UserLoginInfo(login.LoginProvider, login.ProviderKey));
                    }

                    var rolesForUser = await manager.GetRolesAsync(userID);

                    if (rolesForUser.Count() > 0)
                    {
                        foreach (var item in rolesForUser.ToList())
                        {
                            // item should be the name of the role
                            var result = await manager.RemoveFromRoleAsync(user.Id, item);
                        }
                    }

                    await manager.DeleteAsync(user);

                    TempData["ValidationMessage"] = ("Success: " + " " + Username + " " + "Was Removed");
                    return View("ManageUser");
                }
                else
                {
                    TempData["ValidationMessage"] = ("Error: " + " " + Username + " " + "Was Not Removed");
                    return View("ManageUser");
                }
            }
            catch
            {
                TempData["ValidationMessage"] = ("Error: Something Went Wrong");
                return View("ManageUser");
            }

        }

        //Change users password 
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangeUserPassword(string UserNameChangePassword, string NewPassword)
        {
            var passwordLength = NewPassword;
 
            if(passwordLength.Length < 6)
            {
                TempData["ValidationMessage"] = ("Error: Password Must Be At Least 6 Characters");
                return View("ManageUserPassword");
            }


            ApplicationUser user = context.Users.Where(u => u.UserName.Equals(UserNameChangePassword, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            try
            {
                manager.RemovePassword(user.Id);
                manager.AddPassword(user.Id, NewPassword);
                TempData["ValidationMessage"] = ("Success: " + " " + UserNameChangePassword + " " + "Password Has Been Changed");
            }
            catch
            {
                TempData["ValidationMessage"] = ("Error: " + " " + UserNameChangePassword + " " + "Password Has Not Been Changed");
            }

            return View("ManageUser");
        }

	}
}