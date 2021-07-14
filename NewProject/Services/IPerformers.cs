﻿using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NewProject.Services
{
    public interface IPerformers
    {
        Task<List<Song>> GetAllPerformerSongs(Guid id);
    }
}