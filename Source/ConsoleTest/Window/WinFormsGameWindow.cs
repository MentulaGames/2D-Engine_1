namespace Mentula.Engine.Core.Window
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;

    internal class WinFormsGameWindow : GameForm, IDisposable
    {
        public override IntPtr Handle { get { return form.Handle; } }
        public override string ScreenDeviceName { get { return string.Empty; } }

        public override Rectangle ClientBounds
        {
            get
            {
                System.Drawing.Rectangle cRect = form.ClientRectangle;
                return new Rectangle(cRect.X, cRect.Y, cRect.Width, cRect.Height);
            }
        }

        public override bool AllowUserResizing
        {
            get { return isResizable; }
            set
            {
                if (isResizable != value)
                {
                    isResizable = value;
                    form.MaximizeBox = isResizable;
                }
                else return;

                if (isBorderless) return;

                form.FormBorderStyle = isResizable ? FormBorderStyle.Sizable : FormBorderStyle.FixedSingle;
            }
        }

        public override bool AllowAltF4
        {
            get { return base.AllowAltF4; }
            set
            {
                form.AllowAltF4 = value;
                base.AllowAltF4 = value;
            }
        }

        public override Point Position
        {
            get { return new Point(form.DesktopLocation.X, form.DesktopLocation.Y); }
            set { form.DesktopLocation = new System.Drawing.Point(value.X, value.Y); }
        }

        public override bool IsBorderless
        {
            get { return isBorderless; }
            set
            {
                if (isBorderless != value) isBorderless = value;
                else return;

                if (isBorderless) form.FormBorderStyle = FormBorderStyle.None;
                else form.FormBorderStyle = isResizable ? FormBorderStyle.Sizable : FormBorderStyle.FixedSingle;
            }
        }

        internal WinFormsGameForm form;
        internal Game Game { get; private set; }
        internal List<Keys> KeySatet { get; set; }

        private static ReaderWriterLockSlim allWindowsreaderWriterLockSlim;
        private static List<WinFormsGameWindow> allWindows;

        private WinFormsGamePlatform platform;
        private bool isResizable;
        private bool isBorderless;
        private bool isMouseHidden;
        private bool isMouseInBounds;

        internal WinFormsGameWindow(WinFormsGamePlatform platform)
        {
            this.platform = platform;
            Game = platform.Game;

            form = new WinFormsGameForm(this);

            Assembly assembly = Assembly.GetEntryAssembly();
            if (assembly != null) form.Icon = Icon.ExtractAssociatedIcon(assembly.Location);
            Title = Utils.WINDOW_TITLE;

            form.MaximizeBox = false;
            form.FormBorderStyle = FormBorderStyle.FixedSingle;
            form.StartPosition = FormStartPosition.CenterScreen;

            //_form.MouseWheel += OnMouseScroll;
            //_form.MouseEnter += OnMouseEnter;
            //_form.MouseLeave += OnMouseLeave;

            //SharpDx downloaden
        }
    }
}
