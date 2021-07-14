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

        public async Task<List<Song>> GetAllPerformerSongs(Guid id)
        {
            var performer = await _db.Performers.Include(c => c.Songs).FirstOrDefaultAsync(c => c.AccountId == id && c.IsDeleted == false);

            return performer.Songs.ToList();
        }

    }
}
