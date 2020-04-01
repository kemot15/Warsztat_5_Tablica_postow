using ListaPostow.Models.Db;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaPostow.Models
{
    public class User : IdentityUser<int>
    {
        public ICollection<Post> Posts { get; set; }
        public ICollection<ChanelUsers> ChanelUsers { get; set; }
    }
}
