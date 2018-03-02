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

        [Required]
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
                Text = "Choose category...",
                Value = "0"
            });
        }
        
    }
}
