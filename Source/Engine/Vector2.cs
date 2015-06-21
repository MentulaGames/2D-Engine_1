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

        public float Area { get { return X * Y; } }
        public float Length { get { return (float)Math.Sqrt((X * X) + (Y * Y)); } }

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
            result.X = obj1.X + obj2.X;
            result.Y = obj2.Y + obj2.Y;
        }

        public static float Angle(Vector2 obj1, Vector2 obj2)
        {
            return (float)Math.Atan2(obj2.Y - obj1.Y, obj2.X - obj1.X);
        }

        public static void Angle(ref Vector2 obj1, ref Vector2 obj2, out float result)
        {
            result = (float)Math.Atan2(obj2.Y - obj1.Y, obj2.X - obj1.X);
        }

        public static Vector2 Barycentric(Vector2 vertex1, Vector2 vertex2, Vector2 vertex3, float b2, float b3)
        {
            float pX = ((1 - b2 - b3) * vertex1.X) + (b2 * vertex2.X) + (b3 * vertex3.X);
            float pY = ((1 - b2 - b3) * vertex1.Y) + (b2 * vertex2.Y) + (b3 * vertex3.Y);

            return new Vector2(pX, pY);
        }

        public static void Barycentric(ref Vector2 vertex1, ref Vector2 vertex2, ref Vector2 vertex3, float b2, float b3, out Vector2 result)
        {
            result.X = ((1 - b2 - b3) * vertex1.X) + (b2 * vertex2.X) + (b3 * vertex3.X);
            result.Y = ((1 - b2 - b3) * vertex1.Y) + (b2 * vertex2.Y) + (b3 * vertex3.Y);
        }

        public static Vector2 Clamp(Vector2 min, Vector2 max, Vector2 value)
        {
            float vX = value.X < min.X ? min.X : (value.X > max.X ? max.X : value.X);
            float vY = value.Y < min.Y ? min.Y : (value.Y > max.Y ? max.Y : value.Y);

            return new Vector2(vX, vY);
        }

        public static void Clamp(ref Vector2 min, ref Vector2 max, ref Vector2 value, out Vector2 result)
        {
            result.X = value.X < min.X ? min.X : (value.X > max.X ? max.X : value.X);
            result.Y = value.Y < min.Y ? min.Y : (value.Y > max.Y ? max.Y : value.Y);
        }

        public static float Distance(Vector2 obj1, Vector2 obj2)
        {
            float diffX = obj2.X - obj1.X;
            float diffY = obj2.Y - obj1.Y;

            float distX = diffX * diffX;
            float distY = diffY * diffY;

            return (float)Math.Sqrt(distX + distY);
        }

        public static void Distance(ref Vector2 obj1, ref Vector2 obj2, out float result)
        {
            float diffX = obj2.X - obj1.X;
            float diffY = obj2.Y - obj1.Y;

            float distX = diffX * diffX;
            float distY = diffY * diffY;

            result = (float)Math.Sqrt(distX + distY);
        }

        public static float DistanceSquared(Vector2 obj1, Vector2 obj2)
        {
            float diffX = obj2.X - obj1.X;
            float diffY = obj2.Y - obj1.Y;

            return (diffX * diffX) + (diffY * diffY);
        }

        public static void DistanceSquared(ref Vector2 obj1, ref Vector2 obj2, out float result)
        {
            float diffX = obj2.X - obj1.X;
            float diffY = obj2.Y - obj1.Y;

            result = (diffX * diffX) + (diffY * diffY);
        }

        public static Vector2 Devide(Vector2 value, float devider)
        {
            float vX = value.X / devider;
            float vY = value.Y / devider;

            return new Vector2(vX, vY);
        }

        public static void Devide(ref Vector2 value, float devider, out Vector2 result)
        {
            result.X = value.X / devider;
            result.Y = value.Y / devider;
        }

        public static Vector2 Devide(Vector2 value, Vector2 devider)
        {
            float vX = value.X / devider.X;
            float vY = value.Y / devider.Y;

            return new Vector2(vX, vY);
        }

        public static void Devide(ref Vector2 value, ref Vector2 devider, out Vector2 result)
        {
            result.X = value.X / devider.X;
            result.Y = value.Y / devider.Y;
        }
    }
}
