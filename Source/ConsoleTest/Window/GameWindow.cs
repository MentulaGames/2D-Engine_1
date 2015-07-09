namespace Mentula.Engine.Core
{
    using Mentula.Engine.Core.Input;
    using Mentula.Engine.Core.Window;
    using System;
    using System.ComponentModel;

    public abstract class GameWindow
    {
        public virtual bool AllowAltF4 { get { return allowAltF4; } set { allowAltF4 = value; } }
        [DefaultValue(false)]
        public abstract bool AllowUserResizing { get; set; }
        public abstract Rectangle ClientBounds { get; }
        public abstract IntPtr Handle { get; }
        public virtual bool IsBorderless { get { return false; } set { throw new NotImplementedException(); } }
        public abstract Point Position { get; set; }
        public abstract string ScreenDeviceName { get; }
        public string Title 
        { 
            get { return title; } 
            set 
            {
                if (title != value)
                {
                    SetTitle(value);
                    title = value;
                }
            } 
        }

        internal bool allowAltF4;
        internal MouseState mouseState;

        private string title;

        public event EventHandler<EventArgs> ClientSizeChanged;
        public event EventHandler<EventArgs> ScreenDeviceNameChanged;
        public event EventHandler<TextInputEventArgs> TextInput;

        public abstract void BeginScreenDeviceChange(bool willBeFullscreen);
        public abstract void EndScreenDeviceChange(string name, int width, int height);

        public void EndScreenDeviceChange(string name)
        {
            EndScreenDeviceChange(name, ClientBounds.Width, ClientBounds.Height);
        }

        protected void OnActivated()
        {
        }

        protected void OnClientSizeChanged()
        {
            if (ClientSizeChanged != null) ClientSizeChanged(this, EventArgs.Empty);
        }

        protected void OnDeactivated()
        {
        }

        protected void OnPaint()
        {
        }

        protected void OnScreenDeviceNameChanged()
        {
            if (ScreenDeviceNameChanged != null) ScreenDeviceNameChanged(this, EventArgs.Empty);
        }

        protected void OnTextInput(object sender, TextInputEventArgs e)
        {
            if (TextInput != null) TextInput(sender, e);
        }

        protected abstract void SetTitle(string title);
    }
}
