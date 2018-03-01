using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobTracker.Models
{
    public class ContactModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public  string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        [Required]
        public int  CompanyId { get; set; }
        public CompanyModel Company { get; set; }


    }
}
