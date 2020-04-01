using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ListaPostow.Models.Db
{
    public class ChanelUsers
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ChanelID { get; set; }
        [ForeignKey("UserID")]
        public User User { get; set; }
        [ForeignKey("ChanelID")]
        public Chanel Chanel { get; set; }
    }
}
