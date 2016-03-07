using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace HPSMVC.Models
{
  public class Program
  {
    public int ID { get; set; }

    [Display(Name = "Title")]
    [Required(ErrorMessage = "You cannot leave the title blank.")]
    [StringLength(30, ErrorMessage = "The title cannot be more than 30 characters")]
    public string Title { get; set; }

    [Display(Name = "Content")]
    [Required(ErrorMessage = "You cannot leave the content blank.")]
    [StringLength(800, ErrorMessage = "The content cannot be more than 800 characters")]
    public string Content { get; set; }

    [Display(Name = "File Name")]
    [StringLength(250)]
    [ScaffoldColumn(false)]
    public string fileName { get; set; }

    [Display(Name = "File Type")]
    [ScaffoldColumn(false)]
    public string fileType { get; set; }

    [Display(Name = "File Content")]
    [ScaffoldColumn(false)]
    public byte[] fileContent { get; set; }
  }
}