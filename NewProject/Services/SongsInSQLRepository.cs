using Microsoft.EntityFrameworkCore;
using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.Services
{
    public class SongsInSQLRepository : ISongs
    {
        private readonly MyDatabaseContext _db;
        public SongsInSQLRepository(MyDatabaseContext db)
        {
            _db = db;
        }

        public async Task<List<Song>> GetAllSongs()
        {
            await Task.CompletedTask;
            return _db.Songs.Where(c => c.IsDeleted == false).ToList();
        }

        public async Task<Song> GetSong(Guid id)
        {
            var songs = await _db.Songs.ToListAsync();

            var song = songs.FirstOrDefault(c => c.SongId == id && c.IsDeleted == false);

            if (song == null) return null;

            return song;
        }

        public async Task<Song> GetSongByTitle(string title)
        {
            var songs = await _db.Songs.ToListAsync();

            var song = songs.FirstOrDefault(c => c.Title.Contains(title) && c.IsDeleted == false);

            if (song == null) return null;

            return song;
        }

        public async Task<bool> AddSong(string title, long duration)
        {
            var random = new Random();
            var newSong = new Song() 
            { 
                Title = title, 
                DurationMs = duration, 
                ProductionDate = DateTime.MinValue.Add(TimeSpan.FromTicks((long)(random.NextDouble() * DateTime.MaxValue.Ticks)))
            };

            var performer = await _db.Performers.FirstOrDefaultAsync(c => c.AccountId == newSong.PerformerId);

            if (performer == null) return false;

            await _db.Songs.AddAsync(newSong);
            await _db.SaveChangesAsync();
            await _db.DisposeAsync();

            return true;
        }
        
        public async Task<bool> DeleteSong(Guid id)
        {
            var song = await _db.Songs.FirstOrDefaultAsync(c => c.SongId == id);

            if (song == null) return false;

            song.IsDeleted = true;

            await _db.SaveChangesAsync();
            await _db.DisposeAsync();

            return true;
        }
    }
}
