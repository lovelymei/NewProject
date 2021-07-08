using System;
using System.Collections.Generic;

#nullable disable

namespace NewProject.Models
{
    public class Song
    {

        /// <summary>
        /// Идентификатор песни
        /// </summary>
        public Guid SongId { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Длительность
        /// </summary>
        public long DurationMs { get; set; }

        /// <summary>
        /// Дата выпуска
        /// </summary>
        public DateTime ProductionDate { get; set; }

        /// <summary>
        /// Не удалена ли песня
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Идентификатор исполнителя
        /// </summary>
        public Guid PerformerId { get; set; }

        /// <summary>
        /// Идентификатор слушателя
        /// </summary>
        public Guid? ListenerId { get; set; }

        /// <summary>
        /// Исполнитель
        /// </summary>
        public virtual Performer Performer { get; set; }

        /// <summary>
        /// Слушатель
        /// </summary>
        public virtual Listener Listener { get; set; }

        public Song()
        {
            IsDeleted = false;
        }

    }
}
