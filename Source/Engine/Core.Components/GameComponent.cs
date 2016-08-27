namespace Mentula.Engine.Core.Components
{
    using System;

    public class GameComponent<T> : IGameComponent
        where T : Game
    {
        public T Game { get; private set; }
        public bool Disposed { get; private set; }

        public bool Enabled
        {
            get { return enabled; }
            set
            {
                enabled = value;
                if (EnabledChanged != null) EnabledChanged(this, EventArgs.Empty);
            }
        }

        public int UpdateIndex
        {
            get { return updateIndex; }
            set
            {
                updateIndex = value;
                if (UpdateIndexChanded != null) UpdateIndexChanded(this, EventArgs.Empty);
            }
        }

        public event EventHandler<EventArgs> EnabledChanged;
        public event EventHandler<EventArgs> UpdateIndexChanded;

        private bool enabled;
        private int updateIndex;

        public GameComponent(T game)
        {
            Game = game;
            enabled = true;
        }

        public virtual void Initialize() { }

        public virtual void Update(GameTime gameTime) { }

        public virtual void Dispose()
        {
            if (!Disposed)
            {
                Disposed = true;
            }
        }
    }
}
