using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Application.ViewModels;
using PaymentSystem.Domain.Models;

namespace PaymentSystem.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(SignInManager<AppUser> signInManager)
        {
            _signInManager = signInManager;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginVM model,string action)
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if(action == "login")
            {
                if (ModelState.IsValid)
                {
                    var result = await _signInManager.PasswordSignInAsync(model.Username!, model.Password!, model.RememberMe, false);

                    if (result.Succeeded)
                    {
                        string homeRoute = Url.Action("Index", "Home", new { }, Request.Scheme)!;

                        return RedirectToAction("Index", "Home", new
                        {
                            Status = "SUCCESS",
                            PaymentUrl = homeRoute
                        });
                    }
                }

            }
            ModelState.AddModelError("", "Invalid login attempt");
            return Ok("Error");
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout(string action)
        {
            await _signInManager.SignOutAsync();
            string indexRoute = Url.Action("Index", "Home", new { }, Request.Scheme)!;
            return Ok(new
            {
                Status = "SUCCESS",
                PaymentUrl = indexRoute
            });
        }
    }
}
