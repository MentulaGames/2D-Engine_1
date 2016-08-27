namespace Mentula.Engine.Core
{
    using System;

    public interface IUpdateable
    {
        bool Enabled { get; set; }
        int UpdateIndex { get; set; }

        void Update(GameTime gameTime);

        event EventHandler<EventArgs> EnabledChanged;
        event EventHandler<EventArgs> UpdateIndexChanded;
    }
}
