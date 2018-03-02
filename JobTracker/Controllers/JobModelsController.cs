using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobTracker.Data;
using JobTracker.Models;
using Microsoft.AspNetCore.Identity;

namespace JobTracker.Controllers
{
    public class JobModelsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public JobModelsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
        
        // GET: JobModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.JobModel.Include(j => j.AppStatus).Include(j => j.Company).Include(j => j.Contact);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: JobModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobModel = await _context.JobModel
                .Include(j => j.AppStatus)
                .Include(j => j.Company)
                .Include(j => j.Contact)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (jobModel == null)
            {
                return NotFound();
            }

            return View(jobModel);
        }

        // GET: JobModels/Create
        public IActionResult Create()
        {
            ViewData["AppStatusId"] = new SelectList(_context.AppStatusModel, "Id", "AppStatusTitle");
            ViewData["CompanyId"] = new SelectList(_context.CompanyModel, "Id", "WebsiteUrl");
            ViewData["ContactId"] = new SelectList(_context.ContactModel, "Id", "Email");
            return View();
        }

        // POST: JobModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,JobTitle,Language,CompanyId,AppStatusId,ContactId,Notes")] JobModel jobModel)
        {

            ModelState.Remove("User");

                if (ModelState.IsValid)
                {
                ApplicationUser user = await GetCurrentUserAsync();
                jobModel.User = user;
                    _context.Add(jobModel);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                
            ViewData["AppStatusId"] = new SelectList(_context.AppStatusModel, "Id", "AppStatusTitle", jobModel.AppStatusId);
            ViewData["CompanyId"] = new SelectList(_context.CompanyModel, "Id", "WebsiteUrl", jobModel.CompanyId);
            ViewData["ContactId"] = new SelectList(_context.ContactModel, "Id", "Email", jobModel.ContactId);
            return View(jobModel);
        }

        // GET: JobModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobModel = await _context.JobModel.SingleOrDefaultAsync(m => m.Id == id);
            if (jobModel == null)
            {
                return NotFound();
            }
            ViewData["AppStatusId"] = new SelectList(_context.AppStatusModel, "Id", "AppStatusTitle", jobModel.AppStatusId);
            ViewData["CompanyId"] = new SelectList(_context.CompanyModel, "Id", "WebsiteUrl", jobModel.CompanyId);
            ViewData["ContactId"] = new SelectList(_context.ContactModel, "Id", "Email", jobModel.ContactId);
            return View(jobModel);
        }

        // POST: JobModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,JobTitle,Language,CompanyId,AppStatusId,ContactId,Notes")] JobModel jobModel)
        {
            if (id != jobModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(jobModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobModelExists(jobModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AppStatusId"] = new SelectList(_context.AppStatusModel, "Id", "AppStatusTitle", jobModel.AppStatusId);
            ViewData["CompanyId"] = new SelectList(_context.CompanyModel, "Id", "WebsiteUrl", jobModel.CompanyId);
            ViewData["ContactId"] = new SelectList(_context.ContactModel, "Id", "Email", jobModel.ContactId);
            return View(jobModel);
        }

        // GET: JobModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobModel = await _context.JobModel
                .Include(j => j.AppStatus)
                .Include(j => j.Company)
                .Include(j => j.Contact)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (jobModel == null)
            {
                return NotFound();
            }

            return View(jobModel);
        }

        // POST: JobModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobModel = await _context.JobModel.SingleOrDefaultAsync(m => m.Id == id);
            _context.JobModel.Remove(jobModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool JobModelExists(int id)
        {
            return _context.JobModel.Any(e => e.Id == id);
        }
    }
}
