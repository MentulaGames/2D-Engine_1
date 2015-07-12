namespace Mentula.Engine.Core
{
    using System;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;

    public class GameWindow : IDisposable
    {
        public bool AllowAltF4 { get { return form.AllowAltF4; } set { form.AllowAltF4 = value; } }

        public bool AllowUserResizing
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

                if (Mode == WindowMode.Borderless || Mode == WindowMode.Fullscreen) return;

                form.FormBorderStyle = isResizable ? FormBorderStyle.Sizable : FormBorderStyle.FixedSingle;
            }
        }

        public Rectangle ClientBounds { get { return new Rectangle(form.ClientRectangle); } }

        public IntPtr Handle { get { return form.Handle; } }

        public WindowMode Mode
        {
            get { return mode; }
            set
            {
                if (mode != value)
                {
                    mode = value;

                    switch (mode)
                    {
                        case (WindowMode.Windowed):
                            form.FormBorderStyle = isResizable ? FormBorderStyle.Sizable : FormBorderStyle.FixedSingle;
                            form.WindowState = FormWindowState.Normal;
                            form.ClampMouse(false);
                            break;
                        case (WindowMode.Borderless):
                            form.FormBorderStyle = FormBorderStyle.None;
                            form.WindowState = FormWindowState.Normal;
                            form.ClampMouse(false);
                            break;
                        case (WindowMode.BorderlessFullscreen):
                            form.FormBorderStyle = FormBorderStyle.None;
                            form.WindowState = FormWindowState.Maximized;
                            form.ClampMouse(false);
                            break;
                        case (WindowMode.Fullscreen):
                            form.TopMost = true;
                            form.FormBorderStyle = FormBorderStyle.None;
                            form.WindowState = FormWindowState.Maximized;
                            form.ClampMouse(true);
                            break;
                    }
                }
            }
        }

        public Point Position
        {
            get { return new Point(form.DesktopLocation.X, form.DesktopLocation.Y); }
            set { form.DesktopLocation = new System.Drawing.Point(value.X, value.Y); }
        }

        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    form.Text = title;
                }
            }
        }

        public bool IsActive
        {
            get { return isActive; }
            internal set
            {
                if (isActive != value)
                {
                    isActive = value;
                    Raise(isActive ? Activated : Deactivated, EventArgs.Empty);
                }
            }
        }

        public bool IsMouseVisible
        {
            get { return isMouseVisible; }
            set
            {
                if (isMouseVisible != value)
                {
                    isMouseVisible = value;
                    OnMouseVisibleToggled();
                }
            }
        }

        internal GameForm form;

        private bool isActive;
        private bool isMouseVisible;
        private bool isMouseHidden;
        private bool isResizable;
        private string title;
        private WindowMode mode;

        public event EventHandler<EventArgs> Activated;
        public event EventHandler<EventArgs> Deactivated;

        internal GameWindow()
        {
            form = new GameForm();
            Mode = WindowMode.Windowed;

            Assembly assembly = Assembly.GetEntryAssembly();
            if (assembly != null) form.Icon = Icon.ExtractAssociatedIcon(assembly.Location);
            Title = Utils.WINDOW_TITLE;


            Mode = WindowMode.Windowed;
            form.StartPosition = FormStartPosition.CenterScreen;

            form.MouseEnter += OnMouseEnter;
            form.MouseLeave += OnMouseLeave;
            form.Activated += OnActivate;
            form.Deactivate += OnDeactivate;
        }

        ~GameWindow()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Exit()
        {
            form.Dispose();
            form = null;
        }

        internal void Run(Game game)
        {
            form.Show();

            while (form != null && !form.IsDisposed)
            {
                Application.DoEvents();

                if (form.IsDisposed) break;

                game.Tick();
            }
        }

        internal void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (form != null) form.Dispose();
            }
        }

        private void OnActivate(object sender, EventArgs e)
        {
            // To Do: Set graphics.
        }

        private void OnDeactivate(object sender, EventArgs e)
        {
            IsActive = false;
            if (form.keyState != null) form.keyState.ClearAll();
        }

        private void OnMouseVisibleToggled()
        {
            if (isMouseVisible)
            {
                if (isMouseHidden)
                {
                    isMouseHidden = false;
                    Cursor.Show();
                }
            }
            else if (!isMouseHidden && form.IsMouseInBounds)
            {
                isMouseHidden = true;
                Cursor.Hide();
            }
        }

        private void OnMouseEnter(object sender, EventArgs e)
        {
            if (!isMouseVisible && !isMouseHidden)
            {
                isMouseHidden = true;
                Cursor.Hide();
            }
        }

        private void OnMouseLeave(object sender, EventArgs e)
        {
            if (isMouseHidden)
            {
                isMouseHidden = false;
                Cursor.Show();
            }
        }

        private void Raise<TEventArgs>(EventHandler<TEventArgs> handler, TEventArgs e)
            where TEventArgs : EventArgs
        {
            if (handler != null) handler(this, e);
        }
    }
}