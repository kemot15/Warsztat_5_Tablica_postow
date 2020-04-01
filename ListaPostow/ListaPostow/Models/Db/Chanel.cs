using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ListaPostow.Models.Db
{
    public class Chanel
    {
        public int ID { get; set; }
        [Required, StringLength(255)]
        public string Name { get; set; }
        public string Color { get; set; }
        public int OwnerID { get; set; }
        public User User { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<ChanelUsers> ChanelUsers { get; set; }
    }
}
