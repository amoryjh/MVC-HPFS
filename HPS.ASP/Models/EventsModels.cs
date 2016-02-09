using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HPS.ASP.Models
{
  public class EventsModels
  {
    public int ID { get; set; }
    public string EventName { get; set; }
    public string EventDescription { get; set; }
    public string EventLink { get; set; }
    public DateTime EventDate { get; set; }
    public byte EventFile { get; set; }
  }
}