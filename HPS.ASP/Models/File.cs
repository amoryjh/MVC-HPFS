using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HPS.ASP.Models
{
    public class File
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public DateTime Date { get; set; }

        public string Category { get; set; }

        public string Type { get; set; }

        public byte[] Data { get; set; }
    }
}