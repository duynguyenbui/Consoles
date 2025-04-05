using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace Consoles.Statics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("App started");

            Parallel.For(0, 3, i =>
            {
                Console.WriteLine($"[Thread {i}] Getting database instance...");
                var db = DatabaseConnection.Instance;

                var users = db.GetFakeUsers();
                Console.WriteLine($"[Thread {i}] Got users: {string.Join(", ", users)} with {db.GetHashCode()}");
            });

            Console.WriteLine($"{DatabaseConnection.Instance.GetHashCode()} is connected with {DatabaseConnection.Instance.X}");


            var dbInstanceOther = new DatabaseConnection();
            Console.WriteLine($"{dbInstanceOther.GetHashCode()} is connected with {dbInstanceOther.X}");

            Console.WriteLine("Done. Press Enter to exit.");
            Console.ReadLine();
        }
    }
}
