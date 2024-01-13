using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using RoyalResidence.Application.Common.Interfaces;
using RoyalResidence.Domain.Entities;
using RoyalResidence.Web.ViewModels;

namespace RoyalResidence.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager   <ApplicationUser> _userManager;
        private readonly SignInManager <ApplicationUser> _signInManager;
        private readonly RoleManager   <ApplicationUser> _roleManager;

        public AccountController
            (IUnitOfWork unitOfWork,
             UserManager   <ApplicationUser> userManager,
             SignInManager <ApplicationUser> signInManager, 
             RoleManager   <ApplicationUser> roleManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        
        public IActionResult Login(string returnUrl = null)
        {
            returnUrl??= Url.Content("~/");
            LoginVM loginVM = new ()
            {
                RedirectUrl = returnUrl
            };
            return View(loginVM);
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
