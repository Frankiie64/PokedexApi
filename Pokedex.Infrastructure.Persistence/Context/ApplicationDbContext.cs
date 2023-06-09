using Microsoft.EntityFrameworkCore;
using Pokedex.Core.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokedex.Infrastructure.Persistence.Context
{
    public class ApplicationDbContext:DbContext
    {
        public virtual DbSet<TypePokemon> TypePokemon { get; set; }
        public virtual DbSet<Region> Region { get; set; }
        public virtual DbSet<Pokemon> Pokemon { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> opt) : base(opt) { }
     
        protected override void OnModelCreating(ModelBuilder mb)
        {
            // Definition tables
            mb.Entity<TypePokemon>().ToTable("TypePokemon");
            mb.Entity<Region>().ToTable("Region");
            mb.Entity<Pokemon>().ToTable("Pokemon");


            // Primary Key
            mb.Entity<TypePokemon>().HasKey(x => x.Id);
            mb.Entity<Region>().HasKey(x => x.Id);
            mb.Entity<Pokemon>().HasKey(x => x.Id);

            // Foreign Key

            mb.Entity<TypePokemon>()
          .HasMany<Pokemon>(x => x.Pokemons)
          .WithOne(x => x.TypePokemon)
          .HasForeignKey(x => x.TypeId)
          .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

            mb.Entity<Region>()
          .HasMany<Pokemon>(x => x.Pokemons)
          .WithOne(x => x.Region)
          .HasForeignKey(x => x.RegionId)
          .OnDelete(deleteBehavior: DeleteBehavior.NoAction);

            // Required Properties

            mb.Entity<Pokemon>(e =>
            {
                e.Property(p => p.Id).IsRequired();
                e.Property(p => p.RegionId).IsRequired();
                e.Property(p => p.TypeId).IsRequired();
                e.Property(p => p.Name).IsRequired();
                e.Property(p => p.UrlPhoto).IsRequired();            
            });

            mb.Entity<TypePokemon>(e =>
            {
                e.Property(p => p.Id).IsRequired();
                e.Property(p=> p.UrlPhoto).IsRequired();
                e.Property(p => p.Name).IsRequired();
                e.Property(p => p.Description).IsRequired();
            });

            mb.Entity<Region>(e =>
            {
                e.Property(p => p.Id).IsRequired();
                e.Property(p => p.Name).IsRequired();
                e.Property(p => p.Description).IsRequired();
            });
        }
    }
}
