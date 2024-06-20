using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using TaskManagerApp.Data;
using TaskManagerApp.Models;
namespace TaskManagerApp.Controllers
{
    public class LoginController : Controller
    {
        private readonly UserManager<UserDetail> _userManager;
        private readonly SignInManager<UserDetail> _signInManager;
        public LoginController(
                   UserManager<UserDetail> userManager,
                   SignInManager<UserDetail> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [Authorize]
        public async Task<IActionResult> UserInfo()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);
            if (user == null)
            {
                RedirectToAction("Login");
            }
            //login functionality 
            return View(user);
        }

        [HttpGet]
        public IActionResult LoginView()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginView(UserDetail appUser)
        {
            var user = await _userManager.FindByEmailAsync(appUser.Email);

            if (user != null)
            {
                //sign in  
                var signInResult = await _signInManager.PasswordSignInAsync(user, appUser.PasswordHash, false, false);
                if (signInResult.Succeeded)
                {
                    HttpContext.Session.SetString("userName", user.UserName);
                    HttpContext.Session.SetString("Email", user.Email);
                    return RedirectToAction("Index", "TaskHomes");
                }
            }
            return RedirectToAction("Create", "UserDetails");
        }
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("LoginView", "Login");
        }
    }
}
