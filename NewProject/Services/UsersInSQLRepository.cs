using Microsoft.EntityFrameworkCore;
using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.Services
{
    public class UsersInSQLRepository : IUsers
    {
        MyDatabaseContext _db;

        public UsersInSQLRepository(MyDatabaseContext db)
        {
            _db = db;
        }

        public async Task<List<Listener>> GetAllUsers()
        {
            await Task.CompletedTask;
            return _db.Listeners.Where(c => c.IsDeleted == false).ToList();
        }

        public async Task<Listener> GetUser(Guid id)
        {
            var users = await _db.Listeners.Include(c => c.Performers).ToListAsync();

            var user = users.FirstOrDefault(c => c.ListenerId == id && c.IsDeleted == false);

            if (user == null) return null;

            return user;
        }

        public async Task<Listener> AddUser(string name, string surname)
        {
            var random = new Random();
            var user = new Listener()
            { 
                Name = name, 
                Surname = surname, 
                BirthDate = DateTime.MinValue.Add(TimeSpan.FromTicks((long)(random.NextDouble() * DateTime.MaxValue.Ticks))) 
            };

            await _db.Listeners.AddAsync(user);
            await _db.SaveChangesAsync();
            await _db.DisposeAsync();

            return user;
        }

        public async Task<bool> UpdateUser(Guid id, string name, string surname)
        {
            var tempUser = new Listener() { Name = name, Surname = surname };
            
            var user = await _db.Listeners.FirstOrDefaultAsync(c => c.ListenerId == id);

            if (user == null)
            {
                return false;
            }
            else
            {
                user.Name = tempUser.Name;
                user.Surname = tempUser.Surname;
            }

            await _db.SaveChangesAsync();
            await _db.DisposeAsync();

            return true;

        }
        public async Task<bool> DeleteUser(Guid id)
        {
            var user = await _db.Listeners.FirstOrDefaultAsync(c => c.ListenerId == id);

            if (user == null) return false;

            user.IsDeleted = true;

            await _db.SaveChangesAsync();
            await _db.DisposeAsync();

            return true;
        }

        public async Task<List<Song>> GetAllUserSongs(Guid id)
        {
            var user = await _db.Listeners.Include(c=>c.Songs).FirstOrDefaultAsync(c => c.ListenerId == id && c.IsDeleted == false);

            return user.Songs.ToList();
        }
    }

}
