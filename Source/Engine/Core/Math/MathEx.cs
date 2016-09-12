namespace Mentula.Engine.Core.ExtendedMath
{
    internal static class MathEx
    {
        public static float Barycentric(float v1, float v2, float v3, float b2, float b3)
        {
            return ((1 - b2 - b3) * v1) + (b2 * v2) + (b3 * v3);
        }
    }
}