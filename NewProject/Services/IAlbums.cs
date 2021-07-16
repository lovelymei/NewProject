using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewProject.Services
{
    public interface IAlbums
    {
        Task<Album> AddAlbum(string title);
        Task<bool> AttachMusicSong(Guid albumId, Guid songId);
        Task<bool> DeleteAlbum(Guid id);
        Task<Album> GetAlbum(Guid id);
        Task<List<Album>> GetAllAlbums();
        Task<List<Album>> GetAllDeletedAlbums();
        Task<bool> RestoreAlbum(Guid id);
    }
}