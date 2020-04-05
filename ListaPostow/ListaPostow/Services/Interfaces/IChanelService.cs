﻿using ListaPostow.Models;
using ListaPostow.Models.Db;
using ListaPostow.Models.ViewModels;
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
        Task<ChanelDetailViewModel> ChanelDetailAsync(int id, User user);
        Task<bool> AddToFolowAsync(int id, User user, bool visible);
    }
}
