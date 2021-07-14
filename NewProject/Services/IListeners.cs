using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewProject.Services
{
    public interface IListeners
    {
        Task<List<Song>> GetAllListenerSongs(Guid id);
    }
}