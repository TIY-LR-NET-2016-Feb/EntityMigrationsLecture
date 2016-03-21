using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EntityMigrationsLecture
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class RentACarDbModel : DbContext
    {
        // Your context has been configured to use a 'RentACarDbModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'EntityMigrationsLecture.RentACarDbModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'RentACarDbModel' 
        // connection string in the application configuration file.
        public RentACarDbModel()
            : base("name=RentACarDbModel")
        {
        }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
    }

    //POCO 

    public class Location
    {
        public int Id { get; set; }
        public string Address { get; set; }    
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }

        public virtual Employee Manager { get; set; }

        public virtual ICollection<Vehicle> Inventory { get; set; } = new List<Vehicle>();
        public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();
    }

    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }

    public class Vehicle
    {
        public int Id { get; set; }

        [Required]
        public string Make { get; set; }
        [Required]
        public string Model { get; set; }

        public int Year { get; set; }

        [Required]
        public string VIN { get; set; }

        public virtual ICollection<RentalHistory> History { get; set; }
        public virtual Location HomeLocation { get; set; }
    }

  
    public enum RentalAction
    {
        Rented,
        ReturnedByCustomer,
        Cleaned,
        Repaired,
        FilledUp,
        ReturnedToLot,


    }
    public class RentalHistory
    {
        public int Id { get; set; }
        public Vehicle Vehicle { get; set; }
        public RentalAction ActionTaken { get; set; }
        public DateTime StatusDate { get; set; }
    }
}