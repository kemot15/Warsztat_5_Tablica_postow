using ListaPostow.Models.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ListaPostow.Services.Interfaces
{
    public interface IChanelService
    {
        Task<bool> CreateChanelAsync(Chanel chanel);
        Task<List<Chanel>> GetAllChanelsAsync();
        Task<List<Chanel>> GetUserChanelsAsync(int id);
        Task<Chanel> GetDefaultUserChanelAsync(int id);
    }
}
