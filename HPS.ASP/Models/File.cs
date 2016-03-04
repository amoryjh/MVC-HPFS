using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HPS.ASP.Models
{
    public class File
    {
        [Display(Name = "Name")]
        [Required(ErrorMessage = "You cannot leave the name blank.")]
        [StringLength(50, ErrorMessage = "The file name cannot be more than 50 characters")]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "Category")]
        [Required(ErrorMessage = "You cannot leave the category blank.")]
        [StringLength(50, ErrorMessage = "The file name cannot be more than 50 characters")]
        public string Category { get; set; }

        [Display(Name = "Type")]
        [Required(ErrorMessage = "You cannot leave the File Type blank.")]
        [StringLength(50, ErrorMessage = "The file name cannot be more than 50 characters")]
        public string Type { get; set; }

        [Display(Name = "Data")]
        [Required(ErrorMessage = "You cannot leave the Data blank.")]
        public byte[] Data { get; set; }

        [Display(Name = "Event Name")]
        [Required(ErrorMessage = "You must select an event.")]
        public int EventID { get; set; }

        public virtual Event Event { get; set; }
    }
}