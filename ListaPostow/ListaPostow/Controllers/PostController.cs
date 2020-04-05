using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListaPostow.Models;
using ListaPostow.Models.ViewModels;
using ListaPostow.Services;
using ListaPostow.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ListaPostow.Controllers
{
    public class PostController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IPostService postService;

        public PostController(UserManager<User> userManager, IPostService postService)
        {
            this.userManager = userManager;
            this.postService = postService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(ChanelDetailViewModel chanel)
        {
            //if (chanel.PostMessage == null)
            //    return RedirectToAction("Details", "Chanel", new { id = chanel.ChanelID });
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Puste pole");
                return RedirectToAction("Details", "Chanel", new { id = chanel.ChanelID });
            }
                
            var user = await userManager.GetUserAsync(User);
            await postService.AddPostAsync(chanel.PostMessage, chanel.ChanelID, user);
            return RedirectToAction("Details", "Chanel", new { id = chanel.ChanelID});
        }
    }
}