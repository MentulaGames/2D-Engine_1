namespace Mentula.Engine.Core
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{ToString()}")]
    [Serializable]
    public struct Point : IEquatable<Point>
    {
        public int X;
        public int Y;

        public static Point Zero { get { return zero; } }
        public static Point UnitX { get { return unitX; } }
        public static Point UnitY { get { return unitY; } }
        public static Point One { get { return one; } }
        public static Point InvOne { get { return invOne; } }

        private static readonly Point zero = new Point();
        private static readonly Point unitX = new Point(1, 0);
        private static readonly Point unitY = new Point(0, 1);
        private static readonly Point one = new Point(1, 0);
        private static readonly Point invOne = new Point(-1);

        public static bool operator !=(Point value1, Point value2) { return !value1.Equals(value2); }
        public static bool operator ==(Point value1, Point value2) { return value1.Equals(value2); }

        public Point(int value)
        {
            X = value;
            Y = value;
        }

        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Point(Point value)
        {
            X = value.X;
            Y = value.Y;
        }

        public override bool Equals(object obj)
        {
            if (obj is Point) return Equals((Point)obj);
            return false;
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

        public override string ToString()
        {
            return $"(X:{X}, Y:{Y})";
        }

        public Vect2 ToVect2()
        {
            return new Vect2(X, Y);
        }
    }
}