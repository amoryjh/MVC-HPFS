using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HPSMVC.Models
{
  public class FitBit
  {
    public int ID { get; set; }

    [Display(Name = "User")]
    [Required(ErrorMessage = "You cannot leave the user blank.")]
    public string User { get; set; }

    [Display(Name = "Progress")]
    [Required(ErrorMessage = "You cannot leave the progress blank.")]
    [StringLength(7, ErrorMessage = "The progress cannot be more than 7 characters")]
    public string Progress { get; set; }

    [Display(Name = "Goal")]
    [Required(ErrorMessage = "You cannot leave the goal blank.")]
    [StringLength(7, ErrorMessage = "The goal cannot be more than 7 characters")]
    public string Goal { get; set; }

    [Display(Name = "Date Start")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? dateStart { get; set; }

    [Display(Name = "Date End")]
    [DataType(DataType.Date)]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime? dateEnd { get; set; }

    [Display(Name = "Percentage Earned")]
    [Required(ErrorMessage = "You cannot leave the percentage earned blank.")]
    [StringLength(4, ErrorMessage = "The percentage earned cannot be more than 4 characters")]
    public string percentageEarned { get; set; }

  }
}