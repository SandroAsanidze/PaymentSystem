﻿using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PaymentSystem.Models;
using PaymentSystem.ViewModels;

namespace PaymentSystem.Controllers
{
    [Authorize(Roles = "Admin")]
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
        public async Task<IActionResult> Login(LoginVM model)
        {
            if (User.Identity!.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username!, model.Password!, model.RememberMe, false);

                if (result.Succeeded)
                {
                    string indexRoute = Url.Action("Index", "Home", new { }, Request.Scheme)!;


                    return Ok(new
                    {
                        Status = "SUCCESS",
                        PaymentUrl = indexRoute
                    });
                }

                ModelState.AddModelError("", "Invalid login attempt");
                return Ok("Error");
            }

            return View(model);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout([FromHeader] string contentType)
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
