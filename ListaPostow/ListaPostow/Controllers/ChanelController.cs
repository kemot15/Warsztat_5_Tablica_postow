using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListaPostow.Models;
using ListaPostow.Models.Db;
using ListaPostow.Models.ViewModels;
using ListaPostow.Services;
using ListaPostow.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;

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

        // GET: Chanel/Details/5
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            
            var chanelList = await _chanelService.GetAllChanelsAsync();
            var user = await UserManager.GetUserAsync(User);
            if (id == 0)
            {
                id = chanelList.FirstOrDefault(ch => ch.OwnerID == user.Id).ID;
            }
            
            var chanel = await _chanelService.ChanelDetailAsync(id, user);
            var chanelsWithPost = new ChanelsWithPostViewModel()
            {
                Chanels = chanelList,
                Chanel = chanel.Chanel,
                ChanelID = chanel.ChanelID,
                PostMessage = chanel.PostMessage,
                Visible = chanel.Visible
            };
            return View(chanelsWithPost);
        }      


        // GET: Chanel/Create
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Chanel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Chanel chanel)
        {
            var user = await UserManager.GetUserAsync(User);
            chanel.OwnerID = user.Id;            
            await _chanelService.CreateChanelAsync(chanel);
            return View();            
        }

        public async Task<IActionResult> AddFavorite(ChanelDetailViewModel chanel)
        {
            var user = await UserManager.GetUserAsync(User);
            await _chanelService.AddToFavoriteAsync(chanel.ChanelID, user, chanel.Visible);
            return RedirectToAction("Details", "Chanel", new { id = chanel.ChanelID });

        }



        public async Task<IActionResult> Favorite ()
        {
            var user = await UserManager.GetUserAsync(User);
            var chanelUser = await _chanelService.GetFavoritedUserChanelsAsync(user);
            return View(chanelUser);
        }
        
        [HttpGet]
        public async Task<IActionResult> FavoriteList()
        {
           
            var user = await UserManager.GetUserAsync(User);
            var chanels = await _chanelService.GetAllChanelsAsync();
            var newChanelList = new List<Chanel>();
            foreach (var chanel in chanels)
            {   
                var result = chanel.ChanelUsers.Where(ch => ch.ChanelID == chanel.ID && ch.User == user).Single();
                if (result == null)
                {
                    result.ID = chanel.ID;
                    result.UserID = user.Id;
                    result.Chanel = chanel;
                    result.User = user;
                    result.Visable = false;
                }
                chanel.ChanelUsers.Add(result);
                // chanelUser.Visable = result.Visable;                
                newChanelList.Add(chanel);
            }
            var model = new FavoriteViewModel(newChanelList);


            //var chanelsUser = await _chanelService.GetFavoritedUserChanelsAsync(user);
            //foreach( var chanel in chanels)
            //{
            //    var chanelVisability = new FavoriteViewModel();
            //    chanelVisability.Name = chanel.Name;
            //    chanelVisability.ID = chanel.ID;
            //    ChanelUsers? result = chanelsUser.SingleOrDefault(ch => ch.Chanel.Equals(chanel));
            //    //chanelVisability.Visable = (result == null) ? false : result.Visable;
            //    if (result == null)
            //    {
            //        chanelVisability.Visable = false;
            //    }
            //    else
            //    {
            //        chanelVisability.Visable = result.Visable;
            //    }
            //    model.Add(chanelVisability);   
            //} 

            return View(model);
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