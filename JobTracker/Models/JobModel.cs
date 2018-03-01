using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobTracker.Models
{
    public class JobModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string JobTitle { get; set; }

       public string Language { get; set; }

        [Required]
        public int CompanyId { get; set; }
        public CompanyModel Company { get; set; }

        [Required]
        public int AppStatusId { get; set; }
        public AppStatusModel AppStatus { get; set; }

        public int ContactId { get; set; }
        public ContactModel Contact { get; set; }

        public string Notes { get; set; }

        [Required]
        public ApplicationUser User { get; set; }
    }
}
