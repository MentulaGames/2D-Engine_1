namespace Mentula.Engine.Core
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{ToString()}")]
    [Serializable]
    public struct Vect3 : IEquatable<Vect3>, IEquatable<Vect2>
    {
        public float X;
        public float Y;
        public float Z;

        public float Length { get { return (float)Math.Sqrt((X * X) + (Y * Y) + (Z * Z)); } }
        public float LengthSquared { get { return (X * X) + (Y * Y) + (Z * Z); } }

        public static readonly Vect3 Negative;
        public static readonly Vect3 Zero;
        public static readonly Vect3 Right;
        public static readonly Vect3 Left;
        public static readonly Vect3 Up;
        public static readonly Vect3 Down;
        public static readonly Vect3 Back;
        public static readonly Vect3 Forward;
        public static readonly Vect3 One;

        public static Vect3 operator -(Vect3 value) { return Negate(value); }
        public static Vect3 operator -(Vect3 value1, Vect3 value2) { return Subtract(value1, value2); }
        public static bool operator !=(Vect3 value1, Vect3 value2) { return !value1.Equals(value2); }
        public static Vect3 operator *(float scaleFactor, Vect3 value) { return Multiply(value, scaleFactor); }
        public static Vect3 operator *(Vect3 value, float scaleFactor) { return Multiply(value, scaleFactor); }
        public static Vect3 operator *(Vect3 value1, Vect3 value2) { return Multiply(value1, value2); }
        public static Vect3 operator /(Vect3 value1, float divider) { return Divide(value1, divider); }
        public static Vect3 operator /(Vect3 value1, Vect3 value2) { return Divide(value1, value2); }
        public static Vect3 operator +(Vect3 value1, Vect3 value2) { return Add(value1, value2); }
        public static bool operator ==(Vect3 value1, Vect3 value2) { return value1.Equals(value2); }

        public Vect3(float value)
        {
            X = value;
            Y = value;
            Z = value;
        }

        public Vect3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public Vect3(Vect3 value)
        {
            X = value.X;
            Y = value.Y;
            Z = value.Z;
        }

        static Vect3()
        {
            Negative = new Vect3(-1);
            Zero = new Vect3();
            Right = new Vect3(1, 0, 0);
            Left = new Vect3(-1, 0, 0);
            Up = new Vect3(0, 1, 0);
            Down = new Vect3(0, -1, 0);
            Back = new Vect3(0, 0, 1);
            Forward = new Vect3(0, 0, -1);
            One = new Vect3(1);
        }

        public static Vect3 Add(Vect3 obj1, Vect3 obj2)
        {
            Vect3 result = new Vect3();

            result.X = obj1.X + obj2.X;
            result.Y = obj1.Y + obj2.Y;
            result.Z = obj1.Z + obj2.Z;

            return result;
        }

        public static void Add(ref Vect3 obj1, ref Vect3 obj2, out Vect3 result)
        {
            result.X = obj1.X + obj2.X;
            result.Y = obj1.Y + obj2.Y;
            result.Z = obj1.Z + obj2.Z;
        }

        public static Vect3 Barycentric(Vect3 vertex1, Vect3 vertex2, Vect3 vertex3, float b2, float b3)
        {
            float pX = ((1 - b2 - b3) * vertex1.X) + (b2 * vertex2.X) + (b3 * vertex3.X);
            float pY = ((1 - b2 - b3) * vertex1.Y) + (b2 * vertex2.Y) + (b3 * vertex3.Y);
            float pZ = ((1 - b2 - b3) * vertex1.Z) + (b2 * vertex2.Z) + (b3 * vertex3.Z);

            return new Vect3(pX, pY, pZ);
        }

        public static void Barycentric(ref Vect3 vertex1, ref Vect3 vertex2, ref Vect3 vertex3, float b2, float b3, out Vect3 result)
        {
            result.X = ((1 - b2 - b3) * vertex1.X) + (b2 * vertex2.X) + (b3 * vertex3.X);
            result.Y = ((1 - b2 - b3) * vertex1.Y) + (b2 * vertex2.Y) + (b3 * vertex3.Y);
            result.Z = ((1 - b2 - b3) * vertex1.Z) + (b2 * vertex2.Z) + (b3 * vertex3.Z);
        }

        public static Vect3 Clamp(Vect3 min, Vect3 max, Vect3 value)
        {
            float vX = value.X < min.X ? min.X : (value.X > max.X ? max.X : value.X);
            float vY = value.Y < min.Y ? min.Y : (value.Y > max.Y ? max.Y : value.Y);
            float vZ = value.Z < min.Z ? min.Z : (value.Z > max.Z ? max.Z : value.Z);

            return new Vect3(vX, vY, vZ);
        }

        public static void Clamp(ref Vect3 min, ref Vect3 max, ref Vect3 value, out Vect3 result)
        {
            result.X = value.X < min.X ? min.X : (value.X > max.X ? max.X : value.X);
            result.Y = value.Y < min.Y ? min.Y : (value.Y > max.Y ? max.Y : value.Y);
            result.Z = value.Z < min.Z ? min.Z : (value.Z > max.Z ? max.Z : value.Z);
        }

        public static Vect3 Cross(Vect3 obj1, Vect3 obj2)
        {
            float cX = (obj1.Y * obj2.Z) - (obj1.Z * obj2.Y);
            float cY = (obj1.Z * obj2.X) - (obj1.X * obj2.Z);
            float cZ = (obj1.X * obj2.Y) - (obj1.Y * obj2.X);

            return new Vect3(cX, cY, cZ);
        }

        public static void Cross(ref Vect3 obj1, ref Vect3 obj2, out Vect3 result)
        {
            result.X = (obj1.Y * obj2.Z) - (obj1.Z * obj2.Y);
            result.Y = (obj1.Z * obj2.X) - (obj1.X * obj2.Z);
            result.Z = (obj1.X * obj2.Y) - (obj1.Y * obj2.X);
        }

        public static float Distance(Vect3 obj1, Vect3 obj2)
        {
            float diffX = obj2.X - obj1.X;
            float diffY = obj2.Y - obj1.Y;
            float diffZ = obj2.Z - obj1.Z;

            float distX = diffX * diffX;
            float distY = diffY * diffY;
            float distZ = diffZ * diffZ;

            return (float)Math.Sqrt(distX + distY + distZ);
        }

        public static void Distance(ref Vect3 obj1, ref Vect3 obj2, out float result)
        {
            float diffX = obj2.X - obj1.X;
            float diffY = obj2.Y - obj1.Y;
            float diffZ = obj2.Z - obj1.Z;

            float distX = diffX * diffX;
            float distY = diffY * diffY;
            float distZ = diffZ * diffZ;

            result = (float)Math.Sqrt(distX + distY + distZ);
        }

        public static float DistanceSquared(Vect3 obj1, Vect3 obj2)
        {
            float diffX = obj2.X - obj1.X;
            float diffY = obj2.Y - obj1.Y;
            float diffZ = obj2.Z - obj1.Z;

            return (diffX * diffX) + (diffY * diffY) + (diffZ * diffZ);
        }

        public static void DistanceSquared(ref Vect3 obj1, ref Vect3 obj2, out float result)
        {
            float diffX = obj2.X - obj1.X;
            float diffY = obj2.Y - obj1.Y;
            float diffZ = obj2.Z - obj1.Z;

            result = (diffX * diffX) + (diffY * diffY) + (diffZ * diffZ);
        }

        public static Vect3 Divide(Vect3 value, float Divider)
        {
            float vX = value.X / Divider;
            float vY = value.Y / Divider;
            float vZ = value.Z / Divider;

            return new Vect3(vX, vY, vZ);
        }

        public static void Divide(ref Vect3 value, float Divider, out Vect3 result)
        {
            result.X = value.X / Divider;
            result.Y = value.Y / Divider;
            result.Z = value.Z / Divider;
        }

        public static Vect3 Divide(Vect3 value, Vect3 Divider)
        {
            float vX = value.X / Divider.X;
            float vY = value.Y / Divider.Y;
            float vZ = value.Z / Divider.Z;

            return new Vect3(vX, vY, vZ);
        }

        public static void Divide(ref Vect3 value, ref Vect3 Divider, out Vect3 result)
        {
            result.X = value.X / Divider.X;
            result.Y = value.Y / Divider.Y;
            result.Z = value.Z / Divider.Z;
        }

        public static float Dot(Vect3 obj1, Vect3 obj2)
        {
            return (obj1.X * obj2.X) + (obj1.Y * obj2.Y) + (obj1.Z * obj2.Z);
        }

        public static void Dot(ref Vect3 obj1, ref Vect3 obj2, out float result)
        {
            result = (obj1.X * obj2.X) + (obj1.Y * obj2.Y) + (obj1.Z * obj2.Z);
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }

        public bool Equals(Vect2 other)
        {
            return X == other.X && Y == other.Y;
        }

        public bool Equals(Vect3 other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Utils.HASH_BASE;

                hash *= Utils.HASH_MULTIPLIER ^ X.GetHashCode();
                hash *= Utils.HASH_MULTIPLIER ^ Y.GetHashCode();
                hash *= Utils.HASH_MULTIPLIER ^ Z.GetHashCode();

                return hash;
            }
        }

        public static Vect3 Lerp(Vect3 obj1, Vect3 obj2, float amount)
        {
            Vect3 adder = Subtract(obj2, obj1);
            return Multiply(Add(obj1, adder), amount);
        }

        public static void Lerp(ref Vect3 obj1, ref Vect3 obj2, float amount, out Vect3 result)
        {
            Vect3 adder;
            Subtract(ref obj2, ref obj1, out adder);

            result = Multiply(Add(obj1, adder), amount);
        }

        public static Vect3 Max(Vect3 obj1, Vect3 obj2)
        {
            Vect3 result = new Vect3();

            result.X = obj1.X > obj2.X ? obj1.X : obj2.X;
            result.Y = obj1.Y > obj2.Y ? obj1.Y : obj2.Y;
            result.Z = obj1.Z > obj2.Z ? obj1.Z : obj2.Z;

            return result;
        }

        public static void Max(ref Vect3 obj1, ref Vect3 obj2, out Vect3 result)
        {
            result.X = obj1.X > obj2.X ? obj1.X : obj2.X;
            result.Y = obj1.Y > obj2.Y ? obj1.Y : obj2.Y;
            result.Z = obj1.Z > obj2.Z ? obj1.Z : obj2.Z;
        }

        public static Vect3 Min(Vect3 obj1, Vect3 obj2)
        {
            Vect3 result = new Vect3();

            result.X = obj1.X < obj2.X ? obj1.X : obj2.X;
            result.Y = obj1.Y < obj2.Y ? obj1.Y : obj2.Y;
            result.Z = obj1.Z < obj2.Z ? obj1.Z : obj2.Z;

            return result;
        }

        public static void Min(ref Vect3 obj1, ref Vect3 obj2, out Vect3 result)
        {
            result.X = obj1.X < obj2.X ? obj1.X : obj2.X;
            result.Y = obj1.Y < obj2.Y ? obj1.Y : obj2.Y;
            result.Z = obj1.Z < obj2.Z ? obj1.Z : obj2.Z;
        }

        public static Vect3 Multiply(Vect3 value, float multiplier)
        {
            Vect3 result = new Vect3();

            result.X = value.X * multiplier;
            result.Y = value.Y * multiplier;
            result.Z = value.Z * multiplier;

            return result;
        }

        public static void Multiply(ref Vect3 value, float multiplier, out Vect3 result)
        {
            result.X = value.X * multiplier;
            result.Y = value.Y * multiplier;
            result.Z = value.Z * multiplier;
        }

        public static Vect3 Multiply(Vect3 value, Vect3 multiplier)
        {
            Vect3 result = new Vect3();

            result.X = value.X * multiplier.X;
            result.Y = value.Y * multiplier.Y;
            result.Z = value.Z * multiplier.Z;

            return result;
        }

        public static void Multiply(ref Vect3 value, ref Vect3 multiplier, out Vect3 result)
        {
            result.X = value.X * multiplier.X;
            result.Y = value.Y * multiplier.Y;
            result.Z = value.Z * multiplier.Z;
        }

        public static Vect3 Negate(Vect3 value)
        {
            Vect3 result = new Vect3();

            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;

            return result;
        }

        public static void Negate(ref Vect3 value, out Vect3 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
        }

        public Vect3 Normalize()
        {
            Vect3 result = new Vect3();
            float length = Length;

            result.X = X / length;
            result.Y = Y / length;
            result.Z = Z / length;

            return result;
        }

        public static Vect3 Normalize(Vect3 value)
        {
            Vect3 result = new Vect3();
            float length = value.Length;

            result.X = value.X / length;
            result.Y = value.Y / length;
            result.Z = value.Z / length;

            return result;
        }

        public static void Normalize(ref Vect3 value, out Vect3 result)
        {
            float length = value.Length;

            result.X = value.X / length;
            result.Y = value.Y / length;
            result.Z = value.Z / length;
        }

        public static Vect3 Reflect(Vect3 value, Vect3 normal)
        {
            float dot = Dot(value, normal);
            Vect3 diff = Subtract(normal, value);

            return Multiply(diff, 2 * dot);
        }

        public static void Reflect(ref Vect3 value, ref Vect3 normal, out Vect3 result)
        {
            float dot = Dot(value, normal);

            Vect3 diff;
            Subtract(ref normal, ref value, out diff);

            result = Multiply(diff, 2 * dot);
        }

        public static Vect3 Subtract(Vect3 obj1, Vect3 obj2)
        {
            Vect3 result = new Vect3();

            result.X = obj1.X - obj2.X;
            result.Y = obj1.Y - obj2.Y;
            result.Z = obj1.Z - obj2.Z;

            return result;
        }

        public static void Subtract(ref Vect3 obj1, ref Vect3 obj2, out Vect3 result)
        {
            result.X = obj1.X - obj2.X;
            result.Y = obj1.Y - obj2.Y;
            result.Z = obj1.Z - obj2.Z;
        }

        public override string ToString()
        {
            return "(X:" + X + ", Y:" + Y + ", Z:" + Z + ")";
        }
    }
}