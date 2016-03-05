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
        
        //[Display(Name = "File Name")]
        [StringLength(250)]
        [ScaffoldColumn(false)]
        public string fileName { get; set; }

        //[Display(Name = "Type")]
        [ScaffoldColumn(false)]
        public string fileType{ get; set; }

        [ScaffoldColumn(false)]
        //[Required(ErrorMessage = "You cannot leave the a blank file.")]
        public byte[] fileContent { get; set; }

        [Display(Name = "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        [Display(Name = "Category")]
        [StringLength(50, ErrorMessage = "The file name cannot be more than 50 characters")]
        public string Category { get; set; }

        public int? EventID { get; set; }

        public virtual Event Event { get; set; }
    }
}