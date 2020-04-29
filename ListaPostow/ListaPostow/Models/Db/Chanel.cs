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
        [StringLength(255)]
        [Required(ErrorMessage = "Pole wymagane")]
        public string Name { get; set; }
        public string Color { get; set; }
        public int OwnerID { get; set; }
        public User Owner { get; set; }
        public ICollection<Post> Posts { get; set; }
        public ICollection<ChanelUsers> ChanelUsers { get; set; }
    }
}
