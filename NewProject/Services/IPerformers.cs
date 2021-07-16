using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewProject.Services
{
    public interface IPerformers
    {
        Task<List<Song>> GetAllPerformerSongs(Guid id);
        Task<bool> AttachSong(Guid accountId, Guid songId);
        Task<bool> AttachAlbum(Guid accountId, Guid albumId);

    }
}