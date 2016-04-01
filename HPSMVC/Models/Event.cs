using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HPSMVC.Models
{
  public class Event
  {
    public int ID { get; set; }

    [Display(Name = "Title")]
    [Required(ErrorMessage = "You cannot leave the title blank.")]
    [StringLength(20, ErrorMessage = "The Title cannot be more than 20 characters")]
    public string Title { get; set; }

    [Display(Name = "Content")]
    [StringLength(250, ErrorMessage = "The Content cannot be more than 250 characters")]
    public string Content { get; set; }

    [Display(Name = "Time")]
    [StringLength(7, ErrorMessage = "The Time cannot be more than 7 characters")]
    public string Time { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? Date { get; set; }

    [Display(Name = "By")]
    [Required(ErrorMessage = "You cannot leave the author text blank.")]
    [StringLength(50, ErrorMessage = "The Content cannot be more than 50 characters")]
    public string By { get; set; }

    [Display(Name = "Viewer")]
    [Required(ErrorMessage = "You cannot leave the Viewer blank.")]
    public string Viewer { get; set; }

    [Display(Name = "LinkText")]
    [StringLength(15, ErrorMessage = "The link text cannot be more than 15 characters")]
    public string LinkText { get; set; }

    [Display(Name = "Link")]
    [StringLength(100, ErrorMessage = "The Link cannot be more than 100 characters")]
    public string Link { get; set; }

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