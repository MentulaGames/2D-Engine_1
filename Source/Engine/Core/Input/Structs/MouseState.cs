namespace Mentula.Engine.Core.Input
{
    using System;

    public struct MouseState : IEquatable<MouseState>
    {
        public bool LeftButton { get; internal set; }
        public bool MiddleButton { get; internal set; }
        public bool RightButton { get; internal set; }
        public int ScrollWheelValue { get; internal set; }
        public int X { get; internal set; }
        public int Y { get; internal set; }

        public static bool operator ==(MouseState obj1, MouseState obj2) { return obj1.Equals(obj2); }
        public static bool operator !=(MouseState obj1, MouseState obj2) { return !obj1.Equals(obj2); }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }

        public bool Equals(MouseState other)
        {
            return
                LeftButton == other.LeftButton &&
                MiddleButton == other.MiddleButton &&
                RightButton == other.RightButton &&
                ScrollWheelValue == other.ScrollWheelValue &&
                X == other.X &&
                Y == other.Y;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Utils.HASH_BASE;

                hash = hash * Utils.HASH_MULTIPLIER ^ LeftButton.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ MiddleButton.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ RightButton.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ ScrollWheelValue.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ X.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ Y.GetHashCode();

                return hash;
            }
        }

        public override string ToString()
        {
            return "(X: " + X + ", Y: " + Y + ", Btns: " +
                (LeftButton ? '1' : '0') +
                (MiddleButton ? '1' : '0') +
                (RightButton ? '1' : '0');
        }
    }
}
