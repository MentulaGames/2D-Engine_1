namespace Mentula.Engine.Core
{
    using Input;
    using System;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Windows.Forms;

    [DebuggerDisplay("Handle: {Handle}")]
    internal class GameForm : Form, IDisposable
    {
        private const int WM_KEYDOWN = 0x0100;
        private const int WM_KEYUP = 0x0101;

        private const int WM_SYSCOMMAND = 0x0112;

        private const int WM_MOUSEMOVE = 0x0200;
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_LBUTTONUP = 0x0202;
        private const int WM_RBUTTONDOWN = 0x204;
        private const int WM_RBUTTONUP = 0x205;
        private const int WM_MBUTTONDOWN = 0x207;
        private const int WM_MBUTTONUP = 0x208;
        private const int WM_MOUSEWHEEL = 0x20A;
        private const int WM_MOUSELEAVE = 0x02A3;

        private const int SC_CLOSE = 0xF060;
        private const int SC_KEYMENU = 0xF100;

        public bool AllowAltF4;
        public bool IsMouseInBounds;

        internal MouseState mouseState;
        internal KeyBoardState keyState;

        internal GameForm()
        {
            AllowAltF4 = false;
            mouseState = new MouseState();
            keyState = new KeyBoardState();

            Mouse.window = this;
            KeyBoard.window = this;

            Size = new System.Drawing.Size(800, 600);
        }

        internal void ClampMouse(bool clamp)
        {
            if (clamp) Cursor.Clip = new System.Drawing.Rectangle(Location, Size);
            else Cursor.Clip = new System.Drawing.Rectangle();
        }

        protected override void WndProc(ref Message m)
        {
            int wParam;
            int lParam;

            switch (m.Msg)
            {
                case (WM_MOUSEMOVE):
                    lParam = m.LParam.ToInt32();

                    mouseState.Y = (int)GET_L_WORD(lParam);
                    mouseState.X = (int)GET_H_WORD(lParam);

                    if (!IsMouseInBounds) IsMouseInBounds = true;
                    break;
                case(WM_MOUSELEAVE):
                    IsMouseInBounds = false;
                    break;
                case(WM_LBUTTONDOWN):
                    mouseState.LeftButton = true;
                    break;
                case(WM_LBUTTONUP):
                    mouseState.LeftButton = false;
                    break;
                case (WM_MBUTTONDOWN):
                    mouseState.MiddleButton = true;
                    break;
                case (WM_MBUTTONUP):
                    mouseState.MiddleButton = false;
                    break;
                case (WM_RBUTTONDOWN):
                    mouseState.RightButton = true;
                    break;
                case (WM_RBUTTONUP):
                    mouseState.RightButton = false;
                    break;
                case(WM_MOUSEWHEEL):
                    uint wLow = GET_L_WORD(m.WParam.ToInt32());
                    mouseState.ScrollWheelValue += (int)(wLow / Mouse.WHEEL_DELTA);
                    break;
                case(WM_KEYDOWN):
                    wParam = m.WParam.ToInt32();

                    keyState.SetKey(GET_H_WORD(wParam));
                    break;
                case(WM_KEYUP):
                    wParam = m.WParam.ToInt32();

                    keyState.ClearKey(GET_H_WORD(wParam));
                    break;
                case (WM_SYSCOMMAND):
                    wParam = m.WParam.ToInt32();
                    lParam = m.LParam.ToInt32();

                    switch (wParam)
                    {
                        case (SC_CLOSE):
                            if (Focused && !AllowAltF4 && lParam == 0)
                            {
                                //Deny user AltF4 request.
                                m.Result = IntPtr.Zero;
                                return;
                            }
                            break;
                        case (SC_KEYMENU):
                            // Disable the system menu form being toggled by keyboard input.
                            m.Result = IntPtr.Zero;
                            return;
                    }
                    break;
            }

            base.WndProc(ref m);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                Mouse.window = null;
                KeyBoard.window = null;
                Cursor.Clip = new System.Drawing.Rectangle();
            }

            base.Dispose(disposing);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static uint GET_L_WORD(int param)
        {
            return (uint)param >> 16;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static uint GET_H_WORD(int param)
        {
            return (uint)param & 0xFFFF;
        }
    }
}