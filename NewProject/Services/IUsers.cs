using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewProject.Services
{
    public interface IUsers
    {
        Task<Listener> AddUser(string name, string surname);
        Task<List<Listener>> GetAllUsers();
        Task<Listener> GetUser(Guid id);

        Task<bool> DeleteUser(Guid id);
        Task<List<Song>> GetAllUserSongs(Guid id);
    }
}