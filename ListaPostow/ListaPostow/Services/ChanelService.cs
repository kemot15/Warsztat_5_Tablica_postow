using ListaPostow.Context;
using ListaPostow.Models;
using ListaPostow.Models.Db;
using ListaPostow.Models.ViewModels;
using ListaPostow.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public async Task<bool> CreateChanelAsync(Chanel chanel, User user)
        {
            await _context.Chanels.AddAsync(chanel);
            var result = await _context.SaveChangesAsync();
            await AddToFavoriteAsync(chanel.ID, user);
            return  result > 0;
        }

        public async Task<List<Chanel>> GetAllChanelsAsync()
        {
            return await _context.Chanels.Include(ch => ch.ChanelUsers).Include(p => p.Posts).ToListAsync();
        }

        public async Task<List<Chanel>> GetUserChanelsAsync(int id)
        {            
            return await _context.Chanels.Where(u => u.OwnerID == id).Include(u => u.Owner).ToListAsync();
        }

        public async Task<List<Chanel>> GetFavoritedUserChanelsAsync(User user)
        {     
            var result = (from chanel in _context.Chanels
                          join chanelUsers in _context.ChanelUsers on chanel.ID  equals chanelUsers.ChanelID
                          where chanelUsers.UserID == user.Id && chanelUsers.Visable
                          select chanel);            
            return await result.Include(p => p.Posts).Include(ch => ch.ChanelUsers).ThenInclude(u => u.User).ToListAsync();
        }

        public async Task<Chanel> GetDefaultUserChanelAsync(int id)
        {
            return await _context.Chanels.Where(u => u.OwnerID == id).OrderBy(u => u.ID).FirstAsync();
        }

        public async Task<ChanelDetailViewModel> ChanelDetailAsync(int id, User user)
        {
            var chanel = await _context.Chanels.Include(p => p.Posts).ThenInclude(u => u.User).Include(u => u.ChanelUsers).SingleAsync(c => c.ID == id);
            
            var visable = _context.ChanelUsers.SingleOrDefault(u => u.ChanelID.Equals(id) && u.User.Equals(user));
            var result = visable == null ? false : visable.Visable;
            var details = new ChanelDetailViewModel()
            {
                Chanel = chanel,
                Visible = result
            };            
            return details;
        }

        public async Task<bool> AddToFavoriteAsync(int chanelId, User user, bool visible = true)
        {
            var result = _context.ChanelUsers.SingleOrDefault(u => u.ChanelID.Equals(chanelId) && u.User.Equals(user));
            
            var chanel = _context.Chanels.Single(ch => ch.ID.Equals(chanelId));
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
            var mainChanel = _context.ChanelUsers.First(u => u.User.Equals(user));
            if (mainChanel.Equals(result)) //brak mozliwosci wylaczenia glownego kanalu z obserwowanych
                return true; // result.Visable = true;
            else
                result.Visable = visible ? false : true;         
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteChanelAsync(int chanelID, User user)
        {
            Chanel? chanel = await _context.Chanels.Include(p => p.Posts).Include(l => l.ChanelUsers).SingleAsync(ch => ch.ID.Equals(chanelID));
            if (chanel == null)
                return false;
            var mainChanel = _context.Chanels.First(u => u.OwnerID == user.Id).ID;
            if (chanel.OwnerID == user.Id && mainChanel != chanel.ID)
            {
                _context.Posts.RemoveRange(chanel.Posts);
                _context.ChanelUsers.RemoveRange(chanel.ChanelUsers);
                _context.Chanels.Remove(chanel);                
            }
            
            return await _context.SaveChangesAsync() > 0;
        }

    }
}
