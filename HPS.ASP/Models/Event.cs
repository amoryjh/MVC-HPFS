﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HPS.ASP.Models
{
    public class Event
    {
        public Event()
        {
            this.Files = new HashSet<File>();
        }

        public int ID { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "You cannot leave the title blank.")]
        [StringLength(20, ErrorMessage = "The Title cannot be more than 20 characters")]
        public string Title { get; set; }

        [Display(Name = "Content")]
        [Required(ErrorMessage = "You cannot leave the Content blank.")]
        [StringLength(250, ErrorMessage = "The Content cannot be more than 250 characters")]
        public string Content { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Display(Name = "By")]
        [Required(ErrorMessage = "You cannot leave the Link blank.")]
        [StringLength(50, ErrorMessage = "The Content cannot be more than 50 characters")]
        public string By { get; set; }

        [Display(Name = "Viewer")]
        [Required(ErrorMessage = "You cannot leave the Viewer blank.")]
        public string Viewer { get; set; }

        [Display(Name = "Link")]
        [Required(ErrorMessage = "You cannot leave the Content blank.")]
        [StringLength(100, ErrorMessage = "The Link cannot be more than 100 characters")]
        public string Link { get; set; }

        public virtual ICollection<File> Files { get; set; }
    }
}