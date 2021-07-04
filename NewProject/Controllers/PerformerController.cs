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
        /// Получить всех исполнителей
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<PerformerDto>>> GetAllPerformers()
        {
            var performers = await _performers.GetAllPerformers();
            List<PerformerDto> performersDto = new List<PerformerDto>();

            foreach (var performer in performers)
            {
                performersDto.Add(new PerformerDto(performer));
            }

            return Ok(performersDto);
        }

        /// <summary>
        /// Получить исполнителя по идентификатоу
        /// </summary>
        /// <param name="id"> Идентификатор</param>
        /// <returns></returns>
        [HttpGet("{NickName}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PerformerDto>> GetPerformer(Guid id)
        {
            var performer = await _performers.GetPerformer(id);

            if (performer == null) return NotFound();

            return new PerformerDto(performer);
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
        /// <summary>
        /// Добавить нового исполнителя
        /// </summary>
        /// <param name="nickname"> Псевдоним исполнителя</param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<PerformerDto>> AddNewPerformer(string nickname)
        {
            var performer = await _performers.AddPerformer(nickname);
            return new PerformerDto(performer);
        }

    }
}
