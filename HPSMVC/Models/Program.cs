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
    [StringLength(40, ErrorMessage = "The Title cannot be more than 20 characters")]
    public string Title { get; set; }

    [Display(Name = "Content")]
    [Required(ErrorMessage = "You cannot leave the Content blank.")]
    [StringLength(500, ErrorMessage = "The Content cannot be more than 500 characters")]
    public string Content { get; set; }

    [Display(Name = "Image")]
    [Required(ErrorMessage = "You cannot leave the Data blank.")]
    public byte[] Image { get; set; }
  }
}