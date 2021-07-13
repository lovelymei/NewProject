using Microsoft.EntityFrameworkCore;
using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.Services
{
    public class PerformersInSQLRepository : IPerformers
    {
        MyDatabaseContext _db;

        public PerformersInSQLRepository(MyDatabaseContext db)
        {
            _db = db;
        }

        public async Task<List<Performer>> GetAllPerformers()
        {
            await Task.CompletedTask;
            return _db.Performers.Where(c => c.IsDeleted == false).Include(c => c.Songs).ToList();
        }

        public async Task<Performer> GetPerformer(Guid id)
        {
            var performers = await _db.Performers.Include(c => c.Songs).ToListAsync();

            var performer = performers.FirstOrDefault(c => c.PerformerId == id && c.IsDeleted == false);

            if (performer == null) return null;

            return performer;
        }
        public async Task<Performer> GetPerformerByNickName(string nickname)
        {
            var performers = await _db.Performers.Include(c => c.Songs).ToListAsync();

            var performer = performers.FirstOrDefault(c => c.NickName.Contains(nickname) && c.IsDeleted == false);

            if (performer == null) return null;

            return performer;
        }
        public async Task<Performer> AddPerformer(string nickname)
        {
            var performer = new Performer()
            {
                NickName = nickname,
                BirthDate = DateTime.Now
            };

            await _db.Performers.AddAsync(performer);
            await _db.SaveChangesAsync();
            await _db.DisposeAsync();

            return performer;
        }

        public async Task<List<Song>> GetAllPerformerSongs(Guid id)
        {
            var performer = await _db.Performers.Include(c => c.Songs).FirstOrDefaultAsync(c => c.PerformerId == id && c.IsDeleted == false);

            return performer.Songs.ToList();
        }
        public async Task<bool> UpdatePerformer(Guid id, string nickname)
        {

            var random = new Random();
            var tempPerformer = new Performer()
            {
                NickName = nickname,
                BirthDate = DateTime.MinValue.Add(TimeSpan.FromTicks((long)(random.NextDouble()*DateTime.MaxValue.Ticks)))
            };

            var performer = await _db.Performers.FirstOrDefaultAsync(c => c.PerformerId == id);

            if (performer == null)
            {
                return false;
            }
            else
            {
                performer.NickName = tempPerformer.NickName;
                performer.BirthDate = tempPerformer.BirthDate;
            }

            await _db.SaveChangesAsync();
            await _db.DisposeAsync();

            return true;
        }

        public async Task<bool> DeletePerformer(Guid id)
        {
            var performer = await _db.Performers.FirstOrDefaultAsync(c => c.PerformerId == id);

            if (performer == null) return false;

            performer.IsDeleted = true;

            await _db.SaveChangesAsync();
            await _db.DisposeAsync();

            return true;
        }


    }
}
