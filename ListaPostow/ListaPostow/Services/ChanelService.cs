using ListaPostow.Context;
using ListaPostow.Models;
using ListaPostow.Models.Db;
using ListaPostow.Models.ViewModels;
using ListaPostow.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaPostow.Services
{
    public class ChanelService : IChanelService
    {
        private readonly PostContext _context;

        public ChanelService(PostContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateChanelAsync(Chanel chanel)
        {
            await _context.Chanels.AddAsync(chanel);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<List<Chanel>> GetAllChanelsAsync()
        {
            return _context.Chanels.ToList();
        }

        public async Task<List<Chanel>> GetUserChanelsAsync(int id)
        {            
            return await _context.Chanels.Where(u => u.OwnerID == id).Include(u => u.Owner).ToListAsync();
        }

        public async Task<Chanel> GetDefaultUserChanelAsync(int id)
        {
            return await _context.Chanels.Where(u => u.OwnerID == id).OrderBy(u => u.ID).FirstAsync();
        }

        public async Task<ChanelDetailViewModel> ChanelDetailAsync(int id, User user)
        {
            var chanel = await _context.Chanels.Include(p => p.Posts).Include(u => u.ChanelUsers).SingleAsync(c => c.ID == id);
            
            var visable = _context.ChanelUsers.SingleOrDefault(u => u.ChanelID.Equals(id) && u.User.Equals(user));
            var result = visable == null ? false : visable.Visable;
            var details = new ChanelDetailViewModel()
            {
                Chanel = chanel,
                Visible = result
            };            
            return details;
        }

        public async Task<bool> AddToFolowAsync(int id, User  user, bool visible)
        {
            var result = _context.ChanelUsers.SingleOrDefault(u => u.ChanelID.Equals(id) && u.User.Equals(user));
            var chanel = _context.Chanels.Single(ch => ch.ID.Equals(id));
            if (result == null)
            {
                var chanelUser = new ChanelUsers()
                {
                    User = user,
                    Chanel = chanel,
                    Visable = true

                };
                await _context.AddAsync(chanelUser);
                return await _context.SaveChangesAsync() > 0;
            }
            result.Visable = visible ? false : true;
            _context.Update(result);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
