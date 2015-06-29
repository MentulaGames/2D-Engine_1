namespace Mentula.Engine.Core
{
    using System;

    public static class Utils
    {
        internal const int HASH_BASE = unchecked((int)2166136261);
        internal const int HASH_MULTIPLIER = 16777619;

        public static int Max(params int[] args)
        {
            if (args.Length < 1) throw new ArgumentNullException("args cannot be empty.");

            int result = args[0];

            for (int i = 1; i < args.Length; i++)
            {
                int curr = args[i];

                if (curr > result) result = curr;
            }

            return result;
        }

        public static int Min(params int[] args)
        {
            if (args.Length < 1) throw new ArgumentNullException("args cannot be empty.");

            int result = args[0];

            for (int i = 1; i < args.Length; i++)
            {
                int curr = args[i];

                if (curr < result) result = curr;
            }

            return result;
        }
    }
}
