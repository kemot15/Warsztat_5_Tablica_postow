using ListaPostow.Models;
using ListaPostow.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaPostow.Services.Interfaces
{
    public interface IPostService
    {
        Task<bool> AddPostAsync(string message, int chanelID, User user);
        Task<bool> DeleteAsync(int postID);
        Task<Post> GetAsync(int postID);
        Task<bool> EditAsync(Post post);
    }
}
