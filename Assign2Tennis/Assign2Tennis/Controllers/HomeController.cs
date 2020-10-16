using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Assign2Tennis.Data;
using Assign2Tennis.Models;
using System.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace Assign2Tennis.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        public HomeController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [Authorize]
        public async Task<IActionResult> Index()
        {
            await GiveAdminRole();
            List<string> userids = _context.UserRoles.Where(a => a.RoleId == "a6c3a987-35d9-41fd-ba03-6be557dc236a").Select(b => b.UserId).Distinct().ToList();
            //The first step: get all user id collection as userids based on role from db.UserRoles

            List<IdentityUser> listUsers = _context.Users.Where(a => userids.Any(c => c == a.Id)).ToList();
            //The second step: find all users collection  which 's Id is contained at userids ;
            return View("Index", listUsers);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [Authorize]
        [HttpGet]
        // giving a admin role to the admin@gmail.com user 
        public async Task<IActionResult> GiveAdminRole()
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            string mail = user?.Email;
            if (mail == "admin@gmail.com")
            {
                var _userManager = HttpContext.RequestServices
                   .GetRequiredService<UserManager<IdentityUser>>();
                var signInManager = HttpContext.RequestServices
                    .GetRequiredService<SignInManager<IdentityUser>>();

                await _userManager.AddToRoleAsync(user, "Admin");
                await signInManager.RefreshSignInAsync(user);
            }
            

            return RedirectToAction("index", "home");

        }
        // giving a member role 
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GiveMemberRole()
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            string mail = user?.Email;
            var _userManager = HttpContext.RequestServices
                   .GetRequiredService<UserManager<IdentityUser>>();
            var signInManager = HttpContext.RequestServices
                .GetRequiredService<SignInManager<IdentityUser>>();

            await _userManager.AddToRoleAsync(user, "Member");
            await signInManager.RefreshSignInAsync(user);

            return RedirectToAction("index", "home");

        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GiveCoachRole()
        {
            var user = await GetCurrentUserAsync();
            var userId = user?.Id;
            string mail = user?.Email;
            var _userManager = HttpContext.RequestServices
                   .GetRequiredService<UserManager<IdentityUser>>();
            var signInManager = HttpContext.RequestServices
                .GetRequiredService<SignInManager<IdentityUser>>();

            await _userManager.AddToRoleAsync(user, "Coach");
            await signInManager.RefreshSignInAsync(user);

            return RedirectToAction("index", "home");

        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
