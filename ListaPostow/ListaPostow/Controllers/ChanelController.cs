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

namespace ListaPostow.Controllers
{
    public class ChanelController : Controller
    {
        private readonly UserManager<User> UserManager;
        private readonly IChanelService _chanelService;

        public ChanelController(UserManager<User> userManager, IChanelService chanelService)
        {
            UserManager = userManager;
            _chanelService = chanelService;
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
            var user = await UserManager.GetUserAsync(User);
            var chanel = await _chanelService.ChanelDetailAsync(id, user);            
            return View(chanel);
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

        // GET: Chanel/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Chanel/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Chanel/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Chanel/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public async Task<IActionResult> Falow (ChanelDetailViewModel chanel)
        {
            var user = await UserManager.GetUserAsync(User);
            await _chanelService.AddToFolowAsync(chanel.ChanelID, user, chanel.Visible);
            return RedirectToAction("Details", "Chanel", new { id = chanel.ChanelID });

        }
        
    }
}