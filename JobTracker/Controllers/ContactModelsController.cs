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
    public class ContactModelsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContactModelsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ContactModels
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ContactModel.Include(c => c.Company);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ContactModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactModel = await _context.ContactModel
                .Include(c => c.Company)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (contactModel == null)
            {
                return NotFound();
            }

            return View(contactModel);
        }

        // GET: ContactModels/Create
        public IActionResult Create()
        {
            ViewData["CompanyId"] = new SelectList(_context.CompanyModel, "Id", "WebsiteUrl");
            return View();
        }

        // POST: ContactModels/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Email,PhoneNumber,CompanyId")] ContactModel contactModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(contactModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CompanyId"] = new SelectList(_context.CompanyModel, "Id", "WebsiteUrl", contactModel.CompanyId);
            return View(contactModel);
        }

        // GET: ContactModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactModel = await _context.ContactModel.SingleOrDefaultAsync(m => m.Id == id);
            if (contactModel == null)
            {
                return NotFound();
            }
            ViewData["CompanyId"] = new SelectList(_context.CompanyModel, "Id", "WebsiteUrl", contactModel.CompanyId);
            return View(contactModel);
        }

        // POST: ContactModels/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,PhoneNumber,CompanyId")] ContactModel contactModel)
        {
            if (id != contactModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(contactModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactModelExists(contactModel.Id))
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
            ViewData["CompanyId"] = new SelectList(_context.CompanyModel, "Id", "WebsiteUrl", contactModel.CompanyId);
            return View(contactModel);
        }

        // GET: ContactModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contactModel = await _context.ContactModel
                .Include(c => c.Company)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (contactModel == null)
            {
                return NotFound();
            }

            return View(contactModel);
        }

        // POST: ContactModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var contactModel = await _context.ContactModel.SingleOrDefaultAsync(m => m.Id == id);
            _context.ContactModel.Remove(contactModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactModelExists(int id)
        {
            return _context.ContactModel.Any(e => e.Id == id);
        }
    }
}
