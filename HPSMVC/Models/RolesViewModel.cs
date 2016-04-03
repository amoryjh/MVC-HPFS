using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HPSMVC.Models
{
    public class RolesViewModel
    {
        public IEnumerable<string> RoleNames { get; set; }
        public string UserName { get; set; }
    }
}