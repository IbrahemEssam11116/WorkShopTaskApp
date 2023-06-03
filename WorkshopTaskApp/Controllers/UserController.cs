using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using WorkshopTaskApp.Bussniss.Intrfaces;
using WorkshopTaskApp.Entity.Models;
using WorkshopTaskApp.Bussniss.DTOS;

namespace WorkshopTaskApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }


        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user,int isAdmin)
        {

            var savedUser = await _userService.RegisterUser(user,isAdmin==1);
            if (savedUser == null)
                return View();

            var principal = _userService.CreateIdentity(savedUser.Id, savedUser.FirstName,savedUser.Role);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                          principal,
                                          new AuthenticationProperties() { IsPersistent = true });


            return RedirectToAction("Index", "Product");

        }
        public async Task<IActionResult> Login()
        {
            return View();
        }
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Product");
        }
        [HttpPost]
        public async Task<IActionResult> Login(UserToLoginDto loginDto)
        {
            User fetchedUser = await _userService.ValidateUser(loginDto);
            if (fetchedUser != null)
            {
                var principal = _userService.CreateIdentity(fetchedUser.Id, fetchedUser.FirstName,fetchedUser.Role);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                                              principal,
                                              new AuthenticationProperties() { IsPersistent = true });
                return RedirectToAction("Index", "Product");

            }
            loginDto.Password = "";
            ModelState.AddModelError("", "Invalid UserName Or Password");

            return View(loginDto);
        }

    }
}
