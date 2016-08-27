namespace Mentula.Engine.Core
{
    using System;
    using System.Diagnostics;

    [DebuggerDisplay("{ToString()")]
    public class GameTime : IEquatable<GameTime>
    {
        public float DeltaTime { get { return (float)ElapsedGameTime.TotalSeconds; } }
        public TimeSpan ElapsedGameTime { get; set; }
        public bool Lag { get; set; }
        public TimeSpan TotalGameTime { get; set; }

        public GameTime()
        {
            ElapsedGameTime = new TimeSpan();
            Lag = false;
            TotalGameTime = new TimeSpan();
        }

        public GameTime(TimeSpan total, TimeSpan elapsed)
        {
            ElapsedGameTime = elapsed;
            Lag = false;
            TotalGameTime = total;
        }

        public GameTime(TimeSpan total, TimeSpan elapsed, bool lag)
        {
            ElapsedGameTime = elapsed;
            Lag = lag;
            TotalGameTime = total;
        }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }

        public bool Equals(GameTime other)
        {
            return ElapsedGameTime == other.ElapsedGameTime && Lag == other.Lag && TotalGameTime == other.TotalGameTime;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Utils.HASH_BASE;

                hash = hash * Utils.HASH_MULTIPLIER ^ TotalGameTime.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ Lag.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ ElapsedGameTime.GetHashCode();

                return hash;
            }
        }

        public override string ToString()
        {
            return $"Delta: {ElapsedGameTime.TotalSeconds}, Lag: {Lag}";
        }
    }
}