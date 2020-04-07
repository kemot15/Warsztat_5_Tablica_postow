using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListaPostow.Models;
using ListaPostow.Models.Db;
using ListaPostow.Models.ViewModels;
using ListaPostow.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ListaPostow.Controllers
{
    public class AccountController : Controller
    {
        protected readonly UserManager<User> UserManager;
        protected readonly SignInManager<User> SignInManager;
        protected readonly IChanelService _chanelService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IChanelService chanelService)
        {
            UserManager = userManager;
            SignInManager = signInManager;
            _chanelService = chanelService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Registration() => View();
        
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel registrationViewModel) 
        {
            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    UserName = registrationViewModel.Name,
                    Email = registrationViewModel.Email
                };
                var result = await UserManager.CreateAsync(user, registrationViewModel.Password);
                if (result.Succeeded)
                {
                    var chanel = new Chanel()
                    {
                        Name = $"{user.UserName} - główny",
                        OwnerID = user.Id,
                        Color = "blue"
                    };
                    await _chanelService.CreateChanelAsync(chanel);
                   // await _chanelService.AddToFavoriteAsync()
                    return RedirectToAction("Login");
                }
            }            
            return View(registrationViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Login() => View();
        [HttpPost]
        public async Task<IActionResult> Login (LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                var result = await SignInManager.PasswordSignInAsync(loginViewModel.Login, loginViewModel.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("List", "Chanel");
                }
                else
                {
                    ModelState.AddModelError("", "Nie można się zalogować");
                }
            }
            return View(loginViewModel);
        }
    }
}