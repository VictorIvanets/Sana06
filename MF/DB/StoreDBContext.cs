using MF.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

using System;

namespace MF.DB
{
    public class StoreDBContext : IdentityDbContext
    {
        public StoreDBContext(DbContextOptions<StoreDBContext> options) : base(options) {         }

        public DbSet<Store> Store { get; set; }
        public DbSet<TypesFish> TypesFish { get; set; }
        public DbSet<Fishes> Fishes { get; set; }
        public DbSet<Coords> Coords { get; set; }
        public DbSet<PlaceDb> PlaceDb { get; set; }
        public DbSet<PlaceFishCollection> PlaceFishCollection { get; set; }
        public DbSet<AppUser> ApplicationUser { get; set; }

    }
}
