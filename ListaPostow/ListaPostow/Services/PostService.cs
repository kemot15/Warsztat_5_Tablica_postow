using ListaPostow.Context;
using ListaPostow.Models;
using ListaPostow.Models.Db;
using ListaPostow.Services.Interfaces;
using System;
using System.Collections.Generic;
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
            var post = _context.Posts.Single(p => p.ID == postID);
            _context.Remove(post);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
