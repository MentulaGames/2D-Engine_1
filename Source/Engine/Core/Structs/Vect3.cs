namespace Mentula.Engine.Core
{
    using System;
    using System.Diagnostics;
    using ExtendedMath;

    [DebuggerDisplay("{ToString()}")]
    [Serializable]
    public struct Vect3 : IEquatable<Vect3>, IEquatable<Vect2>, IEquatable<Point>
    {
        public float X;
        public float Y;
        public float Z;

        public float Area { get { return X * Z; } }
        public float Volume { get { return X * Y * Z; } }
        public float Length { get { return (float)Math.Sqrt(LengthSquared); } }
        public float LengthSquared { get { return (X * X) + (Y * Y) + (Z * Z); } }

        public static Vect3 Zero { get { return zero; } }
        public static Vect3 Right { get { return right; } }
        public static Vect3 Left { get { return left; } }
        public static Vect3 Up { get { return up; } }
        public static Vect3 Down { get { return down; } }
        public static Vect3 Back { get { return back; } }
        public static Vect3 Forward { get { return forward; } }
        public static Vect3 One { get { return one; } }
        public static Vect3 Negative { get { return negative; } }

        private static readonly Vect3 zero = new Vect3();
        private static readonly Vect3 right = new Vect3(1, 0, 0);
        private static readonly Vect3 left = new Vect3(-1, 0, 0);
        private static readonly Vect3 up = new Vect3(0, 1, 0);
        private static readonly Vect3 down = new Vect3(0, -1, 0);
        private static readonly Vect3 back = new Vect3(0, 0, 1);
        private static readonly Vect3 forward = new Vect3(0, 0, -1);
        private static readonly Vect3 one = new Vect3(1);
        private static readonly Vect3 negative = new Vect3(-1);

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

        public static Vect3 Abs(Vect3 obj)
        {
            float vX = Math.Abs(obj.X);
            float vY = Math.Abs(obj.Y);
            float vZ = Math.Abs(obj.Z);
            return new Vect3(vX, vY, vZ);
        }

        public static void Abs(ref Vect3 obj, out Vect3 result)
        {
            result.X = Math.Abs(obj.X);
            result.Y = Math.Abs(obj.Y);
            result.Z = Math.Abs(obj.Z);
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
            float pX = MathEx.Barycentric(vertex1.X, vertex2.X, vertex3.X, b2, b3);
            float pY = MathEx.Barycentric(vertex1.Y, vertex2.Y, vertex3.Y, b2, b3);
            float pZ = MathEx.Barycentric(vertex1.Z, vertex2.Z, vertex3.Z, b2, b3);

            return new Vect3(pX, pY, pZ);
        }

        public static void Barycentric(ref Vect3 vertex1, ref Vect3 vertex2, ref Vect3 vertex3, float b2, float b3, out Vect3 result)
        {
            result.X = MathEx.Barycentric(vertex1.X, vertex2.X, vertex3.X, b2, b3);
            result.Y = MathEx.Barycentric(vertex1.Y, vertex2.Y, vertex3.Y, b2, b3);
            result.Z = MathEx.Barycentric(vertex1.Z, vertex2.Z, vertex3.Z, b2, b3);
        }

        public static Vect3 Clamp(Vect3 min, Vect3 max, Vect3 value)
        {
            float vX = MathF.Clamp(min.X, max.X, value.X);
            float vY = MathF.Clamp(min.Y, max.Y, value.Y);
            float vZ = MathF.Clamp(min.Z, max.Z, value.Z);

            return new Vect3(vX, vY, vZ);
        }

        public static void Clamp(ref Vect3 min, ref Vect3 max, ref Vect3 value, out Vect3 result)
        {
            result.X = MathF.Clamp(min.X, max.X, value.X);
            result.Y = MathF.Clamp(min.Y, max.Y, value.Y);
            result.Z = MathF.Clamp(min.Z, max.Z, value.Z);
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
            return (float)Math.Sqrt(DistanceSquared(obj1, obj2));
        }

        public static void Distance(ref Vect3 obj1, ref Vect3 obj2, out float result)
        {
            result = (float)Math.Sqrt(DistanceSquared(obj1, obj2));
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
            if (obj is Vect3) return Equals((Vect3)obj);
            if (obj is Vect2) return Equals((Vect2)obj);
            if (obj is Point) return Equals((Point)obj);
            return false;
        }

        public bool Equals(Vect3 other)
        {
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        public bool Equals(Vect2 other)
        {
            return X == other.X && Y == other.Y;
        }

        public bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Utils.HASH_BASE;

                hash = hash * Utils.HASH_MULTIPLIER ^ X.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ Y.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ Z.GetHashCode();

                return hash;
            }
        }

        public static Vect3 Lerp(Vect3 min, Vect3 max, float amount)
        {
            if (amount < 0 || amount > 1) throw new ArgumentException("amount must be between 0 and 1.");
            float vX = MathF.Lerp(min.X, max.X, amount);
            float vY = MathF.Lerp(min.Y, max.Y, amount);
            float vZ = MathF.Lerp(min.Z, max.Z, amount);

            return new Vect3(vX, vY, vZ);
        }

        public static void Lerp(ref Vect3 min, ref Vect3 max, float amount, out Vect3 result)
        {
            if (amount < 0 || amount > 1) throw new ArgumentException("amount must be between 0 and 1.");

            result.X = MathF.Lerp(min.X, max.X, amount);
            result.Y = MathF.Lerp(min.Y, max.Y, amount);
            result.Z = MathF.Lerp(min.Z, max.Z, amount);
        }

        public static Vect3 Lerp(Vect3 min, Vect3 max, Vect3 amount)
        {
            if (amount.Volume < 0 || amount.Volume > 1) throw new ArgumentException("amount must be between 0 and 1.");
            float vX = MathF.Lerp(min.X, max.X, amount.X);
            float vY = MathF.Lerp(min.Y, max.Y, amount.Y);
            float vZ = MathF.Lerp(min.Z, max.Z, amount.Z);

            return new Vect3(vX, vY, vZ);
        }

        public static void Lerp(ref Vect3 min, ref Vect3 max, ref Vect3 amount, out Vect3 result)
        {
            result.X = MathF.Lerp(min.X, max.X, amount.X);
            result.Y = MathF.Lerp(min.Y, max.Y, amount.Y);
            result.Z = MathF.Lerp(min.Z, max.Z, amount.Z);
        }

        public static Vect3 InvLerp(Vect3 min, Vect3 max, float value)
        {
            float vX = MathF.InvLerp(min.X, max.X, value);
            float vY = MathF.InvLerp(min.Y, max.Y, value);
            float vZ = MathF.InvLerp(min.Z, max.Z, value);

            return new Vect3(vX, vY, vZ);
        }

        public static void InvLerp(ref Vect3 min, ref Vect3 max, float value, out Vect3 result)
        {
            result.X = MathF.InvLerp(min.X, max.X, value);
            result.Y = MathF.InvLerp(min.Y, max.Y, value);
            result.Z = MathF.InvLerp(min.Z, max.Z, value);
        }

        public static Vect3 InvLerp(Vect3 min, Vect3 max, Vect3 value)
        {
            float vX = MathF.InvLerp(min.X, max.X, value.X);
            float vY = MathF.InvLerp(min.Y, max.Y, value.Y);
            float vZ = MathF.InvLerp(min.Z, max.Z, value.Z);

            return new Vect3(vX, vY, vZ);
        }

        public static void InvLerp(ref Vect3 min, ref Vect3 max, ref Vect3 value, out Vect3 result)
        {
            result.X = MathF.InvLerp(min.X, max.X, value.X);
            result.Y = MathF.InvLerp(min.Y, max.Y, value.Y);
            result.Z = MathF.InvLerp(min.Z, max.Z, value.Z);
        }

        public static Vect3 Max(Vect3 obj1, Vect3 obj2)
        {
            Vect3 result = new Vect3();

            result.X = Math.Max(obj1.X, obj2.X);
            result.Y = Math.Max(obj1.Y, obj2.Y);
            result.Z = Math.Max(obj1.Z, obj2.Z);

            return result;
        }

        public static void Max(ref Vect3 obj1, ref Vect3 obj2, out Vect3 result)
        {
            result.X = Math.Max(obj1.X, obj2.X);
            result.Y = Math.Max(obj1.Y, obj2.Y);
            result.Z = Math.Max(obj1.Z, obj2.Z);
        }

        public static Vect3 Min(Vect3 obj1, Vect3 obj2)
        {
            Vect3 result = new Vect3();

            result.X = Math.Min(obj1.X, obj2.X);
            result.Y = Math.Min(obj1.Y, obj2.Y);
            result.Z = Math.Min(obj1.Z, obj2.Z);

            return result;
        }

        public static void Min(ref Vect3 obj1, ref Vect3 obj2, out Vect3 result)
        {
            result.X = Math.Min(obj1.X, obj2.X);
            result.Y = Math.Min(obj1.Y, obj2.Y);
            result.Z = Math.Min(obj1.Z, obj2.Z);
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
            return $"(X:{X}, Y:{Y}, Z:{Z})";
        }
    }
}