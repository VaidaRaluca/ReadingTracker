﻿using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadingTracker.Core
{

    public class AppConfig
    {
        public static bool ConsoleLogQueries = true;
        public static ConnectionStringsSettings ConnectionStrings { get; set; }
        public static JWTSettings JWTSettings { get; set; }

        public static void Init(IConfiguration configuration)
        {
            Configure(configuration);
        }

        private static void Configure(IConfiguration configuration)
        {
            ConnectionStrings = configuration.GetSection("ConnectionStrings").Get<ConnectionStringsSettings>();
            JWTSettings = configuration.GetSection("JWT").Get<JWTSettings>();
        }
    }
    public class ConnectionStringsSettings
    {
        public string? TickifyDatabase { get; set; }
    }
    public class JWTSettings
    {
        public string SecurityKey { get; set; }
    }

}
