using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobTracker.Models
{
    public class CompanyModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string WebsiteUrl { get; set; }

        public string Description { get; set; }

    }
}
