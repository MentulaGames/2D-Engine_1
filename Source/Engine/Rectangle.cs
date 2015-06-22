using System;
using System.Diagnostics;

namespace Engine.Core
{
    [DebuggerDisplay("{ToString()}")]
    [Serializable]
    public struct Rectangle
    {
        public int Height;
        public int Width;
        public int X;
        public int Y;

        public int Bottom
        {
            get
            {
                if (Height > -1) return Y + Height;
                else return Y;
            }
        }

        public Point Center
        {
            get
            {
                return new Point(X + (Width >> 1), Y + (Height >> 1));
            }
        }

        public int Left
        {
            get
            {
                if (Width > -1) return X;
                else return X - Width;
            }
        }

        public Point Location
        {
            get
            {
                return new Point(X, Y);
            }

            set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public int Right
        {
            get
            {
                if (Width > -1) return X + Width;
                else return X;
            }
        }

        public int Top
        {
            get
            {
                if (Height > -1) return Y;
                else return Y + Height;
            }
        }

        public static readonly Rectangle Empty;

        public Rectangle(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public Rectangle(int x, int y, int length)
        {
            X = x;
            Y = y;
            Width = length;
            Height = length;
        }

        public Rectangle(Point location, int width, int height)
        {
            X = location.X;
            Y = location.Y;
            Width = width;
            Height = height;
        }

        public Rectangle(Point location, int length)
        {
            X = location.X;
            Y = location.Y;
            Width = length;
            Height = length;
        }

        public Rectangle(Rectangle value)
        {
            X = value.X;
            Y = value.Y;
            Width = value.Width;
            Height = value.Height;
        }

        static Rectangle()
        {
            Empty = new Rectangle(0, 0, 0, 0);
        }

        public bool Contains(int x, int y)
        {
            return x >= Left && x <= Right && y >= Top && y <= Bottom;
        }

        public bool Contains(Point point)
        {
            return point.X >= Left && point.X <= Right && point.Y >= Top && point.Y <= Bottom;
        }

        public void Contains(ref Point point, out bool result)
        {
            result = point.X >= Left && point.X <= Right && point.Y >= Top && point.Y <= Bottom;
        }

        public bool Contains(Rectangle rect)
        {
            return rect.Left >= Left && rect.Right <= Right && rect.Top >= Top && rect.Bottom <= Bottom;
        }

        public void Contains(ref Rectangle rect, out bool result)
        {
            result = rect.Left >= Left && rect.Right <= Right && rect.Top >= Top && rect.Bottom <= Bottom;
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }

        public bool Equals(Rectangle other)
        {
            return X == other.X && Y == other.Y && Width == other.Width && Height == other.Height;
        }

        public override int GetHashCode()
        {
            const int MULTIPLYR = 16777619;

            unchecked
            {
                int hash = (int)2166136261;

                hash *= MULTIPLYR ^ X.GetHashCode();
                hash *= MULTIPLYR ^ Y.GetHashCode();
                hash *= MULTIPLYR ^ Width.GetHashCode();
                hash *= MULTIPLYR ^ Height.GetHashCode();

                return hash;
            }
        }

        public void Inflate(int horizontal, int vertical)
        {
            Width += horizontal;
            Height += vertical;
        }

        public Rectangle Intersect(Rectangle other)
        {
            int x1 = Left;
            int x3 = other.Left;
            int y2 = Top;
            int y4 = other.Top;

            int x2 = x1 + Math.Abs(Width);
            int x4 = x3 + Math.Abs(other.Width);
            int y1 = y2 - Math.Abs(Height);
            int y3 = y4 - Math.Abs(other.Height);

            int xL = Math.Max(x1, x3);
            int xS = Math.Min(x2, x4);

            if (xS <= xL) return Rectangle.Empty;

            int yL = Math.Max(y1, y3);
            int yS = Math.Min(y2, y4);

            if (yS <= yL) return Rectangle.Empty;

            return new Rectangle(xL, yS, xS - xL, yS - yL);
        }

        public void Intersect(ref Rectangle other, out Rectangle result)
        {
            int x1 = Left;
            int x3 = other.Left;
            int y2 = Top;
            int y4 = other.Top;

            int x2 = x1 + Math.Abs(Width);
            int x4 = x3 + Math.Abs(other.Width);
            int y1 = y2 - Math.Abs(Height);
            int y3 = y4 - Math.Abs(other.Height);

            int xL = Math.Max(x1, x3);
            int xS = Math.Min(x2, x4);

            if (xS <= xL) result = Rectangle.Empty;
            else
            {
                int yL = Math.Max(y1, y3);
                int yS = Math.Min(y2, y4);

                if (yS <= yL) result = Rectangle.Empty;
                else result = new Rectangle(xL, yS, xS - xL, yS - yL);
            }
        }

        public static Rectangle Intersect(Rectangle rect, Rectangle other)
        {
            int x1 = rect.Left;                             // Get the Abs(X) of rect
            int x3 = other.Left;                            // Get the Abs(X) of other
            int y2 = rect.Top;                              // Get the Abs(Y) of rect
            int y4 = other.Top;                             // Get the Abs(Y) of rect

            int x2 = x1 + Math.Abs(rect.Width);             // Get the X + Abs(Width) of rect
            int x4 = x3 + Math.Abs(other.Width);            // Get the X + Abs(Width) of other
            int y1 = y2 - Math.Abs(rect.Height);            // Get the Y - Abs(Height) of rect
            int y3 = y4 - Math.Abs(other.Height);           // Get the Y - Abs(Height) of other

            int xL = Math.Max(x1, x3);                      // Get the furthest X
            int xS = Math.Min(x2, x4);                      // Get the closest X

            if (xS <= xL) return Rectangle.Empty;           // Check for X intersection

            int yL = Math.Max(y1, y3);                      // Get the furthest Y
            int yS = Math.Min(y2, y4);                      // Get the closest Y

            if (yS <= yL) return Rectangle.Empty;           // Check for Y intersection

            return new Rectangle(xL, yS, xS - xL, yS - yL); // Rectangle(Furthest(X), Closest(Y), Closest(X) - Furthest(X), Closest(Y) - Furthest(Y)) 
        }

        public static void Intersect(ref Rectangle rect, ref Rectangle other, out Rectangle result)
        {
            int x1 = rect.Left;
            int x3 = other.Left;
            int y2 = rect.Top;
            int y4 = other.Top;

            int x2 = x1 + Math.Abs(rect.Width);
            int x4 = x3 + Math.Abs(other.Width);
            int y1 = y2 - Math.Abs(rect.Height);
            int y3 = y4 - Math.Abs(other.Height);

            int xL = Math.Max(x1, x3);
            int xS = Math.Min(x2, x4);

            if (xS <= xL) result = Rectangle.Empty;
            else
            {
                int yL = Math.Max(y1, y3);
                int yS = Math.Min(y2, y4);

                if (yS <= yL) result = Rectangle.Empty;
                else result = new Rectangle(xL, yS, xS - xL, yS - yL);
            }
        }

        public void Move(int x, int y)
        {
            X += x;
            Y += y;
        }

        public void Move(Point offset)
        {
            X += offset.X;
            Y += offset.Y;
        }

        public override string ToString()
        {
            return "(X:" + X + ", Y:" + Y + ", W:" + Width + ", H:" + Height + ")";
        }

        public static Rectangle Union(Rectangle rect1, Rectangle rect2)
        {
            int x = Math.Min(rect1.Left, rect2.Left);
            int y = Math.Min(rect1.Top, rect2.Top);

            int x2 = Math.Max(rect1.Right, rect2.Right);
            int y2 = Math.Max(rect1.Bottom, rect2.Bottom);

            int width = x2 - x;
            int height = y2 - y;

            return new Rectangle(x, y, width, height);
        }

        public static void Union(ref Rectangle rect1, ref Rectangle rect2, out Rectangle result)
        {
            result.X = Math.Min(rect1.Left, rect2.Left);
            result.Y = Math.Min(rect1.Top, rect2.Top);

            int x2 = Math.Max(rect1.Right, rect2.Right);
            int y2 = Math.Max(rect1.Bottom, rect2.Bottom);

            result.Width = x2 - result.X;
            result.Height = y2 - result.Y;
        }
    }
}