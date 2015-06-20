using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Core
{
    public struct Vector2
    {
        public float X;
        public float Y;

        public static readonly Vector2 Zero;
        public static readonly Vector2 UnitX;
        public static readonly Vector2 UnitY;
        public static readonly Vector2 One;

        public Vector2(float value)
        {
            X = value;
            Y = value;
        }

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        static Vector2()
        {
            Zero = new Vector2();
            UnitX = new Vector2(1, 0);
            UnitY = new Vector2(0, 1);
            One = new Vector2(1);
        }

        public static Vector2 Add(Vector2 obj1, Vector2 obj2)
        {
            Vector2 result = new Vector2();

            result.X = obj1.X + obj2.X;
            result.Y = obj2.Y + obj2.Y;

            return result;
        }

        public static void Add(ref Vector2 obj1, ref Vector2 obj2, out Vector2 result)
        {
            result = new Vector2();

            result.X = obj1.X + obj2.X;
            result.Y = obj2.Y + obj2.Y;
        }

        public static Vector2 Barycentric(Vector2 obj1, Vector2 obj2, Vector2 obj3, float amount1, float amount2)
        {

        }
    }
}
