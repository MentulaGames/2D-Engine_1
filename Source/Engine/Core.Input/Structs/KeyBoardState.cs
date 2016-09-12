namespace Mentula.Engine.Core.Input
{
    using System;

    public struct KeyBoardState : IEquatable<KeyBoardState>
    {
        internal uint keys0, keys1, keys2, keys3, keys4, keys5, keys6, keys7;

        public bool this[Keys key] { get { return GetKey(key); } }

        public static bool operator ==(KeyBoardState obj1, KeyBoardState obj2) { return obj1.Equals(obj2); }
        public static bool operator !=(KeyBoardState obj1, KeyBoardState obj2) { return !obj1.Equals(obj2); }

        public override bool Equals(object obj)
        {
            return GetHashCode() == obj.GetHashCode();
        }

        public bool Equals(KeyBoardState other)
        {
            return
                keys0 == other.keys0 &&
                keys1 == other.keys1 &&
                keys2 == other.keys2 &&
                keys3 == other.keys3 &&
                keys4 == other.keys4 &&
                keys5 == other.keys5 &&
                keys6 == other.keys6 &&
                keys7 == other.keys7;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = Utils.HASH_BASE;

                hash = hash * Utils.HASH_MULTIPLIER ^ keys0.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ keys1.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ keys2.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ keys3.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ keys4.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ keys5.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ keys6.GetHashCode();
                hash = hash * Utils.HASH_MULTIPLIER ^ keys7.GetHashCode();

                return hash;
            }
        }

        public bool GetKey(Keys key)
        {
            uint mask = (uint)1 << (((int)key) & 0x1f);

            uint element;

            switch (((int)key) >> 5)
            {
                case (0):
                    element = keys0;
                    break;
                case (1):
                    element = keys1;
                    break;
                case (2):
                    element = keys2;
                    break;
                case (3):
                    element = keys3;
                    break;
                case (4):
                    element = keys4;
                    break;
                case (5):
                    element = keys5;
                    break;
                case (6):
                    element = keys6;
                    break;
                case (7):
                    element = keys7;
                    break;
                default:
                    element = 0;
                    break;
            }

            return (element & mask) != 0;
        }

        public Keys[] GetPressedKeys()
        {
            uint count =
                CountBits(keys0) +
                CountBits(keys1) +
                CountBits(keys2) +
                CountBits(keys3) +
                CountBits(keys4) +
                CountBits(keys5) +
                CountBits(keys6) +
                CountBits(keys7);

            if (count == 0) return new Keys[0];

            Keys[] result = new Keys[count];

            int index = 0;
            if (keys0 != 0) index = AddKeysToArray(keys0, 0, result, index);
            if (keys1 != 0) index = AddKeysToArray(keys1, 32, result, index);
            if (keys2 != 0) index = AddKeysToArray(keys2, 64, result, index);
            if (keys3 != 0) index = AddKeysToArray(keys3, 96, result, index);
            if (keys4 != 0) index = AddKeysToArray(keys4, 128, result, index);
            if (keys5 != 0) index = AddKeysToArray(keys5, 160, result, index);
            if (keys6 != 0) index = AddKeysToArray(keys6, 192, result, index);
            if (keys7 != 0) index = AddKeysToArray(keys7, 224, result, index);

            return result;
        }

        public override string ToString()
        {
            return
                ToBinary(keys0, 32) + "|" +
                ToBinary(keys1, 32) + "|" +
                ToBinary(keys2, 32) + "|" +
                ToBinary(keys3, 32) + "|" +
                ToBinary(keys4, 32) + "|" +
                ToBinary(keys5, 32) + "|" +
                ToBinary(keys6, 32) + "|" +
                ToBinary(keys7, 32);
        }

        internal void SetKey(uint key)
        {
            uint mask = (uint)1 << (int)(key & 0x1f);

            switch (((int)key) >> 5)
            {
                case (0):
                    keys0 |= mask;
                    break;
                case (1):
                    keys1 |= mask;
                    break;
                case (2):
                    keys2 |= mask;
                    break;
                case (3):
                    keys3 |= mask;
                    break;
                case (4):
                    keys4 |= mask;
                    break;
                case (5):
                    keys5 |= mask;
                    break;
                case (6):
                    keys6 |= mask;
                    break;
                case (7):
                    keys7 |= mask;
                    break;
            }
        }

        internal void ClearKey(uint key)
        {
            uint mask = (uint)1 << (int)(key & 0x1f);

            switch (((int)key) >> 5)
            {
                case (0):
                    keys0 &= ~mask;
                    break;
                case (1):
                    keys1 &= ~mask;
                    break;
                case (2):
                    keys2 &= ~mask;
                    break;
                case (3):
                    keys3 &= ~mask;
                    break;
                case (4):
                    keys4 &= ~mask;
                    break;
                case (5):
                    keys5 &= ~mask;
                    break;
                case (6):
                    keys6 &= ~mask;
                    break;
                case (7):
                    keys7 &= ~mask;
                    break;
            }
        }

        internal void ClearAll()
        {
            keys0 = 0;
            keys1 = 0;
            keys2 = 0;
            keys3 = 0;
            keys4 = 0;
            keys5 = 0;
            keys6 = 0;
            keys7 = 0;
        }

        private static int AddKeysToArray(uint keys, int asciiOffset, Keys[] pressedKeys, int index)
        {
            const uint NUM_OF_BITS = 32;

            for (int i = 0; i < NUM_OF_BITS; i++)
            {
                if ((keys & (1 << i)) != 0) pressedKeys[index++] = (Keys)(asciiOffset + i);
            }

            return index;
        }

        private static uint CountBits(uint v)
        {
            // http://graphics.stanford.edu/~seander/bithacks.html#CountBitsSetParallel
            v = v - ((v >> 1) & 0x55555555);                        // reuse input as temporary
            v = (v & 0x33333333) + ((v >> 2) & 0x33333333);         // temp
            return ((v + (v >> 4) & 0xF0F0F0F) * 0x1010101) >> 24;  // count
        }

        private static string ToBinary(uint value, int length)
        {
            return (length > 1 ? ToBinary(value >> 1, length - 1) : null) + "01"[(int)(value & 1)];
        }
    }
}
