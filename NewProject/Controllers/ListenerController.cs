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
    public class ListenerController : ControllerBase
    {
        private readonly IListeners _users;
        public ListenerController(IListeners users)
        {
            _users = users;
        }

        /// <summary>
        /// Получить все песни пользователя
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetSongs/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<SongDto>>> GetAllUsersSongs(Guid id)
        {
            var songs = await _users.GetAllListenerSongs(id);
            List<SongDto> songsDto = new List<SongDto>();
            foreach (var song in songs)
            {
                songsDto.Add(new SongDto(song));
            }

            return Ok(songsDto);
        }
    }
}
