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
        private readonly IUsers _users;
        public ListenerController(IUsers users)
        {
            _users = users;
        }

        /// <summary>
        /// Получить всех пользователей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<List<ListenerDto>>> GetAllUsers()
        {
            var users = await _users.GetAllUsers();
            List<ListenerDto> usersDto = new List<ListenerDto>();

            foreach (var user in users)
            {
                usersDto.Add(new ListenerDto(user));
            }

            return Ok(usersDto);
        }

        /// <summary>
        /// Получить пользователя по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ListenerDto>> GetUser(Guid id)
        {
            var user = await _users.GetUser(id);

            if (user == null) return NotFound();

            return new ListenerDto(user);
        }

        /// <summary>
        /// Добавить нового пользователя
        /// </summary>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<ListenerDto>> AddUser(string name, string surname)
        { 
            var user = await _users.AddUser(name, surname);
            return Ok(user); 
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
            var songs = await _users.GetAllUserSongs(id);
            List<SongDto> songsDto = new List<SongDto>();
            foreach (var song in songs)
            {
                songsDto.Add(new SongDto(song));
            }

            return Ok(songsDto);
        }

        /// <summary>
        /// Удалить пользователя
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteSong(Guid id)
        {
            var isDeleted = await _users.DeleteUser(id);
            return isDeleted ? Ok() : NotFound();
        }

    }
}
