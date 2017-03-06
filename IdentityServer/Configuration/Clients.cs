﻿using System.Collections.Generic;
using System.Linq;
using IdentityServer3.Core.Models;

namespace IdentityServer.Configuration
{
    public static class Clients
    {
        public static IEnumerable<Client> Get()
        {
            return new[]
            {
                new Client
                {
                    Enabled = true,
                    ClientName = "MVC Client",
                    ClientId = "mvc",
                    Flow = Flows.Implicit,

                    RedirectUris = new List<string>
                    {
                        "https://localhost:44319/"
                    },

                    AllowAccessToAllScopes = true
                }
            };
        }
    }
}