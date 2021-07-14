using Microsoft.EntityFrameworkCore;
using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.Services
{
    public class ListenersInSQLRepository : IListeners
    {
        MyDatabaseContext _db;

        public ListenersInSQLRepository(MyDatabaseContext db)
        {
            _db = db;
        }

        public async Task<List<Song>> GetAllListenerSongs(Guid id)
        {
            var user = await _db.Listeners.Include(c=>c.Songs).FirstOrDefaultAsync(c => c.ListenerId == id && c.IsDeleted == false);

            return user.Songs.ToList();
        }
    }

}
