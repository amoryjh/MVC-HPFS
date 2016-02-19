using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HPS.ASP.Models
{
    public class Event
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public DateTime Date { get; set; }

        public string By { get; set; }

        public string Viewer { get; set; }

        public string Link { get; set; }

        public int FileID { get; set; }

        //Collection of FileID

        public virtual ICollection<File> FileID { get; set; }
    }
}