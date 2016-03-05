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
    [StringLength(100)]
    public string ButtonLink { get; set; }

    [Display(Name = "Image")]
    [Required(ErrorMessage = "You cannot leave the Image blank.")]
    public byte[] Image { get; set; }
  }
}