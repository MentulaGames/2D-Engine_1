namespace Mentula.Engine.Core.Input
{
    using System;

    public static class KeyBoard
    {
        internal static GameForm window;

        private static readonly ObjectDisposedException NullWindow;

        static KeyBoard()
        {
            NullWindow = new ObjectDisposedException("The window the keyboard was bound to has been disposed.", new NullReferenceException());
        }

        public static KeyBoardState GetState()
        {
            if (window != null) return window.keyState;

            throw NullWindow;
        }
    }
}