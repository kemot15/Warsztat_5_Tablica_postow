using ListaPostow.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaPostow.Models.ViewModels
{
    public class ChanelVisableViewModel
    {
        public Chanel MainChanel { get; set; }
        public List<Chanel> Chanels { get; set; }
    }
}
