using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewProject.Services
{
    public interface IUsers
    {
        Task<Listener> AddListener(string name, string surname);
        Task<List<Listener>> GetAllListeners();
        Task<Listener> GetListener(Guid id);

        Task<bool> DeleteListener(Guid id);
        Task<List<Song>> GetAllListenerSongs(Guid id);
    }
}