using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ListaPostow.Models.Db
{
    public class Post
    {
        public Post()
        {
            CreateDate = DateTime.Now;
        }
        public int ID { get; set; }
        [Required(ErrorMessage = "Pole wymagane"), MaxLength]
        public string Text { get; set; }
        [Column(TypeName = "datetime2(7)")]
        public DateTime CreateDate { get; set; }
        public int UserId { get; set; }

        public User User { get; set; }
        public Chanel Chanel { get; set; }
    }
}
