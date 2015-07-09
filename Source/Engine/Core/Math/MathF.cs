namespace Mentula.Engine.Core.ExtendedMath
{
    using System;
    using System.Runtime.CompilerServices;

    public static class MathF
    {
        public const float Log10E = 0.434294f;
        public const float Log2E = 1.4427f;
        public const float PI = 3.14159f;
        public const float PiOver2 = 1.5708f;
        public const float PiOver4 = 0.785398f;
        public const float Tau = 6.28319f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Clamp(float min, float max, float value)
        {
            return value < min ? min : (value > max ? max : value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cubed(float value)
        {
            return value * value * value;
        }

        public static float Distance(float value1, float value2)
        {
            value1 = Math.Abs(value1);
            value2 = Math.Abs(value2);

            return Math.Max(value1, value2) - Math.Min(value1, value2);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float InvLerp(float min, float max, float value)
        {
            return (value - min) / (max - min);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Lerp(float min, float max, float amount)
        {
            if (amount < 0 || amount > 1) throw new ArgumentException("amount must be between 0 and 1.");

            return min + (max - min) * amount;
        }

        public static float Pow(float value, int exp)
        {
            float result = 1;

            while(exp > 0)
            {
                if (exp == 1) result *= value;

                exp >>= 1;
                value *= value;
            }
            
            return result;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Squared(float value)
        {
            return value * value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ToDegrees(float radians)
        {
            return radians * 180f / PI;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float ToRadians(float degrees)
        {
            return degrees * PI / 180f;
        }
    }
}