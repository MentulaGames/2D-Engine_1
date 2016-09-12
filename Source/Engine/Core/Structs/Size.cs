namespace Mentula.Engine.Core
{
    using System;

    public struct Size : IEquatable<Size>
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public bool IsEmpty { get { return Height == 0 && Width == 0; } }

        public static readonly Size Empty = new Size();

        public static Size operator +(Size sz1, Size sz2) { return Add(sz1, sz2); }
        public static Size operator -(Size sz1, Size sz2) { return Subtract(sz1, sz2); }
        public static bool operator ==(Size sz1, Size sz2) { return Equals(sz1, sz2); }
        public static bool operator !=(Size sz1, Size sz2) { return !Equals(sz1, sz2); }

        public Size(int width, int height)
        {
            Height = height;
            Width = width;
        }

        public Size(Point point)
        {
            Height = point.X;
            Width = point.Y;
        }

        public Size(Vect2 vect)
        {
            Height = (int)vect.Y;
            Width = (int)vect.X;
        }

        public static Size Add(Size size1, Size size2)
        {
            return new Size(size1.Width + size2.Width, size1.Height + size2.Height);
        }

        public override bool Equals(object obj)
        {
            if (obj is Size) return Equals((Size)obj);
            return false;
        }

        public bool Equals(Size size)
        {
            return size.Width == Width && size.Height == Height;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Utils.HASH_BASE;
                hash = hash * Utils.HASH_MULTIPLIER ^ Width.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ Height.GetHashCode();
                return hash;
            }
        }

        public static Size Subtract(Size size1, Size size2)
        {
            return new Size(size1.Width - size2.Width, size1.Height - size2.Height);
        }

        public override string ToString()
        {
            return $"W:{Width} H:{Height}";
        }
    }
}