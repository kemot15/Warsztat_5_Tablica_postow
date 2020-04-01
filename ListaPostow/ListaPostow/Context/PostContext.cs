using ListaPostow.Models;
using ListaPostow.Models.Db;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaPostow.Context
{
    public class PostContext : IdentityDbContext<User, IdentityRole<int>, int> 
    {
        public PostContext(DbContextOptions<PostContext> options) : base(options)
        {

        }

        
        public DbSet<Post> Posts { get; set; }
        public DbSet<Chanel> Chanels { get; set; }
    }
}
