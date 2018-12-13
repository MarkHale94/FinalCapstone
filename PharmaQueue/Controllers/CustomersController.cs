using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PharmaQueue.Data;
using PharmaQueue.Models;
using PharmaQueue.Models.CustomerViewModels;

namespace PharmaQueue.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<ApplicationUser> _userManager;

        private Task<ApplicationUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public CustomersController(ApplicationDbContext context,
                    UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            _context = context;
        }
        // GET: Customers
        public ActionResult Index()
        {
            return View();
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(string id)
        {
            var user = await _context.Users
                .Include(u=>u.Prescriptions)
                .Include(u=>u.UserType)
                .FirstOrDefaultAsync(u => u.Id  == id);

            CustomerDetailViewModel viewModel = new CustomerDetailViewModel();
            viewModel.UserId = user.Id;
            viewModel.FirstName = user.FirstName;
            viewModel.LastName = user.LastName;
            viewModel.Prescriptions = user.Prescriptions;
            viewModel.UserTypeId = user.UserTypeId;
            viewModel.UserType = user.UserType;
            return View(viewModel);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            return View();
        }


        // GET: Search Products
        [Authorize]
        public async Task<IActionResult> Search(string search)
        {
            CustomerSearchViewModel viewModel = new CustomerSearchViewModel();

            viewModel.Search = search;
            var Customers = await _context.Users.Where(u=>u.LastName.Contains(search) && u.UserTypeId!=1).ToListAsync();
            List<Customer> customerList = new List<Customer>();
            foreach (var customer in Customers)
            {
                Customer customerToAdd = new Customer();
                customerToAdd.UserId = customer.Id;
                customerToAdd.FirstName = customer.FirstName;
                customerToAdd.LastName = customer.LastName;
                customerList.Add(customerToAdd);
            }
            viewModel.Customers = customerList;
            return View(viewModel);
        }

    }
}