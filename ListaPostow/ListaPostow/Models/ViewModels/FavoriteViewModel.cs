using ListaPostow.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaPostow.Models.ViewModels
{
    public class FavoriteViewModel
    {
        //public int ID { get; set; }
        //public string Name { get; set; }
        ////public Chanel Chanel { get; set; }
        //public bool Visable { get; set; }
        public List<Chanel> Chanels { get; set; }

        public FavoriteViewModel()
        {
        }

        public FavoriteViewModel(List<Chanel> chanels)
        {
            Chanels = chanels ?? throw new ArgumentNullException(nameof(chanels));
        }
    }
}
