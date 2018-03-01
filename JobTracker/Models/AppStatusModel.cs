using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobTracker.Models
{
    public class AppStatusModel
    {
     [Key]
     public int Id { get; set; }
     
     [Required]
     public string AppStatusTitle { get; set; }

    }
}
