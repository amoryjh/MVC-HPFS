using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HPSMVC.Controllers
{
    public class FitbitController : Controller
    {
        // GET: Fitbit
        public ActionResult Index()
        {
            return View();
        }
    }
}