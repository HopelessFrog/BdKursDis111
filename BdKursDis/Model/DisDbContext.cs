using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BdKursDis.Model
{
    public  class DisDbContext : DbContext
    {
        public DisDbContext() : base()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source= dis.db");
        }

        public DbSet<Equipment> Equipments { get; set; }

        public DbSet<Ingredient> Ingredients { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<RecipeIngridient> RecipeIngridients { get; set;}

        public DbSet<Stage> Stages { get; set; }

        public DbSet<TechnicalStage> TechnicalStages{ get; set; }

        public DbSet<Unit> Units { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
