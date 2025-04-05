using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consoles.Statics
{
    internal static class PersonExtension
    {
        internal static void PrintPerson(this Person person)
        {
            Console.WriteLine($"person.Id {person.Id}");
            Console.WriteLine($"person.Name: {person.Name}");
        }
    }
}
