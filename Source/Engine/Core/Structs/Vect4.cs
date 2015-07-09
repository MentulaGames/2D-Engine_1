namespace Mentula.Engine.Core
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{ToString()}")]
    [Serializable]
    public struct Vect4 : IEquatable<Vect4>, IEquatable<Vect3>, IEquatable<Vect2>
    {
        public float W;
        public float X;
        public float Y;
        public float Z;

        public float Length { get { return (float)Math.Sqrt((W * W) + (X * X) + (Y * Y) + (Z * Z)); } }
        public float LengthSquared { get { return (W * W) + (X * X) + (Y * Y) + (Z * Z); } }

        public static Vect4 One { get { return new Vect4(1); } }
        public static Vect4 UnitW { get { return new Vect4(1, 0, 0, 0); } }
        public static Vect4 UnitX { get { return new Vect4(0, 1, 0, 0); } }
        public static Vect4 UnitY { get { return new Vect4(0, 0, 1, 0); } }
        public static Vect4 UnitZ { get { return new Vect4(0, 0, 0, 1); } }
        public static Vect4 Zero { get { return new Vect4(); } }

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
            float pW = ((1 - b2 - b3) * vertex1.W) + (b2 * vertex2.W) + (b3 * vertex3.W);
            float pX = ((1 - b2 - b3) * vertex1.X) + (b2 * vertex2.X) + (b3 * vertex3.X);
            float pY = ((1 - b2 - b3) * vertex1.Y) + (b2 * vertex2.Y) + (b3 * vertex3.Y);
            float pZ = ((1 - b2 - b3) * vertex1.Z) + (b2 * vertex2.Z) + (b3 * vertex3.Z);

            return new Vect4(pW, pX, pY, pZ);
        }

        public static void Barycentric(ref Vect4 vertex1, ref Vect4 vertex2, ref Vect4 vertex3, float b2, float b3, out Vect4 result)
        {
            result.W = ((1 - b2 - b3) * vertex1.W) + (b2 * vertex2.W) + (b3 * vertex3.W);
            result.X = ((1 - b2 - b3) * vertex1.X) + (b2 * vertex2.X) + (b3 * vertex3.X);
            result.Y = ((1 - b2 - b3) * vertex1.Y) + (b2 * vertex2.Y) + (b3 * vertex3.Y);
            result.Z = ((1 - b2 - b3) * vertex1.Z) + (b2 * vertex2.Z) + (b3 * vertex3.Z);
        }

        public static Vect4 Clamp(Vect4 min, Vect4 max, Vect4 value)
        {
            float vW = value.W < min.W ? min.W : (value.W > max.W ? max.W : value.W);
            float vX = value.X < min.X ? min.X : (value.X > max.X ? max.X : value.X);
            float vY = value.Y < min.Y ? min.Y : (value.Y > max.Y ? max.Y : value.Y);
            float vZ = value.Z < min.Z ? min.Z : (value.Z > max.Z ? max.Z : value.Z);

            return new Vect4(vW, vX, vY, vZ);
        }

        public static void Clamp(ref Vect4 min, ref Vect4 max, ref Vect4 value, out Vect4 result)
        {
            result.W = value.W < min.W ? min.W : (value.W > max.W ? max.W : value.W);
            result.X = value.X < min.X ? min.X : (value.X > max.X ? max.X : value.X);
            result.Y = value.Y < min.Y ? min.Y : (value.Y > max.Y ? max.Y : value.Y);
            result.Z = value.Z < min.Z ? min.Z : (value.Z > max.Z ? max.Z : value.Z);
        }

        public static float Distance(Vect4 obj1, Vect4 obj2)
        {
            float diffW = obj2.W - obj1.W;
            float diffX = obj2.X - obj1.X;
            float diffY = obj2.Y - obj1.Y;
            float diffZ = obj2.Z - obj1.Z;

            float distW = diffW * diffW;
            float distX = diffX * diffX;
            float distY = diffY * diffY;
            float distZ = diffZ * diffZ;

            return (float)Math.Sqrt(distW + distX + distY + distZ);
        }

        public static void Distance(ref Vect4 obj1, ref Vect4 obj2, out float result)
        {
            float diffW = obj2.W - obj1.W;
            float diffX = obj2.X - obj1.X;
            float diffY = obj2.Y - obj1.Y;
            float diffZ = obj2.Z - obj1.Z;

            float distW = diffW * diffW;
            float distX = diffX * diffX;
            float distY = diffY * diffY;
            float distZ = diffZ * diffZ;

            result = (float)Math.Sqrt(distW + distX + distY + distZ);
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

        public bool Equals(Vect4 other)
        {
            return W == other.W && X == other.X && Y == other.Y && Z == other.Z;
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

        public static Vect4 Lerp(Vect4 obj1, Vect4 obj2, float amount)
        {
            if (amount < 0 || amount > 1) throw new ArgumentException("amount must be between 0 and 1.");

            Vect4 adder = Subtract(obj2, obj1);
            return Multiply(Add(obj1, adder), amount);
        }

        public static void Lerp(ref Vect4 obj1, ref Vect4 obj2, float amount, out Vect4 result)
        {
            if (amount < 0 || amount > 1) throw new ArgumentException("amount must be between 0 and 1.");

            Vect4 adder;
            Subtract(ref obj2, ref obj1, out adder);

            result = Multiply(Add(obj1, adder), amount);
        }

        public static Vect4 Max(Vect4 obj1, Vect4 obj2)
        {
            Vect4 result = new Vect4();

            result.W = obj1.W > obj2.W ? obj1.W : obj2.W;
            result.X = obj1.X > obj2.X ? obj1.X : obj2.X;
            result.Y = obj1.Y > obj2.Y ? obj1.Y : obj2.Y;
            result.Z = obj1.Z > obj2.Z ? obj1.Z : obj2.Z;

            return result;
        }

        public static void Max(ref Vect4 obj1, ref Vect4 obj2, out Vect4 result)
        {
            result.W = obj1.W > obj2.W ? obj1.W : obj2.W;
            result.X = obj1.X > obj2.X ? obj1.X : obj2.X;
            result.Y = obj1.Y > obj2.Y ? obj1.Y : obj2.Y;
            result.Z = obj1.Z > obj2.Z ? obj1.Z : obj2.Z;
        }

        public static Vect4 Min(Vect4 obj1, Vect4 obj2)
        {
            Vect4 result = new Vect4();

            result.W = obj1.W < obj2.W ? obj1.W : obj2.W;
            result.X = obj1.X < obj2.X ? obj1.X : obj2.X;
            result.Y = obj1.Y < obj2.Y ? obj1.Y : obj2.Y;
            result.Z = obj1.Z < obj2.Z ? obj1.Z : obj2.Z;

            return result;
        }

        public static void Min(ref Vect4 obj1, ref Vect4 obj2, out Vect4 result)
        {
            result.W = obj1.W < obj2.W ? obj1.W : obj2.W;
            result.X = obj1.X < obj2.X ? obj1.X : obj2.X;
            result.Y = obj1.Y < obj2.Y ? obj1.Y : obj2.Y;
            result.Z = obj1.Z < obj2.Z ? obj1.Z : obj2.Z;
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
            return "(W:" + W + ", X:" + X + ", Y:" + Y + ", Z:" + Z + ")";
        }
    }
}