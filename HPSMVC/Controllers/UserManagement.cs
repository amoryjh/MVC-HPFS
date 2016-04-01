using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HPSMVC.Controllers
{
    public class UserManagementController : Controller
    {
        public ActionResult Index()
        {
            var users = Membership.GetAllUsers();
            return View(users);
        }
    }
}