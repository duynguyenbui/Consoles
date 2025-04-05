using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Consoles.Statics
{
    internal class DatabaseConnection
    {
        public static DatabaseConnection Instance => _instance.Value;

        private static readonly Lazy<DatabaseConnection> _instance =
            new(() =>
            {
                var instance = new DatabaseConnection();
                instance.Connect();
                return instance;
            }, LazyThreadSafetyMode.ExecutionAndPublication);

        private static readonly Random random = new Random();
        public int X { get; set; } = random.Next(100, 3000);

        public DatabaseConnection()
        {
            Console.WriteLine("Init database from inside class");
        }

        private void Connect()
        {
            Console.WriteLine($"{_instance.GetHashCode()} is connected");
        }

        public List<string> GetFakeUsers(int count = 5)
        {
            var users = new List<string>();
            for (int i = 0; i < count; i++)
            {
                users.Add($"User_{random.Next(1000, 9999)}");
            }
            return users;
        }
    }
}
