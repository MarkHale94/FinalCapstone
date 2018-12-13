using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmaQueue.Data;
using PharmaQueue.Models;
using PharmaQueue.Models.PrescriptionViewModels;

namespace PharmaQueue.Controllers
{
    public class PrescriptionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public PrescriptionsController(ApplicationDbContext context,
                    UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }


        // GET: Prescriptions
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Prescription
                .Include(p => p.User)
                .Include(p => p.Status);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Prescriptions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescription
                .Include(p => p.User)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(m => m.PrescriptionId == id);
            if (prescription == null)
            {
                return NotFound();
            }

            return View(prescription);
        }
        
        // GET: Prescriptions/Create
        public async Task<IActionResult> Create()
        {
            var user = await GetCurrentUserAsync();
            if (user.UserTypeId != 1)
            {
                return RedirectToAction(nameof(Index));
            }
            PrescriptionCreateViewModel createViewModel = new PrescriptionCreateViewModel();
            createViewModel.UserId = this.RouteData.Values.Values.LastOrDefault().ToString();
            return View(createViewModel);
        }

        // POST: Prescriptions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(PrescriptionCreateViewModel createPrescription)
        {
            createPrescription.UserId = this.RouteData.Values.Values.LastOrDefault().ToString();
            var user = await GetCurrentUserAsync();
            var customer = await _context.Users.FirstOrDefaultAsync(u => u.Id == createPrescription.UserId);
            if (user.UserTypeId != 1)
            { return RedirectToAction(nameof(Index)); }

            ModelState.Remove("Prescription.User");
            ModelState.Remove("Prescription.UserId");
            ModelState.Remove("Prescription.StatusId");
            ModelState.Remove("Prescription.Status");
            if (ModelState.IsValid)
            {
                createPrescription.Prescription.User = customer;
                createPrescription.Prescription.UserId = createPrescription.UserId;
                createPrescription.Prescription.StatusId = 1;
                createPrescription.Prescription.IsSold = false;
                _context.Add(createPrescription.Prescription);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(createPrescription);
        }

        // GET: Prescriptions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescription.FindAsync(id);
            if (prescription == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", prescription.UserId);
            return View(prescription);
        }

        // POST: Prescriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PrescriptionId,Name,Strength,Quantity,Refills,Price,IsSold,UserId,PrescriptionStatusId,DateCreated")] Prescription prescription)
        {
            if (id != prescription.PrescriptionId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(prescription);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrescriptionExists(prescription.PrescriptionId))
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
            ViewData["UserId"] = new SelectList(_context.ApplicationUsers, "Id", "Id", prescription.UserId);
            return View(prescription);
        }

        // GET: Prescriptions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var prescription = await _context.Prescription
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.PrescriptionId == id);
            if (prescription == null)
            {
                return NotFound();
            }

            return View(prescription);
        }

        // POST: Prescriptions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var prescription = await _context.Prescription.FindAsync(id);
            _context.Prescription.Remove(prescription);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrescriptionExists(int id)
        {
            return _context.Prescription.Any(e => e.PrescriptionId == id);
        }
    }
}
