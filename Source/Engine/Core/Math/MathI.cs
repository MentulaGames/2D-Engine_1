namespace Mentula.Engine.Core.ExtendedMath
{
    using System;
    using System.Runtime.CompilerServices;

    public static class MathI
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Clamp(int min, int max, int value)
        {
            return value < min ? min : (value > max ? max : value);
        }

        public static int Distance(int value1, int value2)
        {
            value1 = Math.Abs(value1);
            value2 = Math.Abs(value2);

            return Math.Max(value1, value2) - Math.Min(value1, value2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int InvLerp(int min, int max, int value)
        {
            return (value - min) / (max - min);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int Lerp(int min, int max, float amount)
        {
            if (amount < 0 || amount > 1) throw new ArgumentException("amount must be between 0 and 1.");

            return (int)(min + (max - min) * amount);
        }

        public static int Pow(int value, int exp)
        {
            int result = 1;

            while (exp > 0)
            {
                if (exp == 1) result *= value;

                exp >>= 1;
                value *= value;
            }

            return result;
        }
    }
}