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
    public class ChanelController : Controller
    {
        private readonly UserManager<User> UserManager;
        private readonly IChanelService _chanelService;
        private readonly IPostService _postService;

        public ChanelController(UserManager<User> userManager, IChanelService chanelService, IPostService postService)
        {
            UserManager = userManager;
            _chanelService = chanelService;
            _postService = postService;
        }

        // GET: Chanel
        public ActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> List()
        {
            var chanels = await _chanelService.GetAllChanelsAsync();
            return View(chanels);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id, int? page, bool favoriteChanels)
        {
            int currentPage = page ?? 1;
            ViewBag.Page = currentPage;
            var user = await UserManager.GetUserAsync(User);
            var chanelList = favoriteChanels ? await _chanelService.GetFavoritedUserChanelsAsync(user) : await _chanelService.GetAllChanelsAsync();
            ViewBag.ShowFavoriteChanels = favoriteChanels;
            var test = await _chanelService.GetAllChanelsAsync();
            var mainChanel = chanelList.First(ch => ch.OwnerID == user.Id).ID;
            if (id == 0)
            {
                id = mainChanel;
            }
            ViewBag.MainChanelId = mainChanel;
            var chanel = await _chanelService.ChanelDetailAsync(id, user);
            chanel.PageSize = 5;
            chanel.PageMax = (int)Math.Ceiling((double)chanel.Chanel.Posts.Count / chanel.PageSize);            
            chanel.Chanel.Posts = chanel.Chanel.Posts.Skip((currentPage - 1) * chanel.PageSize).Take(chanel.PageSize).ToList();
            var chanelsWithPost = new ChanelsWithPostViewModel()
            {
                Chanels = chanelList,
                Chanel = chanel.Chanel,
                ChanelID = chanel.ChanelID,
                PostMessage = chanel.PostMessage,
                Visible = chanel.Visible,
                PageSize = chanel.PageSize,
                PageMax = chanel.PageMax
            };
            return View(chanelsWithPost);
        }     

        // GET: Chanel/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Chanel chanel)
        {
            var user = await UserManager.GetUserAsync(User);
            chanel.OwnerID = user.Id;            
            await _chanelService.CreateChanelAsync(chanel, user);
            return View();            
        }

        public async Task<IActionResult> AddFavorite(ChanelDetailViewModel chanel, bool favoriteChanels)
        {
            var user = await UserManager.GetUserAsync(User);
            await _chanelService.AddToFavoriteAsync(chanel.ChanelID, user, chanel.Visible);
            return RedirectToAction("Details", "Chanel", new { id = chanel.ChanelID, favoriteChanels = favoriteChanels });
        }

        public async Task<IActionResult> Favorite ()
        {
            var user = await UserManager.GetUserAsync(User);
            var chanelUser = await _chanelService.GetFavoritedUserChanelsAsync(user);
            return View(chanelUser);
        }
        
        
        public async Task<IActionResult> FavoriteList(int chanelID, bool visible)
        {
            var user = await UserManager.GetUserAsync(User);
            await _chanelService.AddToFavoriteAsync(chanelID, user, visible);
            return RedirectToAction("List");
        }

        //public async Task<IActionResult> FavoriteList1(IList<FavoriteViewModel> model)
        public async Task<IActionResult> FavoriteList1(FavoriteViewModel model)
        {
            var user = await UserManager.GetUserAsync(User);
            foreach (var chanel in model.Chanels)
            {
                await _chanelService.AddToFavoriteAsync(chanel.ID, user, !chanel.ChanelUsers.First().Visable);
            }
            return RedirectToAction("Favorite");
        }

    }
}