using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace BysykkelDatafetcher
{
    public class StationContext : DbContext
    {
        public DbSet<Station> Stations { get; set; }
        public string connectionString { get; }

        public StationContext()
        {
            connectionString = Environment.GetEnvironmentVariable("OsloBySykkelDbConnectionString");
        }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options.UseSqlServer(connectionString);


    }
    
    public class Station
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string station_id { get; set; }
        public int is_installed { get; set; }
        public int is_renting { get; set; }
        public int is_returning { get; set; }
        public int last_reported { get; set; }
        public int num_bikes_available { get; set; }
        public int num_docks_available { get; set; }

        // from station_information
        public string name { get; set; }
        public string address { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public int capacity { get; set; }

    }
}
