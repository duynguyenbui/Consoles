using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Consoles.Statics
{
    internal class AccessCounter
    {
        private static int _counter = 0;
        private static AccessCounter _instance =  new AccessCounter();

        public int Counter
        {
            get
            {
                return _counter;
            }
        }

        public int Increase()
        {
            _counter++;
            return Counter;
        }

        public static AccessCounter GetInstance()
        {
            return _instance;
        }
    }
}
