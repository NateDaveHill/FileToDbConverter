using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace _76_FileToDbConverter
{
    internal class AppContext : DbContext
    {
        public DbSet<Share> Shares { get; set; }
        public DbSet<User> Users{ get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source = (localDb)\\MSSQLLocalDb; Initial Catalog = WallstreetDb; Integrated Security = true;");
        }
    }
}
