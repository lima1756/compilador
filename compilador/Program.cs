using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace compilador
{
    class Program
    {
        static void Main(string[] args)
        {            
            Console.WriteLine(Environment.GetCommandLineArgs()[1]);
            Console.ReadKey();
        }
    }
}
