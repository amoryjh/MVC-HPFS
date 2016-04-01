using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HPSMVC.Models
{
  public class Index
  {
    public int ID { get; set; }

    [Display(Name = "Title")]
    [Required(ErrorMessage = "You cannot leave the title blank.")]
    [StringLength(20, ErrorMessage = "The Title cannot be more than 20 characters")]
    public string Title { get; set; }

    [Display(Name = "Content")]
    [Required(ErrorMessage = "You cannot leave the Content blank.")]
    [StringLength(250, ErrorMessage = "The Content cannot be more than 250 characters")]
    public string Content { get; set; }

    [Display(Name = "Button Text")]
    [StringLength(20, ErrorMessage = "The Content cannot be more than 20 characters")]
    public string ButtonText { get; set; }

    [Display(Name = "Button Link")]
    [StringLength(200)]
    public string ButtonLink { get; set; }

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