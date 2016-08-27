namespace Mentula.Engine.Core.Components
{
    using System;

    public class DrawableGameComponent<T> : GameComponent<T>, IDrawableGameComponent
        where T : Game
    {
        public bool Visible
        {
            get { return visible; }
            set
            {
                visible = value;
                if (VisibilityChanged != null) VisibilityChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler<EventArgs> VisibilityChanged;

        private bool visible;

        public DrawableGameComponent(T game)
            : base(game)
        {
            visible = true;
        }

        public virtual void Draw(GameTime gameTime) { }
    }
}
