using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consoles.Statics
{
    internal class C
    {
        public static int x { get; set; } = 999;

        static C()
        {
            Console.WriteLine(x);
            x = 1000;
            Console.WriteLine("Init from static constructor");
        }
    }
}
