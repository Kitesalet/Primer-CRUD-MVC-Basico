using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace WebAppEnFr.DAL
{
    public partial class EscritoresDBContext : DbContext
    {
        public EscritoresDBContext()
        {
        }

        public virtual DbSet<Escritor> Escritores { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {            
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()

               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json")
               .Build();

                string connectionString = configuration.GetConnectionString("conn_string_excritores");

                optionsBuilder.UseSqlServer(connectionString);
            }
        }

    }
}
