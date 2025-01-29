using MF.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace MF.DB
{
    public class StoreDBContext : DbContext
    {
        public StoreDBContext(DbContextOptions<StoreDBContext> options) : base(options) {         }

        public DbSet<Store> Store { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<TypesFish> TypesFish { get; set; }
        public DbSet<Fishes> Fishes { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Coords> Coords { get; set; }
        public DbSet<PlaceDb> PlaceDb { get; set; }
        public DbSet<PlaceFishCollection> PlaceFishCollection { get; set; }

    }
}
