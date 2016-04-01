using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HPSMVC.Models
{
    public class File
    {
        public int ID { get; set; }
        
        [Display(Name = "File Name")]
        [StringLength(250)]
        [ScaffoldColumn(false)]
        public string fileName { get; set; }

        [Display(Name = "File Type")]
        [ScaffoldColumn(false)]
        public string fileType{ get; set; }

        [Display(Name = "File Content")]
        [ScaffoldColumn(false)]
        public byte[] fileContent { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        [Display(Name = "Category")]
        [StringLength(50, ErrorMessage = "The file name cannot be more than 50 characters")]
        public string Category { get; set; }

        [Display(Name = "Viewer")]
        [StringLength(50, ErrorMessage = "The file name cannot be more than 50 characters")]
        public string Viewer { get; set; }
    }
}