using JobTracker.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JobTracker.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for Appstatuses
                if (context.AppStatusModel.Any())
                {
                    return;   // DB has been seeded
                }

                var AppStatuses = new AppStatusModel[]
                {
                    new AppStatusModel {
                        AppStatusTitle = "Not Yet Applied"
                    },
                    new AppStatusModel {
                        AppStatusTitle = "Applied"
                    },
                    new AppStatusModel {
                        AppStatusTitle = "Interview Offered"
                    },
                    new AppStatusModel {
                        AppStatusTitle = "Interview Completed"
                    },
                    new AppStatusModel {
                        AppStatusTitle = "Job Offer Received"
                    },
                    new AppStatusModel {
                        AppStatusTitle = "Not Hired"
                    },
                    new AppStatusModel {
                        AppStatusTitle = "Declined Offer"
                    },
                    new AppStatusModel {
                        AppStatusTitle = "Accepeted Offer"
                    },
                };

                foreach (AppStatusModel i in AppStatuses)
                {
                    context.AppStatusModel.Add(i);
                }
                context.SaveChanges();
            }
        }

    }
}
