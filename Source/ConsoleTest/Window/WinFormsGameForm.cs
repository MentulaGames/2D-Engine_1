namespace Mentula.Engine.Core.Window
{
    using System;
    using System.Security.Permissions;
    using System.Windows.Forms;

    internal class WinFormsGameForm : Form
    {
        public const int WM_KEYDOWN = 0x0100;
        public const int WM_SYSCOMMAND = 0x0112;

        public bool AllowAltF4;

        internal GameForm window;

        public WinFormsGameForm(GameForm window)
        {
            AllowAltF4 = true;
            this.window = window;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case (WM_KEYDOWN):
                    int wParam = m.WParam.ToInt32();

                    if (wParam == 0x5B || wParam == 0x5C)   // Left or Right Windows Key
                    {
                        if (WindowState == FormWindowState.Maximized) WindowState = FormWindowState.Minimized;
                    }
                    break;
                case(WM_SYSCOMMAND):
                    wParam = m.WParam.ToInt32();

                    if (!AllowAltF4 && wParam == 0xF060 && m.LParam.ToInt32() == 0 && Focused)
                    {
                        m.Result = IntPtr.Zero;
                        return;
                    }

                    // Disable the system menu form being toggled by keyboard input.
                    if (wParam == 0xF100)
                    {
                        m.Result = IntPtr.Zero;
                        return;
                    }
                    break;
            }

            base.WndProc(ref m);
        }
    }
}
