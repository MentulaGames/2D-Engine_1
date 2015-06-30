namespace Mentula.Engine.Core
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{ToString()}")]
    public struct Matrix3 : IEquatable<Matrix3>
    {
        public float A, B, C;
        public float D, E, F;
        public float G, H, I;

        public Vect2 Translation { get { return new Vect2(C, F); } set { C = value.X; F = value.Y; } }

        public static readonly Matrix3 Identity;

        public Matrix3(
            float r1c1, float r1c2, float r1c3,
            float r2c1, float r2c2, float r2c3,
            float r3c1, float r3c2, float r3c3)
        {
            A = r1c1;
            B = r1c2;
            C = r1c3;
            D = r2c1;
            E = r2c2;
            F = r2c3;
            G = r3c1;
            H = r3c2;
            I = r3c3;
        }

        public Matrix3(Matrix3 value)
        {
            A = value.A;
            B = value.B;
            C = value.C;
            D = value.D;
            E = value.E;
            F = value.F;
            G = value.G;
            H = value.H;
            I = value.I;
        }

        static Matrix3()
        {
            Identity = new Matrix3(1, 0, 0, 0, 1, 0, 0, 0, 1);
        }

        public static Matrix3 ApplyRotation(float radians)
        {
            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);

            return new Matrix3(cos, -sin, 0, sin, cos, 0, 0, 0, 1);
        }

        public static void ApplyRotation(float radians, out Matrix3 result)
        {
            float cos = (float)Math.Cos(radians);
            float sin = (float)Math.Sin(radians);

            result = new Matrix3(cos, -sin, 0, sin, cos, 0, 0, 0, 1);
        }

        public static Matrix3 ApplyScale(float scale)
        {
            return new Matrix3(scale, 0, 0, 0, scale, 0, 0, 0, 1);
        }

        public static void ApplyScale(float scale, out Matrix3 result)
        {
            result = new Matrix3(scale, 0, 0, 0, scale, 0, 0, 0, 1);
        }

        public static Matrix3 ApplyScale(Vect2 scale)
        {
            return new Matrix3(scale.X, 0, 0, 0, scale.Y, 0, 0, 0, 1);
        }

        public static void ApplyScale(Vect2 scale, out Matrix3 result)
        {
            result = new Matrix3(scale.X, 0, 0, 0, scale.Y, 0, 0, 0, 1);
        }

        public static Matrix3 ApplyTranslation(Vect2 transform)
        {
            return new Matrix3(0, 0, transform.X, 0, 0, transform.Y, 0, 0, 0);
        }

        public static void ApplyTranslation(Vect2 transform, out Matrix3 result)
        {
            result = new Matrix3(0, 0, transform.X, 0, 0, transform.Y, 0, 0, 0);
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }

        public bool Equals(Matrix3 other)
        {
            return
                A == other.A &&
                B == other.B &&
                C == other.C &&
                D == other.D &&
                E == other.E &&
                F == other.F &&
                G == other.G &&
                H == other.H &&
                I == other.I;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Utils.HASH_BASE;

                hash = hash * Utils.HASH_MULTIPLIER ^ A.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ B.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ C.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ D.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ E.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ F.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ G.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ H.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ I.GetHashCode();

                return hash;
            }
        }

        public static Matrix3 Multiply(Matrix3 matrix, float multiplier)
        {
            Matrix3 result = new Matrix3();

            result.A = matrix.A * multiplier;
            result.E = matrix.E * multiplier;
            result.I = matrix.I * multiplier;

            return result;
        }

        public static void Multiply(ref Matrix3 matrix, float multiplier, out Matrix3 result)
        {
            result = new Matrix3();
            result.A = matrix.A * multiplier;
            result.E = matrix.E * multiplier;
            result.I = matrix.I * multiplier;
        }

        public static Matrix3 Multiply(Matrix3 m1, Matrix3 m2)
        {
            Matrix3 result = new Matrix3();

            result.A = m1.A * m2.A + m1.B * m2.D + m1.C * m2.G;
            result.B = m1.A * m2.B + m1.B * m2.E + m1.C * m2.H;
            result.C = m1.A * m2.C + m1.B * m2.F + m1.C * m2.I;
            result.D = m1.D * m2.A + m1.E * m2.D + m1.F * m2.G;
            result.E = m1.D * m2.B + m1.E * m2.E + m1.F * m2.H;
            result.F = m1.D * m2.C + m1.E * m2.F + m1.F * m2.I;
            result.G = m1.G * m2.A + m1.H * m2.D + m1.I * m2.G;
            result.H = m1.G * m2.B + m1.H * m2.E + m1.I * m2.H;
            result.I = m1.G * m2.C + m1.H * m2.F + m1.I * m2.I;

            return result;
        }

        public static void Multiply(ref Matrix3 m1, ref Matrix3 m2, out Matrix3 result)
        {
            result.A = m1.A * m2.A + m1.B * m2.D + m1.C * m2.G;
            result.B = m1.A * m2.B + m1.B * m2.E + m1.C * m2.H;
            result.C = m1.A * m2.C + m1.B * m2.F + m1.C * m2.I;
            result.D = m1.D * m2.A + m1.E * m2.D + m1.F * m2.G;
            result.E = m1.D * m2.B + m1.E * m2.E + m1.F * m2.H;
            result.F = m1.D * m2.C + m1.E * m2.F + m1.F * m2.I;
            result.G = m1.G * m2.A + m1.H * m2.D + m1.I * m2.G;
            result.H = m1.G * m2.B + m1.H * m2.E + m1.I * m2.H;
            result.I = m1.G * m2.C + m1.H * m2.F + m1.I * m2.I;
        }

        public override string ToString()
        {
            return "{ (A: " + A + ", B: " + B + ", C: " + C + ") (D: " + D + ", E: " + E + ", F: " + F + ") (G: " + G + ", H: " + H + ", I: " + I + ") }";
        }

        public string ToTableString()
        {
            return "|" + A + " " + B + " " + C + "|\n|" + D + " " + E + " " + F + "|\n|" + G + " " + H + " " + I + "|";
        }
    }
}