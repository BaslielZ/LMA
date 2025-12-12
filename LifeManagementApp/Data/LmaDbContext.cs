
using LifeManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace LifeManagementApp.Data
{
    public class LmaDbContext : DbContext
    {
        public DbSet<DbNote> Notes { get; set; }

        private string _databasePath;

        public LmaDbContext()
        {
            // Database file will be stored in a safe app data directory
            _databasePath = Path.Combine(
                FileSystem.AppDataDirectory,
                "lma.db3"
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={_databasePath}");
        }
    }
}

