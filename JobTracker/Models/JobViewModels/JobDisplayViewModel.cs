using JobTracker.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace JobTracker.Models.JobViewModels
{
    public class JobDisplayViewModel
    {

        public List<JobModel> Jobs { get; set; }

        public List<SelectListItem> Contacts {get; set;}

        public List<SelectListItem> AppStatuses { get; set; }

        public JobDisplayViewModel (ApplicationDbContext ctx)
        {
            this.AppStatuses = ctx.AppStatusModel
                        .OrderBy(l => l.AppStatusTitle)
                        .AsEnumerable()
                        .Select(li => new SelectListItem
                        {
                            Text = li.AppStatusTitle,
                            Value = li.Id.ToString()
                        }).ToList();

            this.AppStatuses.Insert(0, new SelectListItem
            {
                Text = "Choose Status...",
                Value = "0"
            });

            this.Contacts = ctx.ContactModel
                .OrderBy(I => I.Name)
                .AsEnumerable()
                .Select(li => new SelectListItem
                {
                    Text = li.Name,
                    Value = li.Id.ToString()
                }).ToList();

            this.Contacts.Insert(0, new SelectListItem
            {
                Text = "Choose Contact...",
                Value = "0"
            });
        }
        
    }
}
