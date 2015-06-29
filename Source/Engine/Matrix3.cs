namespace Mentula.Engine.Core
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{ToString()}")]
    public struct Matrix3 : IEquatable<Matrix3>
    {
        public float R1_C1, R1_C2, R1_C3;   // X |A, B, C|
        public float R2_C1, R2_C2, R2_C3;   // Y |D, E, F|
        public float R3_C1, R3_C2, R3_C3;   // Z |1, 0, 0|

        public Vect2 Translation { get { return new Vect2(R1_C3, R2_C3); } set { R1_C3 = value.X; R2_C3 = value.Y; } }

        public static readonly Matrix3 Identity;

        public Matrix3(
            float r1c1, float r1c2, float r1c3,
            float r2c1, float r2c2, float r2c3,
            float r3c1, float r3c2, float r3c3)
        {
            R1_C1 = r1c1;
            R1_C2 = r1c2;
            R1_C3 = r1c3;
            R2_C1 = r2c1;
            R2_C2 = r2c2;
            R2_C3 = r2c3;
            R3_C1 = r3c1;
            R3_C2 = r3c2;
            R3_C3 = r3c3;
        }

        public Matrix3(Matrix3 value)
        {
            R1_C1 = value.R1_C1;
            R1_C2 = value.R1_C2;
            R1_C3 = value.R1_C3;
            R2_C1 = value.R2_C1;
            R2_C2 = value.R2_C2;
            R2_C3 = value.R2_C3;
            R3_C1 = value.R3_C1;
            R3_C2 = value.R3_C2;
            R3_C3 = value.R3_C3;
        }

        static Matrix3()
        {
            Identity = new Matrix3(1, 0, 0, 0, 1, 0, 0, 0, 1);
        }

        public static Matrix3 Add(Matrix3 m1, Matrix3 m2)
        {
            Matrix3 result = new Matrix3();

            result.R1_C1 = m1.R1_C1 + m2.R1_C1;
            result.R1_C2 = m1.R1_C2 + m2.R1_C2;
            result.R1_C3 = m1.R1_C3 + m2.R1_C3;
            result.R2_C1 = m1.R2_C1 + m2.R2_C1;
            result.R2_C2 = m1.R2_C2 + m2.R2_C2;
            result.R2_C3 = m1.R2_C3 + m2.R2_C3;
            result.R3_C1 = m1.R3_C1 + m2.R3_C1;
            result.R3_C2 = m1.R3_C2 + m2.R3_C2;
            result.R3_C3 = m1.R3_C3 + m2.R3_C3;

            return result;
        }

        public static void Add(ref Matrix3 m1, ref Matrix3 m2, out Matrix3 result)
        {
            result.R1_C1 = m1.R1_C1 + m2.R1_C1;
            result.R1_C2 = m1.R1_C2 + m2.R1_C2;
            result.R1_C3 = m1.R1_C3 + m2.R1_C3;
            result.R2_C1 = m1.R2_C1 + m2.R2_C1;
            result.R2_C2 = m1.R2_C2 + m2.R2_C2;
            result.R2_C3 = m1.R2_C3 + m2.R2_C3;
            result.R3_C1 = m1.R3_C1 + m2.R3_C1;
            result.R3_C2 = m1.R3_C2 + m2.R3_C2;
            result.R3_C3 = m1.R3_C3 + m2.R3_C3;
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
            return new Matrix3(scale, 0, 0, 0, scale, 0, 0, 0, scale);
        }

        public static void ApplyScale(float scale, out Matrix3 result)
        {
            result = new Matrix3(scale, 0, 0, 0, scale, 0, 0, 0, scale);
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
                R1_C1 == other.R1_C1 &&
                R1_C2 == other.R1_C2 &&
                R1_C3 == other.R1_C3 &&
                R2_C1 == other.R2_C1 &&
                R2_C2 == other.R2_C2 &&
                R2_C3 == other.R2_C3 &&
                R3_C1 == other.R3_C1 &&
                R3_C2 == other.R3_C2 &&
                R3_C3 == other.R3_C3;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Utils.HASH_BASE;

                hash *= Utils.HASH_MULTIPLIER ^ R1_C1.GetHashCode();
                hash *= Utils.HASH_MULTIPLIER ^ R1_C2.GetHashCode();
                hash *= Utils.HASH_MULTIPLIER ^ R1_C3.GetHashCode();
                hash *= Utils.HASH_MULTIPLIER ^ R2_C1.GetHashCode();
                hash *= Utils.HASH_MULTIPLIER ^ R2_C2.GetHashCode();
                hash *= Utils.HASH_MULTIPLIER ^ R2_C3.GetHashCode();
                hash *= Utils.HASH_MULTIPLIER ^ R3_C1.GetHashCode();
                hash *= Utils.HASH_MULTIPLIER ^ R3_C2.GetHashCode();
                hash *= Utils.HASH_MULTIPLIER ^ R3_C3.GetHashCode();

                return hash;
            }
        }

        public override string ToString()
        {
            return "{ (A: " + R1_C1 + ", B: " + R1_C2 + ", C: " + R1_C3 + ") (D: " + R2_C1 + ", E: " + R2_C2 + ", F: " + R2_C3 + ") (G: " + R3_C1 + ", H: " + R3_C2 + ", I: " + R3_C3 + ") }";
        }

        public string ToTableString()
        {
            return "|A: " + R1_C1 + ", B: " + R1_C2 + ", C: " + R1_C3 + "|\n|D: " + R2_C1 + ", E: " + R2_C2 + ", F: " + R2_C3 + "|\n|G: " + R3_C1 + ", H: " + R3_C2 + ", I: " + R3_C3 + "|";
        }
    }
}