using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Open_School_Library.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using Open_School_Library.Models;
using Open_School_Library.Models.AccountViewModels;
using Microsoft.AspNetCore.Identity;
using Open_School_Library.Models.RoleViewModel;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace Open_School_Library.Controllers
{
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;


        public RolesController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var roles = _context.Roles.ToList();

            return View(roles);
        }

        public IActionResult Create()
        {
            var role = new IdentityRole();

            return View(role);
        }

        [HttpPost]
        public IActionResult Create(IdentityRole role)
        {
            _context.Roles.Add(new IdentityRole()
            {
                Name = role.Name,
                NormalizedName = role.Name.ToUpper()
            });

            //_context.Roles.Add(role);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult RegisterRole()
        {
            //var model = new RegisterViewModel
            //{
            //    Roles = new SelectList(_context.Users.ToList(), "UserName", "UserName"),
            //    Users = new SelectList(_context.Roles.ToList(), "Name", "Name")
            //};

            ViewBag.Name = new SelectList(_context.Roles.ToList(), "Name", "Name");
            ViewBag.UserName = new SelectList(_context.Users.ToList(), "UserName", "UserName");

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> RegisterRole(RegisterViewModel model, ApplicationUser user)
        {
            var userID = _context.Users.Where(u => u.UserName == user.UserName).Select(u => u.Id).FirstOrDefault();

            ApplicationUser userX = new ApplicationUser();
            userX = await _userManager.FindByIdAsync(userID);

            await _userManager.AddToRoleAsync(userX, model.Name);

            return RedirectToAction("Index", "Roles");
        }
    }
}
