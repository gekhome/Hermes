﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Principal;

namespace Hermes.DAL.Security
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }

        public bool IsInRole(string role)
        {
            if (roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public CustomPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Afm { get; set; }
        public string[] roles { get; set; }

    }

    public class CustomPrincipalSerializeModel
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Afm { get; set; }
        public string[] roles { get; set; }
    }
}