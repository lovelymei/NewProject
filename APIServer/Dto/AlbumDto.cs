using APIServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServer.Dto
{
    public class AlbumDto
    {
        public AlbumDto(Album album)
        {
            Name = album.Name;
            ProductionDate = album.ProductionDate;
            AccountId = album.AccountId;
        }
        public string Name { get; set; }
        public DateTime ProductionDate { get; set; }
        public Guid AccountId { get; set; }
    }
}
