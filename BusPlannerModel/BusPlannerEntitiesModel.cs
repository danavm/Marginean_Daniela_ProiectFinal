using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace BusPlannerModel
{
    public partial class BusPlannerEntitiesModel : DbContext
    {
        public BusPlannerEntitiesModel()
            : base("name=BusPlannerEntitiesModel")
        {
        }

        public virtual DbSet<Bus> Buses { get; set; }
        public virtual DbSet<Driver> Drivers { get; set; }
        public virtual DbSet<Planner> Planners { get; set; }
        public virtual DbSet<Route> Routes { get; set; }
        public virtual DbSet<Shift> Shifts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }
}
