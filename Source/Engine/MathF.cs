namespace Mentula.Engine.Core
{
    using System;
    using System.Runtime.CompilerServices;

    public static class MathF
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Squared(float value)
        {
            return value * value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Cubed(float value)
        {
            return value * value * value;
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

        public static float Clamp(float min, float max, float value)
        {
            return value < min ? min : (value > max ? max : value);
        }
    }
}