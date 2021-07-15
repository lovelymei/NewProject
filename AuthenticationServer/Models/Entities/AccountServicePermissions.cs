using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NewProject.AuthenticationServer.Models.Entities
{
    public class AccountServicePermissions
    {
        private Dictionary<string, bool> _values;
        public Guid Id { get; set; }
        public DateTime? Timestamp { get; set; }
        public Guid? SetByAccountId { get; set; }

        public Dictionary<string, bool> Values
        {
            get => _values ??= new Dictionary<string, bool>();
            set => _values = value;
        }
    }
}
