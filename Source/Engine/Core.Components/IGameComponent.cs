namespace Mentula.Engine.Core.Components
{
    using System;

    public interface IGameComponent : IUpdateable, IDisposable
    {
        void Initialize();
    }
}