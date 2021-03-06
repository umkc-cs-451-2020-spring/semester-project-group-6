﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CommerceApi.initial_setup;
using CommerceApi.dao;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace CommerceApi {
    public class Program {
        public static void Main(string[] args) {
            // Fill the database when running the program (only needs done once)
            // FillDB fill = new FillDB();
            // fill.populateDatabase();

            CreateWebHostBuilder(args).Build().Run();
        }
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}