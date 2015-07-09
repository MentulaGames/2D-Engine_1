namespace Mentula.Engine.Core
{
    using System;

    public interface IDrawable
    {
        int DrawIndex { get; set; }
        bool Visible { get; set; }

        void Draw(GameTime gameTime);

        event EventHandler<EventArgs> DrawIndexChanged;
        event EventHandler<EventArgs> VisibilityChanged;
    }
}
