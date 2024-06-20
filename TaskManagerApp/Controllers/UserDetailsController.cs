using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManagerApp.Data;
using TaskManagerApp.Models;

namespace TaskManagerApp.Controllers
{
    [Authorize]
    public class UserDetailsController : Controller
    {
        private readonly UserManager<UserDetail> _userManager;
        private readonly SignInManager<UserDetail> _signInManager;
        private PasswordHasher<UserDetail> passwordHasher;
        public UserDetailsController(PasswordHasher<UserDetail> passwordhasher, UserManager<UserDetail> userManager,
                   SignInManager<UserDetail> signInManager, TaskManagerAppContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            passwordHasher = passwordhasher;
        }
        //get user info
        public async Task<IActionResult> Details()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);
            if (user == null)
            {
                RedirectToAction("Login");
            }
            //login functionality 
            return View(user);
        }

        [AllowAnonymous]
        public IActionResult Create()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Create(UserDetail appUser)
        {
            //register functionality  

            var user = new UserDetail
            {
                UserName = appUser.UserName,
                Email = appUser.Email,
                PasswordHash = appUser.PasswordHash
            };
            var result = await _userManager.CreateAsync(user, user.PasswordHash);
            if (result.Succeeded)
            {
                return RedirectToAction("LoginView", "Login");
            }
            else if (!result.Succeeded)
            {
                if (result.Errors.Count() > 0)
                {
                    throw new Exception(result.Errors?.FirstOrDefault()?.Description);
                }
            }
            return View(user);
        }

        //  GET: UserDetails/Edit/5
        public async Task<IActionResult> EditUserDetails()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User).ConfigureAwait(false);
            if (user == null)
            {
                RedirectToAction("Login");
            }
            //login functionality 
            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditUserDetails([Bind("Id,Email,UserName,PasswordHash")] UserDetail userDetail)
        {

            if (ModelState.IsValid)
            {
                UserDetail UpdatedUserData = await _userManager.FindByIdAsync(userDetail.Id);
                if (!string.IsNullOrEmpty(userDetail.Email))
                    UpdatedUserData.Email = userDetail.Email;
                if (!string.IsNullOrEmpty(userDetail.UserName))
                    UpdatedUserData.UserName = userDetail.UserName;
                if (!string.IsNullOrEmpty(userDetail.PasswordHash))
                    UpdatedUserData.PasswordHash = passwordHasher.HashPassword(userDetail, userDetail.PasswordHash);

                var result = await _userManager.UpdateAsync(UpdatedUserData);
                if (result.Succeeded)
                {
                    HttpContext.Session.SetString("userName", UpdatedUserData.UserName);
                    HttpContext.Session.SetString("Email", UpdatedUserData.Email);
                    return View(UpdatedUserData);
                }
                else
                    return Content("Error Occured!!");

            }
            return View(userDetail);
        }
    }
}
