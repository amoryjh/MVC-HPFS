using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HPS.ASP.Models
{
    public class Event
    {
        public Event()
        {
            this.Files = new HashSet<File>();
        }

        public int ID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public string By { get; set; }

        public string Viewer { get; set; }

        public string Link { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}