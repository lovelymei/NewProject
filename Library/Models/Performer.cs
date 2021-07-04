using System;
using System.Collections.Generic;

#nullable disable

namespace NewProject.Models
{
    public partial class Performer
    {

        public Performer()
        {
            Listeners = new List<Listener>();
            Songs = new List<Song>();
            IsDeleted = false;
        }

        /// <summary>
        /// Идентификатор исполнителя
        /// </summary>
        public Guid PerformerId { get; set; }

        /// <summary>
        /// Ник
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// Дата рождения
        /// </summary>
        public DateTime BirthDate { get; set; }

        /// <summary>
        /// Не удален ли исполнитель
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Песни
        /// </summary>
        public virtual List<Song> Songs { get; set; }

        /// <summary>
        /// Слушатели
        /// </summary>
        public virtual List<Listener> Listeners { get; set; }
    }
}
