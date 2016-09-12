namespace Mentula.Engine.Core
{
    using ExtendedMath;
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{ToString()}")]
    [Serializable]
    public struct Color : IPackedVector<uint>, IEquatable<Color>, IEquatable<Vect4>
    {
        public byte A;
        public byte B;
        public byte G;
        public byte R;

        public uint PackedValue
        {
            get
            {
                uint pack = 0;

                pack |= (uint)(A & byte.MaxValue) << 24;
                pack |= (uint)(R & byte.MaxValue) << 16;
                pack |= (uint)(G & byte.MaxValue) << 8;
                pack |= (uint)(B & byte.MaxValue);

                return pack;
            }
            set
            {
                A = (byte)((value >> 24) & byte.MaxValue);
                R = (byte)((value >> 16) & byte.MaxValue);
                G = (byte)((value >> 8) & byte.MaxValue);
                B = (byte)(value & byte.MaxValue);
            }
        }

        public static Color operator *(Color color, float multiplier) { return Multiply(color, multiplier); }
        public static bool operator ==(Color obj1, Color obj2) { return obj1.Equals(obj2); }
        public static bool operator !=(Color obj1, Color obj2) { return !obj1.Equals(obj2); }

        public Color(byte value)
        {
            A = byte.MaxValue;
            B = value;
            G = value;
            R = value;
        }

        public Color(int b, int g, int r)
        {
            A = byte.MaxValue;
            B = (byte)MathI.Clamp(byte.MinValue, byte.MaxValue, b);
            G = (byte)MathI.Clamp(byte.MinValue, byte.MaxValue, g);
            R = (byte)MathI.Clamp(byte.MinValue, byte.MaxValue, r);
        }

        public Color(int b, int g, int r, int a)
        {
            A = (byte)MathI.Clamp(byte.MinValue, byte.MaxValue, a);
            B = (byte)MathI.Clamp(byte.MinValue, byte.MaxValue, b);
            G = (byte)MathI.Clamp(byte.MinValue, byte.MaxValue, g);
            R = (byte)MathI.Clamp(byte.MinValue, byte.MaxValue, r);
        }

        public Color(float b, float g, float r)
        {
            A = 255;
            B = (byte)MathF.Clamp(byte.MinValue, byte.MaxValue, b);
            G = (byte)MathF.Clamp(byte.MinValue, byte.MaxValue, g);
            R = (byte)MathF.Clamp(byte.MinValue, byte.MaxValue, r);
        }

        public Color(float b, float g, float r, float a)
        {
            A = (byte)MathF.Clamp(byte.MinValue, byte.MaxValue, a);
            B = (byte)MathF.Clamp(byte.MinValue, byte.MaxValue, b);
            G = (byte)MathF.Clamp(byte.MinValue, byte.MaxValue, g);
            R = (byte)MathF.Clamp(byte.MinValue, byte.MaxValue, r);
        }

        public Color(Vect3 value)
        {
            A = 255;
            B = (byte)MathF.Clamp(byte.MinValue, byte.MaxValue, value.X);
            G = (byte)MathF.Clamp(byte.MinValue, byte.MaxValue, value.Y);
            R = (byte)MathF.Clamp(byte.MinValue, byte.MaxValue, value.Z);
        }

        public Color(Vect4 value)
        {
            A = (byte)MathF.Clamp(byte.MinValue, byte.MaxValue, value.W);
            B = (byte)MathF.Clamp(byte.MinValue, byte.MaxValue, value.X);
            G = (byte)MathF.Clamp(byte.MinValue, byte.MaxValue, value.Y);
            R = (byte)MathF.Clamp(byte.MinValue, byte.MaxValue, value.Z);
        }

        public Color(System.Drawing.Color value)
        {
            A = value.A;
            B = value.B;
            G = value.G;
            R = value.R;
        }

        public override bool Equals(object obj)
        {
            if (obj is Color) return Equals((Color)obj);
            return false;
        }

        public bool Equals(Color other)
        {
            return A == other.A && B == other.B && G == other.G && R == other.R;
        }

        public bool Equals(Vect4 other)
        {
            return A == other.W && B == other.X && G == other.Y && R == other.Z;
        }

        public static Color FromNonPremultiplied(int r, int g, int b, float a)
        {
            if (a < 0 || a > 1) throw new ArgumentException("The alpha must be between 0 and 1.");

            Color result = new Color();

            result.A = (byte)(byte.MaxValue * a);
            result.B = (byte)(MathI.Clamp(byte.MinValue, byte.MaxValue, b) * a);
            result.G = (byte)(MathI.Clamp(byte.MinValue, byte.MaxValue, g) * a);
            result.R = (byte)(MathI.Clamp(byte.MinValue, byte.MaxValue, r) * a);

            return result;
        }

        public static Color FromNonPremultiplied(Vect4 value)
        {
            if (value.W < 0 || value.W > 1) throw new ArgumentException("The alpha must be between 0 and 1.");

            Color result = new Color();

            result.A = (byte)(byte.MaxValue * value.W);
            result.B = (byte)(MathF.Clamp(byte.MinValue, byte.MaxValue, value.X) * value.X);
            result.G = (byte)(MathF.Clamp(byte.MinValue, byte.MaxValue, value.Y) * value.Y);
            result.R = (byte)(MathF.Clamp(byte.MinValue, byte.MaxValue, value.Z) * value.Z);

            return result;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Utils.HASH_BASE;

                hash = hash * Utils.HASH_MULTIPLIER ^ A.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ B.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ G.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ R.GetHashCode();

                return hash;
            }
        }

        public static Color Lerp(Color obj1, Color obj2, float amount)
        {
            if (amount < 0 || amount > 1) throw new ArgumentException("amount must be between 0 and 1.");

            Color result = new Color();

            result.A = (byte)((obj2.A - obj1.A) * amount);
            result.B = (byte)((obj2.B - obj1.B) * amount);
            result.G = (byte)((obj2.G - obj1.G) * amount);
            result.R = (byte)((obj2.R - obj1.R) * amount);

            return result;
        }

        public static Color Multiply(Color value, float multiplier)
        {
            Color result = new Color();

            result.A = (byte)(MathF.Clamp(byte.MinValue, byte.MaxValue, value.A * multiplier));
            result.B = (byte)(MathF.Clamp(byte.MinValue, byte.MaxValue, value.B * multiplier));
            result.G = (byte)(MathF.Clamp(byte.MinValue, byte.MaxValue, value.G * multiplier));
            result.R = (byte)(MathF.Clamp(byte.MinValue, byte.MaxValue, value.R * multiplier));

            return result;
        }

        public override string ToString()
        {
            return $"(R:{R}, G:{G}, B:{B}, A:{A})";
        }

        public Vect3 ToVect3()
        {
            return new Vect3(B, G, R);
        }

        public Vect4 ToVect4()
        {
            return new Vect4(A, B, G, R);
        }
    }
}