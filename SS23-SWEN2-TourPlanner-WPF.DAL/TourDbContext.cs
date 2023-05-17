using Microsoft.EntityFrameworkCore;
using SS23_SWEN2_TourPlanner_WPF.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SS23_SWEN2_TourPlanner_WPF.DAL
{
    public class TourDbContext : DbContext
    {
        public DbSet<Tour> Tours { get; set; }
        public DbSet<TourLog> TourLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PostgreSQLConnectionString"].ConnectionString;
            optionsBuilder.UseNpgsql(connectionString);
        }
    }
}
