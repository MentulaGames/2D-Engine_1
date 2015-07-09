namespace Mentula.Engine.Core.Window
{
    using Mentula.Engine.Core.Input;
    using System;

    internal abstract class GamePlatform : IDisposable
    {
        public abstract RunBehavior DefaultRunBehavior { get; }

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
                    OnIsMouseVisibleChanged();
                }
            }
        }

        public Game Game { get; private set; }

        public GameForm Window
        {
            get { return window; }
            protected set
            {
                //if (window == null) Mouse.PrimaryWindow = value;
            }
        }

        protected bool alreadyInFullScreen;
        protected bool alreadyInWindowed;
        protected TimeSpan inactiveSleepTime;
        protected bool needsToResetElapsedTime;
        protected bool IsDisposed { get { return disposed; } }

        protected GamePlatform(Game game)
        {
            alreadyInFullScreen = false;
            alreadyInWindowed = false;
            inactiveSleepTime = TimeSpan.FromMilliseconds(20.0);
            needsToResetElapsedTime = false;

            if (game == null) throw new ArgumentNullException("game");
            Game = game;
        }

        private bool isActive;
        private bool isMouseVisible;
        private bool disposed;
        private GameForm window;

        public event EventHandler<EventArgs> AsyncRunLoopEnded;
        public event EventHandler<EventArgs> Activated;
        public event EventHandler<EventArgs> Deactivated;

        public virtual bool BeforeRun()
        {
            return true;
        }

        public virtual void BeforeInitialize()
        {
            IsActive = true;

            //Create Game graphicsDevice if non is pressend.
        }

        public abstract void Exit();
        public abstract void RunLoop();
        public abstract void StartRunLoop();
        public abstract bool BeforeUpdate(GameTime gameTime);
        public abstract bool BeforeDraw(GameTime gameTime);
        public abstract void EnterFullScreen();
        public abstract void ExitFullSreen();

        public virtual TimeSpan TargetElapsedTimeChanging(TimeSpan value)
        {
            return value;
        }

        public abstract void BeginScreenDeviceChange(bool willBeFullScreen);
        public abstract void EndScreenDeviceChange(string name, int width, int height);

        public virtual void TargetElapsedChanged() { }
        public virtual void ResetElapsedTime() { }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                Mouse.PrimaryWindow = null;
                disposed = true;
            }
        }

        public virtual void Present() { }

        protected virtual void OnIsMouseVisibleChanged() { }

        protected void RaiseAsyncRunLoopEnded()
        {
            Raise(AsyncRunLoopEnded, EventArgs.Empty);
        }

        private void Raise<TEventArgs>(EventHandler<TEventArgs> handler, TEventArgs e)
            where TEventArgs : EventArgs
        {
            if (handler != null) handler(this, e);
        }
    }
}
