using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading.Tasks;
using ListaPostow.Models;
using ListaPostow.Models.Db;
using ListaPostow.Models.ViewModels;
using ListaPostow.Services;
using ListaPostow.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers.Cache;


namespace ListaPostow.Controllers
{
    [Authorize]
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
        [Authorize]
        public async Task<IActionResult> Add(ChanelDetailViewModel chanel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Puste pole");
                return RedirectToAction("Details", "Chanel", new { id = chanel.ChanelID });
            }

            var user = await userManager.GetUserAsync(User);
            await postService.AddPostAsync(chanel.PostMessage, chanel.ChanelID, user);
            return RedirectToAction("Details", "Chanel", new { id = chanel.ChanelID });
        }

        
        public async Task<IActionResult> Delete(int postID, int chanelID)
        {
            await postService.DeleteAsync(postID);
            return RedirectToAction("Details", "Chanel", new { id = chanelID });
        }
        [HttpGet]
        public async Task<IActionResult> Edit (int postID)
        {
            
            var post = await postService.GetAsync(postID);
            if (post != null)
            if ((DateTime.Now - post.CreateDate).TotalMinutes < 10 && post.UserId == userManager.GetUserAsync(User).Result.Id)
            {
                var postModel = new EditPostViewModel()
                {
                    Post = post,
                    ChanelID = post.Chanel.ID
                };
                return View(postModel);
            }
            //else
            //{
            //    ModelState.AddModelError("", "Probujesz edytowac post do ktorego nie masz dostepu");
            //    return RedirectToAction("Details", "Chanel", new { id = post.Chanel.ID });
            //}
            return RedirectToAction("Error", "Home");
        }

       // public IActionResult Error() => View();

        [HttpPost]
        public async Task<IActionResult> Edit (EditPostViewModel editPostViewModel)
        {   if (!ModelState.IsValid)
                return View();
            await postService.EditAsync(editPostViewModel.Post);
            return RedirectToAction ("Details", "Chanel", new { id = editPostViewModel.ChanelID });
        }
    }
}