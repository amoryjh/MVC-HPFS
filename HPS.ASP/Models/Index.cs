using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HPS.ASP.Models
{
    public class Index
    {
        public int ID { get; set; }

        public string Title { get; set; }

        public string Content { get; set; }

        public string ButtonText { get; set; }

        public string ButtonLink { get; set; }

        public byte[] Image { get; set; }
    }
}