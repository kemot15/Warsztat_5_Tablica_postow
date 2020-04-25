using ListaPostow.Models.Db;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ListaPostow.Models.ViewModels
{
    public class ChanelDetailViewModel
    {       
        public Chanel Chanel { get; set; }
        [Required(ErrorMessage = "Nie może być puste"), MaxLength]
        public string PostMessage { get; set; }
        public int ChanelID { get; set; }
        public bool Visible { get; set; }
        public int PageMax { get; set; }
        public int PageSize { get; set; }
    }
}
