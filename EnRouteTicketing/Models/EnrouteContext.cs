using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace EnRouteTicketing.Models
{
    public class EnRouteAppContext : DbContext
    {
        public DbSet<BusService> BusServices { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Terminal> Terminals { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Commuter> Commuters { get; set; }
        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<Review> Reviews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
           // Database.SetInitializer<EnRouteTicketingContext>(new DropCreateDatabaseIfModelChanges<EnRouteTicketingContext>());
            //Database.SetInitializer<EnRouteTicketingContext>(null);
            //base.OnModelCreating(modelBuilder);
        }

       
    }
}
