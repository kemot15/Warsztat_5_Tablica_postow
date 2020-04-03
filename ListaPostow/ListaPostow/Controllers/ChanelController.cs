using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ListaPostow.Models;
using ListaPostow.Models.Db;
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

        // GET: Chanel/Details/5
        public async Task<ActionResult> Details()
        {
            
            var user = await UserManager.GetUserAsync(User);
            var defaultChanel = await _chanelService.GetDefaultUserChanelAsync(user.Id);
            ViewBag.domyslny = defaultChanel.ID;
            var chanelUserList = await _chanelService.GetUserChanelsAsync(user.Id);
            return View(chanelUserList);
        }

        // GET: Chanel/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: Chanel/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Chanel chanel)
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
    }
}