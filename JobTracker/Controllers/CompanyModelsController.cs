using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using JobTracker.Data;
using JobTracker.Models;

namespace JobTracker.Controllers
{
    public class CompanyModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompanyModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: CompanyModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.CompanyModel.ToListAsync());
        }

        // GET: CompanyModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyModel = await _context.CompanyModel
                .SingleOrDefaultAsync(m => m.Id == id);
            if (companyModel == null)
            {
                return NotFound();
            }

            return View(companyModel);
        }

        // GET: CompanyModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CompanyModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,WebsiteUrl,Description")] CompanyModel companyModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(companyModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(companyModel);
        }

        // GET: CompanyModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyModel = await _context.CompanyModel.SingleOrDefaultAsync(m => m.Id == id);
            if (companyModel == null)
            {
                return NotFound();
            }
            return View(companyModel);
        }

        // POST: CompanyModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,WebsiteUrl,Description")] CompanyModel companyModel)
        {
            if (id != companyModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(companyModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CompanyModelExists(companyModel.Id))
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
            return View(companyModel);
        }

        // GET: CompanyModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var companyModel = await _context.CompanyModel
                .SingleOrDefaultAsync(m => m.Id == id);
            if (companyModel == null)
            {
                return NotFound();
            }

            return View(companyModel);
        }

        // POST: CompanyModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var companyModel = await _context.CompanyModel.SingleOrDefaultAsync(m => m.Id == id);
            _context.CompanyModel.Remove(companyModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CompanyModelExists(int id)
        {
            return _context.CompanyModel.Any(e => e.Id == id);
        }
    }
}
