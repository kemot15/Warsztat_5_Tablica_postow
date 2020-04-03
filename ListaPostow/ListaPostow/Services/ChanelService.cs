using ListaPostow.Context;
using ListaPostow.Models.Db;
using ListaPostow.Services.Interfaces;
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
        
    }
}
