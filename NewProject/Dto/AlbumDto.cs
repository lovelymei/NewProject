using NewProject.Models;
using System;

namespace NewProject.Dto
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
