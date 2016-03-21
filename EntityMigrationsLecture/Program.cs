using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityMigrationsLecture
{
    class Program
    {
        static void Main(string[] args)
        {

            var db = new RentACarDbModel();
            db.Database.Log += Console.Write;
            db.Database.Log += Test;

            List<Vehicle> allCars = db.Vehicles.ToList();

            var allCars2 = (from v in db.Vehicles
                group v by v.Make
                into g
                select new {Make = g.Key, Count = g.Count(), Message = "hello"}).ToList();


            foreach (var row in allCars2)
            {
                if (row.Count > 2)
                {
                    //row.Message = "We got a lot of Cars";
                }
            }
            Console.ReadLine();
        }

        static void Test(string s)
        {

            return;
        }
    }
}
