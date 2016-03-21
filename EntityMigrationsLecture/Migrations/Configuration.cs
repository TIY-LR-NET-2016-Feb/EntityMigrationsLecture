using FizzWare.NBuilder;

namespace EntityMigrationsLecture.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EntityMigrationsLecture.RentACarDbModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EntityMigrationsLecture.RentACarDbModel db)
        {
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

            if (!db.Locations.Any())
            {
                var locations = Builder<Location>.CreateListOfSize(3)
                    .All()
                    .With(m => m.Address = Faker.Address.StreetAddress())
                    .With(m => m.City = Faker.Address.City())
                    .With(m => m.State = Faker.Address.UsStateAbbr())
                    .With(m => m.Zip = Faker.Address.ZipCode())
                    .With(m => m.Manager = new Employee() { FirstName = Faker.Name.First(), LastName = Faker.Name.Last()})
                .Build();

                db.Locations.AddOrUpdate(c => c.Id, locations.ToArray());
            }
            if (!db.Vehicles.Any())
            {
                var vehicles = Builder<Vehicle>.CreateListOfSize(300)
                   .All()
                       .With(m => m.Make = Faker.Lorem.Words(1).First())
                       .With(m => m.Model = Faker.Lorem.Words(1).First())
                       .With(m => m.Year = Faker.RandomNumber.Next(2010, 2016))
                       .With(m => m.VIN = Faker.RandomNumber.Next(100000000, 900000000).ToString())
                       .With(m => m.HomeLocation = db.Locations.Find(Faker.RandomNumber.Next(1,3)))
                   .Build();

                db.Vehicles.AddOrUpdate(c => c.Id, vehicles.ToArray());
            }
        }
    }
}
