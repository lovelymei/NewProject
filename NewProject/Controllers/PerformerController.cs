using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NewProject.Models;
using NewProject.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PerformerController : ControllerBase
    {
        IPerformers _performers;

        public PerformerController(IPerformers performers)
        {
            _performers = performers;
        }

        /// <summary>
        /// Получить все песни исполнителя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<SongDto>>> GetAllPerformersSongs(Guid id)
        {
            var songs = await _performers.GetAllPerformerSongs(id);
            List<SongDto> songsDto = new List<SongDto>();
            foreach (var song in songs)
            {
                songsDto.Add(new SongDto(song));
            }

            return Ok(songsDto);
        }

    }
}
