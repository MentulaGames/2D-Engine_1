#pragma warning disable 1690    // Ignore https://msdn.microsoft.com/en-us/library/x524dkh4.aspx

namespace Mentula.Engine.Core.Input
{
    using System;
    using SystPoint = System.Drawing.Point;
    using System.Runtime.InteropServices;
    using System.Diagnostics;

    public static class Mouse
    {
        internal const int WHEEL_DELTA = 120;

        internal static GameForm window;

        private static readonly ObjectDisposedException NullWindow;

        static Mouse()
        {
            NullWindow = new ObjectDisposedException("The window the mouse was bound to has been disposed.", new NullReferenceException());
        }

        public static MouseState GetState()
        {
            if (window != null) return window.mouseState;

            throw NullWindow;
        }

        public static Point GetPosition()
        {
            if (window != null)
            {
                MouseState state = window.mouseState;
                return new Point(state.X, state.Y);
            }

            throw NullWindow;
        }

        public static void SetPosition(int x, int y)
        {
            if (window == null) throw NullWindow;
            
            window.mouseState.X = x;
            window.mouseState.Y = y;

            SystPoint pt = window.PointToScreen(new SystPoint(x, y));
            SetCursorPos(pt.X, pt.Y);
        }

        [DllImportAttribute("user32.dll", EntryPoint = "SetCursorPos")]
        [return: MarshalAsAttribute(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int X, int Y);
    }
}