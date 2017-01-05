using GigHub.Core;

namespace GigHub.DataLayer.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GigHub.DataLayer.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GigHub.DataLayer.ApplicationDbContext context)
        {
            context.Genres.AddOrUpdate(g => g.Name, new Genre { Id=1, Name = "Jazz" });
            context.Genres.AddOrUpdate(g => g.Name, new Genre { Id=2, Name = "Blues" });
            context.Genres.AddOrUpdate(g => g.Name, new Genre { Id=3, Name = "Rock" });
            context.Genres.AddOrUpdate(g => g.Name, new Genre { Id=4, Name = "Country" });


            //context.Gigs.AddOrUpdate(new Gig
            //{
            //    Genre = context.Genres.First(),
            //    DateTime = DateTime.MaxValue,
            //    Artist = context.Users.First(),
            //    Venue = "Venue 1"
            //});

            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
