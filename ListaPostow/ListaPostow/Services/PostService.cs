using ListaPostow.Context;
using ListaPostow.Models;
using ListaPostow.Models.Db;
using ListaPostow.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ListaPostow.Services
{
    public class PostService : IPostService
    {
        private readonly PostContext _context;

        public PostService(PostContext context)
        {
            _context = context;
        }

        public async Task<bool> AddPostAsync(string message, int chanelID, User user)
        {
            var chanel = _context.Chanels.SingleOrDefault(ch => ch.ID.Equals(chanelID));
            var post = new Post()
            {
                Text = message,
                Chanel = chanel,
                User = user
            };
            await _context.Posts.AddAsync(post);
            return await _context.SaveChangesAsync() >0;            
        }       
        
        public async Task<bool> DeleteAsync(int postID)
        {
            var post = await GetAsync(postID);
            _context.Remove(post);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Post> GetAsync (int postID)
        {
            var result = await _context.Posts.Include(ch => ch.Chanel).SingleOrDefaultAsync(p => p.ID == postID);
            if (result == null)
                return null;
            return  result;
        }

        public async Task<bool> EditAsync (Post post)
        {
            _context.Update(post);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<int> CountChanelPostAsync(int chanelID)
        {
            return await _context.Posts.Include(ch => ch.Chanel).Where(p => p.Chanel.ID == chanelID).CountAsync();
        }
    }
}
