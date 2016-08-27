namespace Mentula.Engine.Core
{
    using System;

    public interface IDrawable
    {
        bool Visible { get; set; }

        void Draw(GameTime gameTime);

        event EventHandler<EventArgs> VisibilityChanged;
    }
}
