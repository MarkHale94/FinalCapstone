using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmaQueue.Data;
using PharmaQueue.Models;
using PharmaQueue.Models.HomeViewModel;

namespace PharmaQueue.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        [Authorize]
        public async Task<IActionResult> Index()
        {
            var user = await GetCurrentUserAsync();
            if (user == null || user.UserTypeId==2)
            {
                var currentPrescriptions = await _context.Prescription
                        .Include(p => p.User)
                        .Include(p => p.Status)
                        .Where(p => p.UserId == user.Id)
                        .ToListAsync();
                var viewModel = new HomeIndexViewModel();
                viewModel.EnteredPrescriptions = currentPrescriptions.Where(p=>p.StatusId == 1).ToList();
                viewModel.ReviewedPrescriptions = currentPrescriptions.Where(p => p.StatusId == 2).ToList();
                viewModel.FilledPrescriptions = currentPrescriptions.Where(p => p.StatusId == 3).ToList();
                viewModel.ReadyPrescriptions = currentPrescriptions.Where(p => p.StatusId == 4 && p.IsSold == false).ToList();
                viewModel.SoldPrescriptions = currentPrescriptions.Where(p => p.StatusId == 4 && p.IsSold == true).ToList();
                return View(viewModel);
            }
            else
            {
                var currentPrescriptions = await _context.Prescription
                        .Include(p => p.User)
                        .Include(p => p.Status)
                        .Where(p => p.IsSold == false)
                        .ToListAsync();
                var viewModel = new HomeIndexViewModel();
                viewModel.EnteredPrescriptions = currentPrescriptions.Where(p => p.StatusId == 1).ToList();
                viewModel.ReviewedPrescriptions = currentPrescriptions.Where(p => p.StatusId == 2).ToList();
                viewModel.FilledPrescriptions = currentPrescriptions.Where(p => p.StatusId == 3).ToList();
                viewModel.ReadyPrescriptions = currentPrescriptions.Where(p => p.StatusId == 4).ToList();
                viewModel.SoldPrescriptions = currentPrescriptions.Where(p => p.StatusId == 4 && p.IsSold == true).ToList();
                return View(viewModel);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
