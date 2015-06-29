﻿namespace Mentula.Engine.Core
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{ToString()}")]
    [Serializable]
    public struct Triangle : IEquatable<Triangle>
    {
        public Point A;
        public Point B;
        public Point C;

        public int Bottom { get { return Utils.Max(A.Y, B.Y, C.Y); } }
        public int Left { get { return Utils.Min(A.X, B.X, C.X); } }
        public int Right { get { return Utils.Max(A.X, B.X, C.X); } }
        public int Top { get { return Utils.Min(A.Y, B.Y, C.Y); } }

        public static readonly Triangle Empty;

        public Triangle(int aX, int aY, int bX, int bY, int cX, int cY)
        {
            A = new Point(aX, bY);
            B = new Point(bX, bY);
            C = new Point(cX, cY);
        }

        public Triangle(Point a, Point b, Point c)
        {
            A = a;
            B = b;
            C = c;
        }

        public Triangle(Triangle value)
        {
            A = value.A;
            B = value.B;
            C = value.C;
        }

        static Triangle()
        {
            Empty = new Triangle(0, 0, 0, 0, 0, 0);
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }

        public bool Equals(Triangle other)
        {
            return A.Equals(other.A) && B.Equals(other.B) && C.Equals(other.C);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Utils.HASH_BASE;

                hash *= Utils.HASH_MULTIPLIER ^ A.GetHashCode();
                hash *= Utils.HASH_MULTIPLIER ^ B.GetHashCode();
                hash *= Utils.HASH_MULTIPLIER ^ C.GetHashCode();

                return hash;
            }
        }

        public override string ToString()
        {
            return "A=" + A + ", B=" + B + ", C=" + C;
        }
    }
}