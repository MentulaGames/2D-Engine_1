namespace Mentula.Engine.Core
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{ToString()}")]
    [Serializable]
    public struct Vect2 : IEquatable<Vect2>
    {
        public float X;
        public float Y;

        public float Area { get { return X * Y; } }
        public float Length { get { return (float)Math.Sqrt((X * X) + (Y * Y)); } }
        public float LengthSquared { get { return (X * X) + (Y * Y); } }

        public static readonly Vect2 Zero;
        public static readonly Vect2 UnitX;
        public static readonly Vect2 UnitY;
        public static readonly Vect2 One;

        public static Vect2 operator -(Vect2 value) { return Negate(value); }
        public static Vect2 operator -(Vect2 value1, Vect2 value2) { return Subtract(value1, value2); }
        public static bool operator !=(Vect2 value1, Vect2 value2) { return !value1.Equals(value2); }
        public static Vect2 operator *(float scaleFactor, Vect2 value) { return Multiply(value, scaleFactor); }
        public static Vect2 operator *(Vect2 value, float scaleFactor) { return Multiply(value, scaleFactor); }
        public static Vect2 operator *(Vect2 value1, Vect2 value2) { return Multiply(value1, value2); }
        public static Vect2 operator /(Vect2 value1, float divider) { return Divide(value1, divider); }
        public static Vect2 operator /(Vect2 value1, Vect2 value2) { return Divide(value1, value2); }
        public static Vect2 operator +(Vect2 value1, Vect2 value2) { return Add(value1, value2); }
        public static bool operator ==(Vect2 value1, Vect2 value2) { return value1.Equals(value2); }

        public Vect2(float value)
        {
            X = value;
            Y = value;
        }

        public Vect2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public Vect2(Vect2 value)
        {
            X = value.X;
            Y = value.Y;
        }

        static Vect2()
        {
            Zero = new Vect2();
            UnitX = new Vect2(1, 0);
            UnitY = new Vect2(0, 1);
            One = new Vect2(1);
        }

        public static Vect2 Add(Vect2 obj1, Vect2 obj2)
        {
            Vect2 result = new Vect2();

            float vX = obj1.X + obj2.X;
            float vY = obj2.Y + obj2.Y;

            return result;
        }

        public static void Add(ref Vect2 obj1, ref Vect2 obj2, out Vect2 result)
        {
            result.X = obj1.X + obj2.X;
            result.Y = obj2.Y + obj2.Y;
        }

        public static float Angle(Vect2 obj1, Vect2 obj2)
        {
            return (float)Math.Atan2(obj2.Y - obj1.Y, obj2.X - obj1.X);
        }

        public static void Angle(ref Vect2 obj1, ref Vect2 obj2, out float result)
        {
            result = (float)Math.Atan2(obj2.Y - obj1.Y, obj2.X - obj1.X);
        }

        public static Vect2 Barycentric(Vect2 vertex1, Vect2 vertex2, Vect2 vertex3, float b2, float b3)
        {
            float pX = ((1 - b2 - b3) * vertex1.X) + (b2 * vertex2.X) + (b3 * vertex3.X);
            float pY = ((1 - b2 - b3) * vertex1.Y) + (b2 * vertex2.Y) + (b3 * vertex3.Y);

            return new Vect2(pX, pY);
        }

        public static void Barycentric(ref Vect2 vertex1, ref Vect2 vertex2, ref Vect2 vertex3, float b2, float b3, out Vect2 result)
        {
            result.X = ((1 - b2 - b3) * vertex1.X) + (b2 * vertex2.X) + (b3 * vertex3.X);
            result.Y = ((1 - b2 - b3) * vertex1.Y) + (b2 * vertex2.Y) + (b3 * vertex3.Y);
        }

        public static Vect2 Clamp(Vect2 min, Vect2 max, Vect2 value)
        {
            float vX = value.X < min.X ? min.X : (value.X > max.X ? max.X : value.X);
            float vY = value.Y < min.Y ? min.Y : (value.Y > max.Y ? max.Y : value.Y);

            return new Vect2(vX, vY);
        }

        public static void Clamp(ref Vect2 min, ref Vect2 max, ref Vect2 value, out Vect2 result)
        {
            result.X = value.X < min.X ? min.X : (value.X > max.X ? max.X : value.X);
            result.Y = value.Y < min.Y ? min.Y : (value.Y > max.Y ? max.Y : value.Y);
        }

        public static float Distance(Vect2 obj1, Vect2 obj2)
        {
            float diffX = obj2.X - obj1.X;
            float diffY = obj2.Y - obj1.Y;

            float distX = diffX * diffX;
            float distY = diffY * diffY;

            return (float)Math.Sqrt(distX + distY);
        }

        public static void Distance(ref Vect2 obj1, ref Vect2 obj2, out float result)
        {
            float diffX = obj2.X - obj1.X;
            float diffY = obj2.Y - obj1.Y;

            float distX = diffX * diffX;
            float distY = diffY * diffY;

            result = (float)Math.Sqrt(distX + distY);
        }

        public static float DistanceSquared(Vect2 obj1, Vect2 obj2)
        {
            float diffX = obj2.X - obj1.X;
            float diffY = obj2.Y - obj1.Y;

            return (diffX * diffX) + (diffY * diffY);
        }

        public static void DistanceSquared(ref Vect2 obj1, ref Vect2 obj2, out float result)
        {
            float diffX = obj2.X - obj1.X;
            float diffY = obj2.Y - obj1.Y;

            result = (diffX * diffX) + (diffY * diffY);
        }

        public static Vect2 Divide(Vect2 value, float Divider)
        {
            float vX = value.X / Divider;
            float vY = value.Y / Divider;

            return new Vect2(vX, vY);
        }

        public static void Divide(ref Vect2 value, float Divider, out Vect2 result)
        {
            result.X = value.X / Divider;
            result.Y = value.Y / Divider;
        }

        public static Vect2 Divide(Vect2 value, Vect2 Divider)
        {
            float vX = value.X / Divider.X;
            float vY = value.Y / Divider.Y;

            return new Vect2(vX, vY);
        }

        public static void Divide(ref Vect2 value, ref Vect2 Divider, out Vect2 result)
        {
            result.X = value.X / Divider.X;
            result.Y = value.Y / Divider.Y;
        }

        public static float Dot(Vect2 obj1, Vect2 obj2)
        {
            return (obj1.X * obj2.X) + (obj1.Y * obj2.Y);
        }

        public static void Dot(ref Vect2 obj1, ref Vect2 obj2, out float result)
        {
            result = (obj1.X * obj2.X) + (obj1.Y * obj2.Y);
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }

        public bool Equals(Vect2 other)
        {
            return X == other.X && Y == other.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Utils.HASH_BASE;

                hash *= Utils.HASH_MULTIPLIER ^ X.GetHashCode();
                hash *= Utils.HASH_MULTIPLIER ^ Y.GetHashCode();

                return hash;
            }
        }

        public static Vect2 Lerp(Vect2 obj1, Vect2 obj2, float amount)
        {
            Vect2 adder = Subtract(obj2, obj1);
            return Multiply(Add(obj1, adder), amount);
        }

        public static void Lerp(ref Vect2 obj1, ref Vect2 obj2, float amount, out Vect2 result)
        {
            Vect2 adder;
            Subtract(ref obj2, ref obj1, out adder);

            result = Multiply(Add(obj1, adder), amount);
        }

        public static Vect2 Max(Vect2 obj1, Vect2 obj2)
        {
            Vect2 result = new Vect2();

            result.X = obj1.X > obj2.X ? obj1.X : obj2.X;
            result.Y = obj1.Y > obj2.Y ? obj1.Y : obj2.Y;

            return result;
        }

        public static void Max(ref Vect2 obj1, ref Vect2 obj2, out Vect2 result)
        {
            result.X = obj1.X > obj2.X ? obj1.X : obj2.X;
            result.Y = obj1.Y > obj2.Y ? obj1.Y : obj2.Y;
        }

        public static Vect2 Min(Vect2 obj1, Vect2 obj2)
        {
            Vect2 result = new Vect2();

            result.X = obj1.X < obj2.X ? obj1.X : obj2.X;
            result.Y = obj1.Y < obj2.Y ? obj1.Y : obj2.Y;

            return result;
        }

        public static void Min(ref Vect2 obj1, ref Vect2 obj2, out Vect2 result)
        {
            result.X = obj1.X < obj2.X ? obj1.X : obj2.X;
            result.Y = obj1.Y < obj2.Y ? obj1.Y : obj2.Y;
        }

        public static Vect2 Multiply(Vect2 value, float multiplier)
        {
            Vect2 result = new Vect2();

            result.X = value.X * multiplier;
            result.Y = value.Y * multiplier;

            return result;
        }

        public static void Multiply(ref Vect2 value, float multiplier, out Vect2 result)
        {
            result.X = value.X * multiplier;
            result.Y = value.Y * multiplier;
        }

        public static Vect2 Multiply(Vect2 value, Vect2 multiplier)
        {
            Vect2 result = new Vect2();

            result.X = value.X * multiplier.X;
            result.Y = value.Y * multiplier.Y;

            return result;
        }

        public static void Multiply(ref Vect2 value, ref Vect2 multiplier, out Vect2 result)
        {
            result.X = value.X * multiplier.X;
            result.Y = value.Y * multiplier.Y;
        }

        public static Vect2 Negate(Vect2 value)
        {
            Vect2 result = new Vect2();

            result.X = -value.X;
            result.Y = -value.Y;

            return result;
        }

        public static void Negate(ref Vect2 value, out Vect2 result)
        {
            result.X = -value.X;
            result.Y = -value.Y;
        }

        public Vect2 Normalize()
        {
            Vect2 result = new Vect2();
            float length = Length;

            result.X = X / length;
            result.Y = Y / length;

            return result;
        }

        public static Vect2 Normalize(Vect2 value)
        {
            Vect2 result = new Vect2();
            float length = value.Length;

            result.X = value.X / length;
            result.Y = value.Y / length;

            return result;
        }

        public static void Normalize(ref Vect2 value, out Vect2 result)
        {
            float length = value.Length;

            result.X = value.X / length;
            result.Y = value.Y / length;
        }

        public static Vect2 Reflect(Vect2 value, Vect2 normal)
        {
            float dot = Dot(value, normal);
            Vect2 diff = Subtract(normal, value);

            return Multiply(diff, 2 * dot);
        }

        public static void Reflect(ref Vect2 value, ref Vect2 normal, out Vect2 result)
        {
            float dot = Dot(value, normal);

            Vect2 diff; 
            Subtract(ref normal, ref value, out diff);

            result = Multiply(diff, 2 * dot);
        }

        public static Vect2 Subtract(Vect2 obj1, Vect2 obj2)
        {
            Vect2 result = new Vect2();

            result.X = obj1.X - obj2.X;
            result.Y = obj1.Y - obj2.Y;

            return result;
        }

        public static void Subtract(ref Vect2 obj1, ref Vect2 obj2, out Vect2 result)
        {
            result.X = obj1.X - obj2.X;
            result.Y = obj1.Y - obj2.Y;
        }

        public override string ToString()
        {
            return "(X:" + X + ", Y:" + Y + ")";
        }

        public static Vect2 Transform(Vect2 vect, Matrix3 matrix)
        {
            float x = (vect.X * matrix.R1_C1) + (vect.Y * matrix.R1_C2);
            float y = (vect.X * matrix.R2_C1) + (vect.Y * matrix.R2_C2);

            return new Vect2(x, y);
        }

        public static void Transform(ref Vect2 vect, ref Matrix3 matrix, out Vect2 result)
        {
            result.X = (vect.X * matrix.R1_C1) + (vect.Y * matrix.R1_C2);
            result.Y = (vect.X * matrix.R2_C1) + (vect.Y * matrix.R2_C2);
        }

        public static void Transform(Vect2[] sourceArray, int begin, ref Matrix3 matrix, Vect2[] destinationArray, int destBegin, int length)
        {
            int last = begin + length;
            int index = 0;

            for (int i = begin; i < last; i++)
            {
                Vect2 curr = sourceArray[i];

                float x = (curr.X * matrix.R1_C1) + (curr.Y * matrix.R1_C2);
                float y = (curr.X * matrix.R2_C1) + (curr.Y * matrix.R2_C2);

                destinationArray[destBegin + index] = new Vect2(x, y);
                index++;
            }
        }

        public static void Transform(ref Vect2[] source, ref Matrix3 matrix)
        {
            for (int i = 0; i < source.Length; i++)
            {
                Vect2 curr = source[i];

                float x = (curr.X * matrix.R1_C1) + (curr.Y * matrix.R1_C2);
                float y = (curr.X * matrix.R2_C1) + (curr.Y * matrix.R2_C2);

                source[i] = new Vect2(x, y);
            }
        }

        public Point ToPoint()
        {
            return new Point((int)X, (int)Y);
        }
    }
}