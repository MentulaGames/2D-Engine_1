namespace Mentula.Engine.Core
{
    using System;
    using System.Diagnostics;
    using ExtendedMath;

    [DebuggerDisplay("{ToString()}")]
    [Serializable]
    public struct Vect4 : IEquatable<Vect4>, IEquatable<Vect3>, IEquatable<Vect2>, IEquatable<Point>
    {
        public float W;
        public float X;
        public float Y;
        public float Z;

        public float Area { get { return X * Z; } }
        public float Volume { get { return X * Y * Z; } }
        public float Volume4D { get { return X * Y * Z * W; } }
        public float Length { get { return (float)Math.Sqrt(LengthSquared); } }
        public float LengthSquared { get { return (W * W) + (X * X) + (Y * Y) + (Z * Z); } }

        public static Vect4 One { get { return one; } }
        public static Vect4 UnitW { get { return unitW; } }
        public static Vect4 UnitX { get { return unitX; } }
        public static Vect4 UnitY { get { return unitY; } }
        public static Vect4 UnitZ { get { return unitZ; } }
        public static Vect4 Zero { get { return zero; } }
        public static Vect4 Negative { get { return negative; } }

        private static readonly Vect4 one = new Vect4(1);
        private static readonly Vect4 unitW = new Vect4(1, 0, 0, 0);
        private static readonly Vect4 unitX = new Vect4(0, 1, 0, 0);
        private static readonly Vect4 unitY = new Vect4(0, 0, 1, 0);
        private static readonly Vect4 unitZ = new Vect4(0, 0, 0, 1);
        private static readonly Vect4 zero = new Vect4();
        private static readonly Vect4 negative = new Vect4(-1);

        public static Vect4 operator -(Vect4 value) { return Negate(value); }
        public static Vect4 operator -(Vect4 value1, Vect4 value2) { return Subtract(value1, value2); }
        public static bool operator !=(Vect4 value1, Vect4 value2) { return !value1.Equals(value2); }
        public static Vect4 operator *(float scaleFactor, Vect4 value) { return Multiply(value, scaleFactor); }
        public static Vect4 operator *(Vect4 value, float scaleFactor) { return Multiply(value, scaleFactor); }
        public static Vect4 operator *(Vect4 value1, Vect4 value2) { return Multiply(value1, value2); }
        public static Vect4 operator /(Vect4 value1, float divider) { return Divide(value1, divider); }
        public static Vect4 operator /(Vect4 value1, Vect4 value2) { return Divide(value1, value2); }
        public static Vect4 operator +(Vect4 value1, Vect4 value2) { return Add(value1, value2); }
        public static bool operator ==(Vect4 value1, Vect4 value2) { return value1.Equals(value2); }

        public Vect4(float value)
        {
            W = value;
            X = value;
            Y = value;
            Z = value;
        }

        public Vect4(float w, float x, float y, float z)
        {
            W = w;
            X = x;
            Y = y;
            Z = z;
        }

        public Vect4(Vect4 value)
        {
            W = value.W;
            X = value.X;
            Y = value.Y;
            Z = value.Z;
        }

        public static Vect4 Abs(Vect4 obj)
        {
            float vW = Math.Abs(obj.W);
            float vX = Math.Abs(obj.X);
            float vY = Math.Abs(obj.Y);
            float vZ = Math.Abs(obj.Z);

            return new Vect4(vW, vX, vY, vZ);
        }

        public static void Abs(ref Vect4 obj, out Vect4 result)
        {
            result.W = Math.Abs(obj.W);
            result.X = Math.Abs(obj.X);
            result.Y = Math.Abs(obj.Y);
            result.Z = Math.Abs(obj.Z);
        }

        public static Vect4 Add(Vect4 obj1, Vect4 obj2)
        {
            Vect4 result = new Vect4();

            result.W = obj1.W + obj2.W;
            result.X = obj1.X + obj2.X;
            result.Y = obj1.Y + obj2.Y;
            result.Z = obj1.Z + obj2.Z;

            return result;
        }

        public static void Add(ref Vect4 obj1, ref Vect4 obj2, out Vect4 result)
        {
            result.W = obj1.W + obj2.W;
            result.X = obj1.X + obj2.X;
            result.Y = obj1.Y + obj2.Y;
            result.Z = obj1.Z + obj2.Z;
        }

        public static Vect4 Barycentric(Vect4 vertex1, Vect4 vertex2, Vect4 vertex3, float b2, float b3)
        {
            float pW = MathEx.Barycentric(vertex1.W, vertex2.W, vertex3.W, b2, b3);
            float pX = MathEx.Barycentric(vertex1.X, vertex2.X, vertex3.X, b2, b3);
            float pY = MathEx.Barycentric(vertex1.Y, vertex2.Y, vertex3.Y, b2, b3);
            float pZ = MathEx.Barycentric(vertex1.Z, vertex2.Z, vertex3.Z, b2, b3);

            return new Vect4(pW, pX, pY, pZ);
        }

        public static void Barycentric(ref Vect4 vertex1, ref Vect4 vertex2, ref Vect4 vertex3, float b2, float b3, out Vect4 result)
        {
            result.W = MathEx.Barycentric(vertex1.W, vertex2.W, vertex3.W, b2, b3);
            result.X = MathEx.Barycentric(vertex1.X, vertex2.X, vertex3.X, b2, b3);
            result.Y = MathEx.Barycentric(vertex1.Y, vertex2.Y, vertex3.Y, b2, b3);
            result.Z = MathEx.Barycentric(vertex1.Z, vertex2.Z, vertex3.Z, b2, b3);
        }

        public static Vect4 Clamp(Vect4 min, Vect4 max, Vect4 value)
        {
            float vW = MathF.Clamp(min.W, max.W, value.W);
            float vX = MathF.Clamp(min.X, max.X, value.X);
            float vY = MathF.Clamp(min.Y, max.Y, value.Y);
            float vZ = MathF.Clamp(min.Z, max.Z, value.Z);

            return new Vect4(vW, vX, vY, vZ);
        }

        public static void Clamp(ref Vect4 min, ref Vect4 max, ref Vect4 value, out Vect4 result)
        {
            result.W = MathF.Clamp(min.W, max.W, value.W);
            result.X = MathF.Clamp(min.X, max.X, value.X);
            result.Y = MathF.Clamp(min.Y, max.Y, value.Y);
            result.Z = MathF.Clamp(min.Z, max.Z, value.Z);
        }

        public static float Distance(Vect4 obj1, Vect4 obj2)
        {
            return (float)Math.Sqrt(DistanceSquared(obj1, obj2));
        }

        public static void Distance(ref Vect4 obj1, ref Vect4 obj2, out float result)
        {
            result = (float)Math.Sqrt(DistanceSquared(obj1, obj2));
        }

        public static float DistanceSquared(Vect4 obj1, Vect4 obj2)
        {
            float diffW = obj2.W - obj1.W;
            float diffX = obj2.X - obj1.X;
            float diffY = obj2.Y - obj1.Y;
            float diffZ = obj2.Z - obj1.Z;

            return (diffW * diffW) + (diffX * diffX) + (diffY * diffY) + (diffZ * diffZ);
        }

        public static void DistanceSquared(ref Vect4 obj1, ref Vect4 obj2, out float result)
        {
            float diffW = obj2.W - obj1.W;
            float diffX = obj2.X - obj1.X;
            float diffY = obj2.Y - obj1.Y;
            float diffZ = obj2.Z - obj1.Z;

            result = (diffW * diffW) + (diffX * diffX) + (diffY * diffY) + (diffZ * diffZ);
        }

        public static Vect4 Divide(Vect4 value, float Divider)
        {
            float vW = value.W / Divider;
            float vX = value.X / Divider;
            float vY = value.Y / Divider;
            float vZ = value.Z / Divider;

            return new Vect4(vW, vX, vY, vZ);
        }

        public static void Divide(ref Vect4 value, float Divider, out Vect4 result)
        {
            result.W = value.W / Divider;
            result.X = value.X / Divider;
            result.Y = value.Y / Divider;
            result.Z = value.Z / Divider;
        }

        public static Vect4 Divide(Vect4 value, Vect4 Divider)
        {
            float vW = value.W / Divider.W;
            float vX = value.X / Divider.X;
            float vY = value.Y / Divider.Y;
            float vZ = value.Z / Divider.Z;

            return new Vect4(vW, vX, vY, vZ);
        }

        public static void Divide(ref Vect4 value, ref Vect4 Divider, out Vect4 result)
        {
            result.W = value.W / Divider.W;
            result.X = value.X / Divider.X;
            result.Y = value.Y / Divider.Y;
            result.Z = value.Z / Divider.Z;
        }

        public static float Dot(Vect4 obj1, Vect4 obj2)
        {
            return (obj1.W + obj2.W) + (obj1.X * obj2.X) + (obj1.Y * obj2.Y) + (obj1.Z * obj2.Z);
        }

        public static void Dot(ref Vect4 obj1, ref Vect4 obj2, out float result)
        {
            result = (obj1.W + obj2.W) + (obj1.X * obj2.X) + (obj1.Y * obj2.Y) + (obj1.Z * obj2.Z);
        }

        public override bool Equals(object obj)
        {
            if (obj is Vect4) return Equals((Vect4)obj);
            if (obj is Vect3) return Equals((Vect3)obj);
            if (obj is Vect2) return Equals((Vect2)obj);
            if (obj is Point) return Equals((Point)obj);
            return false;
        }

        public bool Equals(Vect4 other)
        {
            return W == other.W && X == other.X && Y == other.Y && Z == other.Z;
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

                hash = hash * Utils.HASH_MULTIPLIER ^ W.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ X.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ Y.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ Z.GetHashCode();

                return hash;
            }
        }

        public static Vect4 Lerp(Vect4 min, Vect4 max, float amount)
        {
            if (amount < 0 || amount > 1) throw new ArgumentException("amount must be between 0 and 1.");
            float vW = MathF.Lerp(min.W, max.W, amount);
            float vX = MathF.Lerp(min.X, max.X, amount);
            float vY = MathF.Lerp(min.Y, max.Y, amount);
            float vZ = MathF.Lerp(min.Z, max.Z, amount);

            return new Vect4(vW, vX, vY, vZ);
        }

        public static void Lerp(ref Vect4 min, ref Vect4 max, float amount, out Vect4 result)
        {
            if (amount < 0 || amount > 1) throw new ArgumentException("amount must be between 0 and 1.");
            result.W = MathF.Lerp(min.W, max.W, amount);
            result.X = MathF.Lerp(min.X, max.X, amount);
            result.Y = MathF.Lerp(min.Y, max.Y, amount);
            result.Z = MathF.Lerp(min.Z, max.Z, amount);
        }

        public static Vect4 Lerp(Vect4 min, Vect4 max, Vect4 amount)
        {
            if (amount.Volume4D < 0 || amount.Volume4D > 1) throw new ArgumentException("amount must be between 0 and 1.");
            float vW = MathF.Lerp(min.W, max.W, amount.W);
            float vX = MathF.Lerp(min.X, max.X, amount.X);
            float vY = MathF.Lerp(min.Y, max.Y, amount.Y);
            float vZ = MathF.Lerp(min.Z, max.Z, amount.Z);

            return new Vect4(vW, vX, vY, vZ);
        }

        public static void Lerp(ref Vect4 min, ref Vect4 max, ref Vect4 amount, out Vect4 result)
        {
            if (amount.Volume4D < 0 || amount.Volume4D > 1) throw new ArgumentException("amount must be between 0 and 1.");
            result.W = MathF.Lerp(min.W, max.W, amount.W);
            result.X = MathF.Lerp(min.X, max.X, amount.X);
            result.Y = MathF.Lerp(min.Y, max.Y, amount.Y);
            result.Z = MathF.Lerp(min.Z, max.Z, amount.Z);
        }

        public static Vect4 InvLerp(Vect4 min, Vect4 max, float value)
        {
            float vW = MathF.InvLerp(min.W, max.W, value);
            float vX = MathF.InvLerp(min.X, max.X, value);
            float vY = MathF.InvLerp(min.Y, max.Y, value);
            float vZ = MathF.InvLerp(min.Z, max.Z, value);

            return new Vect4(vW, vX, vY, vZ);
        }

        public static void InvLerp(ref Vect4 min, ref Vect4 max, float value, out Vect4 result)
        {
            result.W = MathF.InvLerp(min.W, max.W, value);
            result.X = MathF.InvLerp(min.X, max.X, value);
            result.Y = MathF.InvLerp(min.Y, max.Y, value);
            result.Z = MathF.InvLerp(min.Z, max.Z, value);
        }

        public static Vect4 InvLerp(Vect4 min, Vect4 max, Vect4 value)
        {
            float vW = MathF.InvLerp(min.W, max.W, value.W);
            float vX = MathF.InvLerp(min.X, max.X, value.X);
            float vY = MathF.InvLerp(min.Y, max.Y, value.Y);
            float vZ = MathF.InvLerp(min.Z, max.Z, value.Z);

            return new Vect4(vW, vX, vY, vZ);
        }

        public static void InvLerp(ref Vect4 min, ref Vect4 max, ref Vect4 value, out Vect4 result)
        {
            result.W = MathF.InvLerp(min.W, max.W, value.W);
            result.X = MathF.InvLerp(min.X, max.X, value.X);
            result.Y = MathF.InvLerp(min.Y, max.Y, value.Y);
            result.Z = MathF.InvLerp(min.Z, max.Z, value.Z);
        }

        public static Vect4 Max(Vect4 obj1, Vect4 obj2)
        {
            Vect4 result = new Vect4();

            result.W = Math.Max(obj1.W, obj2.W);
            result.X = Math.Max(obj1.X, obj2.X);
            result.Y = Math.Max(obj1.Y, obj2.Y);
            result.Z = Math.Max(obj1.Z, obj2.Z);

            return result;
        }

        public static void Max(ref Vect4 obj1, ref Vect4 obj2, out Vect4 result)
        {
            result.W = Math.Max(obj1.W, obj2.W);
            result.X = Math.Max(obj1.X, obj2.X);
            result.Y = Math.Max(obj1.Y, obj2.Y);
            result.Z = Math.Max(obj1.Z, obj2.Z);
        }

        public static Vect4 Min(Vect4 obj1, Vect4 obj2)
        {
            Vect4 result = new Vect4();

            result.W = Math.Min(obj1.W, obj2.W);
            result.X = Math.Min(obj1.X, obj2.X);
            result.Y = Math.Min(obj1.Y, obj2.Y);
            result.Z = Math.Min(obj1.Z, obj2.Z);

            return result;
        }

        public static void Min(ref Vect4 obj1, ref Vect4 obj2, out Vect4 result)
        {
            result.W = Math.Min(obj1.W, obj2.W);
            result.X = Math.Min(obj1.X, obj2.X);
            result.Y = Math.Min(obj1.Y, obj2.Y);
            result.Z = Math.Min(obj1.Z, obj2.Z);
        }

        public static Vect4 Multiply(Vect4 value, float multiplier)
        {
            Vect4 result = new Vect4();

            result.W = value.W * multiplier;
            result.X = value.X * multiplier;
            result.Y = value.Y * multiplier;
            result.Z = value.Z * multiplier;

            return result;
        }

        public static void Multiply(ref Vect4 value, float multiplier, out Vect4 result)
        {
            result.W = value.W * multiplier;
            result.X = value.X * multiplier;
            result.Y = value.Y * multiplier;
            result.Z = value.Z * multiplier;
        }

        public static Vect4 Multiply(Vect4 value, Vect4 multiplier)
        {
            Vect4 result = new Vect4();

            result.W = value.W * multiplier.W;
            result.X = value.X * multiplier.X;
            result.Y = value.Y * multiplier.Y;
            result.Z = value.Z * multiplier.Z;

            return result;
        }

        public static void Multiply(ref Vect4 value, ref Vect4 multiplier, out Vect4 result)
        {
            result.W = value.W * multiplier.W;
            result.X = value.X * multiplier.X;
            result.Y = value.Y * multiplier.Y;
            result.Z = value.Z * multiplier.Z;
        }

        public static Vect4 Negate(Vect4 value)
        {
            Vect4 result = new Vect4();

            result.W = -value.W;
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;

            return result;
        }

        public static void Negate(ref Vect4 value, out Vect4 result)
        {
            result.W = -value.W;
            result.X = -value.X;
            result.Y = -value.Y;
            result.Z = -value.Z;
        }

        public Vect4 Normalize()
        {
            Vect4 result = new Vect4();
            float length = Length;

            result.W = W / length;
            result.X = X / length;
            result.Y = Y / length;
            result.Z = Z / length;

            return result;
        }

        public static Vect4 Normalize(Vect4 value)
        {
            Vect4 result = new Vect4();
            float length = value.Length;

            result.W = value.W / length;
            result.X = value.X / length;
            result.Y = value.Y / length;
            result.Z = value.Z / length;

            return result;
        }

        public static void Normalize(ref Vect4 value, out Vect4 result)
        {
            float length = value.Length;

            result.W = value.W / length;
            result.X = value.X / length;
            result.Y = value.Y / length;
            result.Z = value.Z / length;
        }

        public static Vect4 Reflect(Vect4 value, Vect4 normal)
        {
            float dot = Dot(value, normal);
            Vect4 diff = Subtract(normal, value);

            return Multiply(diff, 2 * dot);
        }

        public static void Reflect(ref Vect4 value, ref Vect4 normal, out Vect4 result)
        {
            float dot = Dot(value, normal);

            Vect4 diff;
            Subtract(ref normal, ref value, out diff);

            result = Multiply(diff, 2 * dot);
        }

        public static Vect4 Subtract(Vect4 obj1, Vect4 obj2)
        {
            Vect4 result = new Vect4();

            result.W = obj1.W - obj2.W;
            result.X = obj1.X - obj2.X;
            result.Y = obj1.Y - obj2.Y;
            result.Z = obj1.Z - obj2.Z;

            return result;
        }

        public static void Subtract(ref Vect4 obj1, ref Vect4 obj2, out Vect4 result)
        {
            result.W = obj1.W - obj2.W;
            result.X = obj1.X - obj2.X;
            result.Y = obj1.Y - obj2.Y;
            result.Z = obj1.Z - obj2.Z;
        }

        public override string ToString()
        {
            return $"(W:{W}, X:{X}, Y:{Y}, Z:{Z})";
        }
    }
}