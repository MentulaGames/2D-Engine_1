using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Mentula.Engine.Core;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Matrix.Identity);
            Console.WriteLine("---------------------");
            Console.WriteLine(Matrix3.Identity.ToTableString());
            Console.ReadKey();
        }
    }
}
