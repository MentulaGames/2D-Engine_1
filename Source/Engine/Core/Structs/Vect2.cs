namespace Mentula.Engine.Core
{
    using System;
    using System.Diagnostics;
    using ExtendedMath;

    [DebuggerDisplay("{ToString()}")]
    [Serializable]
    public struct Vect2 : IEquatable<Vect2>, IEquatable<Point>
    {
        public float X;
        public float Y;

        public float Area { get { return X * Y; } }
        public float Length { get { return (float)Math.Sqrt(LengthSquared); } }
        public float LengthSquared { get { return (X * X) + (Y * Y); } }

        public static Vect2 Zero { get { return zero; } }
        public static Vect2 UnitX { get { return unitX; } }
        public static Vect2 UnitY { get { return unitY; } }
        public static Vect2 One { get { return one; } }
        public static Vect2 Negative { get { return negative; } }

        private static readonly Vect2 zero = new Vect2();
        private static readonly Vect2 unitX = new Vect2(1, 0);
        private static readonly Vect2 unitY = new Vect2(0, 1);
        private static readonly Vect2 one = new Vect2(1);
        private static readonly Vect2 negative = new Vect2(-1);

        public static Vect2 operator -(Vect2 value) { return Negate(value); }
        public static Vect2 operator -(Vect2 value1, Vect2 value2) { return Subtract(value1, value2); }
        public static bool operator !=(Vect2 value1, Vect2 value2) { return !value1.Equals(value2); }
        public static Vect2 operator *(float scaleFactor, Vect2 value) { return Multiply(value, scaleFactor); }
        public static Vect2 operator *(Vect2 value, float scaleFactor) { return Multiply(value, scaleFactor); }
        public static Vect2 operator *(Vect2 value1, Vect2 value2) { return Multiply(value1, value2); }
        public static Vect2 operator *(Vect2 value, Matrix3 matrix) { return Transform(value, matrix); }
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

        public static Vect2 Abs(Vect2 obj)
        {
            float vX = Math.Abs(obj.X);
            float vY = Math.Abs(obj.Y);
            return new Vect2(vX, vY);
        }

        public static void Abs(ref Vect2 obj, out Vect2 result)
        {
            result.X = obj.X < 0 ? -obj.X : obj.X;
            result.Y = obj.Y < 0 ? -obj.Y : obj.Y;
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
            float pX = MathEx.Barycentric(vertex1.X, vertex2.X, vertex3.X, b2, b3);
            float pY = MathEx.Barycentric(vertex1.Y, vertex2.Y, vertex3.Y, b2, b3);

            return new Vect2(pX, pY);
        }

        public static void Barycentric(ref Vect2 vertex1, ref Vect2 vertex2, ref Vect2 vertex3, float b2, float b3, out Vect2 result)
        {
            result.X = MathEx.Barycentric(vertex1.X, vertex2.X, vertex3.X, b2, b3);
            result.Y = MathEx.Barycentric(vertex1.Y, vertex2.Y, vertex3.Y, b2, b3);
        }

        public static Vect2 Clamp(Vect2 min, Vect2 max, Vect2 value)
        {
            float vX = MathF.Clamp(min.X, min.X, value.X);
            float vY = MathF.Clamp(min.Y, max.Y, value.Y);

            return new Vect2(vX, vY);
        }

        public static void Clamp(ref Vect2 min, ref Vect2 max, ref Vect2 value, out Vect2 result)
        {
            result.X = MathF.Clamp(min.X, min.X, value.X);
            result.Y = MathF.Clamp(min.Y, max.Y, value.Y);
        }

        public static float Distance(Vect2 obj1, Vect2 obj2)
        {
            return (float)Math.Sqrt(DistanceSquared(obj1, obj2));
        }

        public static void Distance(ref Vect2 obj1, ref Vect2 obj2, out float result)
        {
            result = (float)Math.Sqrt(DistanceSquared(obj1, obj2));
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
            if (obj is Vect2) return Equals((Vect2)obj);
            if (obj is Point) return Equals((Point)obj);
            return false;
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

                return hash;
            }
        }

        public static Vect2 Lerp(Vect2 min, Vect2 max, float amount)
        {
            if (amount < 0 || amount > 1) throw new ArgumentException("amount must be between 0 and 1.");
            float vX = MathF.Lerp(min.X, max.X, amount);
            float vY = MathF.Lerp(min.Y, max.Y, amount);

            return new Vect2(vX, vY);
        }

        public static void Lerp(ref Vect2 min, ref Vect2 max, float amount, out Vect2 result)
        {
            if (amount < 0 || amount > 1) throw new ArgumentException("amount must be between 0 and 1.");
            result.X = MathF.Lerp(min.X, max.X, amount);
            result.Y = MathF.Lerp(min.Y, max.Y, amount);
        }

        public static Vect2 Lerp(Vect2 min, Vect2 max, Vect2 amount)
        {
            if (amount.Area < 0 || amount.Area > 1) throw new ArgumentException("amount x and y must be between 0 and 1.");
            float vX = MathF.Lerp(min.X, max.X, amount.X);
            float vY = MathF.Lerp(min.Y, max.Y, amount.Y);

            return new Vect2(vX, vY);
        }

        public static void Lerp(ref Vect2 min, ref Vect2 max, ref Vect2 amount, out Vect2 result)
        {
            if (amount.Area < 0 || amount.Area > 1) throw new ArgumentException("amount x and y must be between 0 and 1.");
            result.X = MathF.Lerp(min.X, max.X, amount.X);
            result.Y = MathF.Lerp(min.Y, max.Y, amount.Y);
        }

        public static Vect2 InvLerp(Vect2 min, Vect2 max, float value)
        {
            float vX = MathF.InvLerp(min.X, max.X, value);
            float vY = MathF.InvLerp(min.Y, max.Y, value);
            return new Vect2(vX, vY);
        }

        public static void InvLerp(ref Vect2 min, ref Vect2 max, float value, out Vect2 result)
        {
            result.X = MathF.InvLerp(min.X, max.X, value);
            result.Y = MathF.InvLerp(min.Y, max.Y, value);
        }

        public static Vect2 InvLerp(Vect2 min, Vect2 max, Vect2 value)
        {
            float vX = MathF.InvLerp(min.X, max.X, value.X);
            float vY = MathF.InvLerp(min.Y, max.Y, value.Y);
            return new Vect2(vX, vY);
        }

        public static void InvLerp(ref Vect2 min, ref Vect2 max, ref Vect2 value, out Vect2 result)
        {
            result.X = MathF.InvLerp(min.X, max.X, value.X);
            result.Y = MathF.InvLerp(min.Y, max.Y, value.Y);
        }

        public static Vect2 Max(Vect2 obj1, Vect2 obj2)
        {
            Vect2 result = new Vect2();

            result.X = Math.Max(obj1.X, obj2.X);
            result.Y = Math.Max(obj1.Y, obj2.Y);

            return result;
        }

        public static void Max(ref Vect2 obj1, ref Vect2 obj2, out Vect2 result)
        {
            result.X = Math.Max(obj1.X, obj2.X);
            result.Y = Math.Max(obj1.Y, obj2.Y);
        }

        public static Vect2 Min(Vect2 obj1, Vect2 obj2)
        {
            Vect2 result = new Vect2();

            result.X = Math.Min(obj1.X, obj2.X);
            result.Y = Math.Min(obj1.Y, obj2.Y);

            return result;
        }

        public static void Min(ref Vect2 obj1, ref Vect2 obj2, out Vect2 result)
        {
            result.X = Math.Min(obj1.X, obj2.X);
            result.Y = Math.Min(obj1.Y, obj2.Y);
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
            return $"(X: {X}, Y:{Y})";
        }

        public static Vect2 Transform(Vect2 vect, Matrix3 matrix)
        {
            float x = (vect.X * matrix.A) + (vect.Y * matrix.B);
            float y = (vect.X * matrix.D) + (vect.Y * matrix.E);

            return new Vect2(x, y);
        }

        public static void Transform(ref Vect2 vect, ref Matrix3 matrix, out Vect2 result)
        {
            result.X = (vect.X * matrix.A) + (vect.Y * matrix.B);
            result.Y = (vect.X * matrix.D) + (vect.Y * matrix.E);
        }

        public static void Transform(Vect2[] sourceArray, int begin, ref Matrix3 matrix, Vect2[] destinationArray, int destBegin, int length)
        {
            int last = begin + length;
            int index = 0;

            for (int i = begin; i < last; i++)
            {
                Vect2 curr = sourceArray[i];

                float x = (curr.X * matrix.A) + (curr.Y * matrix.B);
                float y = (curr.X * matrix.D) + (curr.Y * matrix.E);

                destinationArray[destBegin + index] = new Vect2(x, y);
                index++;
            }
        }

        public static void Transform(ref Vect2[] source, ref Matrix3 matrix)
        {
            for (int i = 0; i < source.Length; i++)
            {
                Vect2 curr = source[i];

                float x = (curr.X * matrix.A) + (curr.Y * matrix.B);
                float y = (curr.X * matrix.D) + (curr.Y * matrix.E);

                source[i] = new Vect2(x, y);
            }
        }

        public Point ToPoint()
        {
            return new Point((int)X, (int)Y);
        }
    }
}