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
using PharmaQueue.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace PharmaQueue.Controllers
{
    public class PrescriptionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IHubContext<StatusHub> _hubContext;

        private bool PrescriptionExists(int id)
        {
            return _context.Prescription.Any(e => e.PrescriptionId == id);
        }

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public PrescriptionsController(ApplicationDbContext context,
                    UserManager<ApplicationUser> userManager,
                    IHubContext<StatusHub> hubContext)
        {
            _userManager = userManager;
            _context = context;
            _hubContext = hubContext;
        }

        // GET: Prescriptions/Details/5
        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            var prescription = await _context.Prescription
                .Include(p => p.User)
                .Include(p => p.Status)
                .FirstOrDefaultAsync(m => m.PrescriptionId == id);
            var user = await GetCurrentUserAsync();
            if (id == null || prescription == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (user.UserTypeId != 1 && user.Id != prescription.UserId)
            {
                return RedirectToAction("Index", "Home");
            }
            var viewModel = new PrescriptionDetailViewModel();
            viewModel.Prescription = prescription;
            viewModel.CurrentUserTypeId = user.UserTypeId;
            return View(viewModel);
        }

        // GET: Prescriptions/Create
        [Authorize]
        public async Task<IActionResult> Create()
        {
            var user = await GetCurrentUserAsync();
            if (user.UserTypeId != 1)
            {
                return RedirectToAction("Index", "Home");
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
            {
                return RedirectToAction("Index", "Home");
            }

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
                await _hubContext.Clients.All.SendAsync("PrescriptionUpdate", createPrescription.UserId);
                return RedirectToAction("Index", "Home");
            }
            return View(createPrescription);
        }

        //Delete
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var user = await GetCurrentUserAsync();
            var prescription = await _context.Prescription
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.PrescriptionId == id);
            if (user.UserTypeId != 1 || prescription.StatusId != 1 || prescription == null)
            {
                return RedirectToAction("Index", "Home");
            }

            _context.Prescription.Remove(prescription);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("PrescriptionUpdate", prescription.UserId);
            return RedirectToAction("Index", "Home");
        }

        // GET: Prescriptions/Edit/5
        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            var user = await GetCurrentUserAsync();
            if (user.UserTypeId != 1 || id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            var viewModel = new PrescriptionEditViewModel();
            var prescription = await _context.Prescription
                .Include(p => p.Status)
                .Include(p => p.User)
                .SingleOrDefaultAsync(p => p.PrescriptionId == id);
            if (prescription == null)
            {
                return RedirectToAction("Index", "Home");
            }
            viewModel.Prescription = prescription;
            return View(viewModel);
        }

        // POST: Prescriptions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, PrescriptionEditViewModel viewModel)
        {
            _context.Update(viewModel.Prescription);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("PrescriptionUpdate", viewModel.Prescription.UserId);
            return RedirectToAction("Index", "Home");
        }

        // POST: Precriptions/Update/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Update(int? id)
        {
            var user = await GetCurrentUserAsync();

            if (id == null || user.UserTypeId != 1)
            {
                return RedirectToAction("Index", "Home");
            }


            var prescriptionToUpdate = await _context.Prescription
                .Include(p => p.Status)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.PrescriptionId == id);
            if (prescriptionToUpdate == null)
            {
                return RedirectToAction("Index", "Home");
            }
            prescriptionToUpdate.StatusId++;
            _context.Update(prescriptionToUpdate);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("PrescriptionUpdate", prescriptionToUpdate.UserId);
            return RedirectToAction("Index", "Home");
        }

        //Sell
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Sell(int? id)
        {
            var user = await GetCurrentUserAsync();

            if (id == null || user.UserTypeId != 1)
            {
                return RedirectToAction("Index", "Home");
            }

            var prescriptionToSell = await _context.Prescription
                .Include(p => p.Status)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.PrescriptionId == id);
            if (prescriptionToSell == null)
            {
                return RedirectToAction("Index", "Home");
            }
            prescriptionToSell.IsSold = true;
            _context.Update(prescriptionToSell);
            await _context.SaveChangesAsync();
            await _hubContext.Clients.All.SendAsync("PrescriptionUpdate", prescriptionToSell.UserId);
            return RedirectToAction("Index", "Home");
        }

        //Sold
        [Authorize]
        public async Task<IActionResult> Sold()
        {
            var user = await GetCurrentUserAsync();
            if (user.UserTypeId != 1)
            {
                var prescriptions = _context.Prescription
                    .Include(p => p.User)
                    .Include(p => p.Status)
                    .Where(p => p.UserId == user.Id && p.StatusId == 4 && p.IsSold == true)
                    .ToListAsync();
                var viewModelForCustomers = new PrescriptionSoldViewModel();
                viewModelForCustomers.Prescriptions = await prescriptions;
                viewModelForCustomers.CurrentUserTypeId = user.UserTypeId;
                return View(viewModelForCustomers);
            }
            var prescriptionsForEmployees = _context.Prescription
                    .Include(p => p.User)
                    .Include(p => p.Status)
                    .Where(p => p.StatusId == 4 && p.IsSold == true)
                    .ToListAsync();
            var viewModelForEmployees = new PrescriptionSoldViewModel();
            viewModelForEmployees.Prescriptions = await prescriptionsForEmployees;
            viewModelForEmployees.CurrentUserTypeId = user.UserTypeId;
            return View(viewModelForEmployees);
        }


    }
}
