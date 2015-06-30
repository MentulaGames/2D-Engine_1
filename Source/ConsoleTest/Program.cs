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
        static Matrix m;
        static Matrix3 m3;

        static Vector2 v;
        static Vect2 v2;

        static Program()
        {
            m = Matrix.Identity;
            m3 = Matrix3.Identity;

            v = Vector2.One;
            v2 = Vect2.One;
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Microsoft\tMentula\n");

            DisplayMatrix();
            DisplayVector();

            float rads = MathHelper.ToRadians(90);

            m = Matrix.Multiply(m, Matrix.CreateRotationZ(rads));
            m3 = Matrix3.Multiply(m3, Matrix3.ApplyRotation(rads));

            v = Vector2.Transform(v, m);
            v2 = Vect2.Transform(v2, m3);

            DisplayMatrix();
            DisplayVector();

            Console.ReadKey();
        }

        static void DisplayMatrix()
        {
            Console.WriteLine("|{0} {1} {2} {3}|\t|{4} {5} {6}|", m.M11, m.M12, m.M13, m.M14, m3.A, m3.B, m3.C);
            Console.WriteLine("|{0} {1} {2} {3}|\t|{4} {5} {6}|", m.M21, m.M22, m.M23, m.M24, m3.D, m3.E, m3.F);
            Console.WriteLine("|{0} {1} {2} {3}|\t|{4} {5} {6}|", m.M31, m.M32, m.M33, m.M34, m3.G, m3.H, m3.I);
            Console.WriteLine("|{0} {1} {2} {3}|\n", m.M41, m.M42, m.M43, m.M44);
        }

        static void DisplayVector()
        {
            Console.WriteLine("{0}\t{1}\n", v, v2);
        }
    }
}
