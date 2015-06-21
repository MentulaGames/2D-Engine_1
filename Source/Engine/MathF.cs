﻿namespace Engine.Core
{
    public static class MathF
    {
        public static float Squared(float value)
        {
            return value * value;
        }

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