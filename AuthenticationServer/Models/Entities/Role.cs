﻿using NewProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthenticationServer.Models
{
    public class Role
    {
        public Roles RoleId { get; set; }
        public string Name { get; set; }
        public IEnumerable<Account> Accounts{ get; set; }

        public Role()
        {
            Accounts = new List<Account>();
        }

    }

   
