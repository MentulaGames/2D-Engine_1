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

        public static readonly Point Zero;
        public static readonly Point UnitX;
        public static readonly Point UnitY;
        public static readonly Point One;

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

        static Point()
        {
            Zero = new Point(0);
            UnitX = new Point(1, 0);
            UnitY = new Point(0, 1);
            One = new Point(1);
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
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
            return "(X:" + X + ", Y:" + Y + ")";
        }

        public Vect2 ToVecr2()
        {
            return new Vect2(X, Y);
        }
    }
}