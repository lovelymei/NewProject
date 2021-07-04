using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewProject.Services
{
    public interface IPerformers
    {
        Task<Performer> AddPerformer(string nickname);
        Task<bool> DeletePerformer(Guid id);
        Task<List<Performer>> GetAllPerformers();
        Task<Performer> GetPerformer(Guid id);
        Task<Performer> GetPerformerByNickName(string nickname);
        Task<List<Song>> GetAllPerformerSongs(Guid id);
        Task<bool> UpdatePerformer(Guid id, string nickaname);
    }
}