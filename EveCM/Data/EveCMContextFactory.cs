﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EveCM.Data
{
    public class EveCMContextFactory : IDesignTimeDbContextFactory<EveCMContext>
    {
        public EveCMContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<EveCMContext>();
            var connectionString = configuration.GetConnectionString("EveCM");
            builder.UseNpgsql(connectionString);
            return new EveCMContext(builder.Options);
        }
    }
}
