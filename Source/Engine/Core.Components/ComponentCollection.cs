namespace Mentula.Engine.Core.Components
{
    using System;

    public sealed class ComponentCollection : IDisposable
    {
        public bool Disposed { get; private set; }

        private IGameComponent[] normal;
        private IDrawableGameComponent[] drawable;

        public ComponentCollection()
        {
            normal = new IGameComponent[0];
            drawable = new IDrawableGameComponent[0];
        }

        public void Add(IGameComponent component)
        {
            AddSingle(ref normal, component);
        }

        public void Add(IDrawableGameComponent component)
        {
            AddSingle(ref drawable, component);
        }

        public void AddRange(params IGameComponent[] components)
        {
            AddMultiple(ref normal, components);
        }

        public void AddRange(params IDrawableGameComponent[] components)
        {
            AddMultiple(ref drawable, components);
        }

        public void Dispose()
        {
            if (!Disposed)
            {
                Disposed = true;

                for (int i = 0; i < normal.Length; i++)
                {
                    normal[i].Dispose();
                }

                for (int i = 0; i < drawable.Length; i++)
                {
                    drawable[i].Dispose();
                }
            }
        }

        public void Initialze()
        {
            for (int i = 0; i < normal.Length; i++)
            {
                normal[i].Initialize();
            }

            for (int i = 0; i < drawable.Length; i++)
            {
                drawable[i].Initialize();
            }
        }

        public void Update(GameTime gameTime)
        {
            IGameComponent cur;

            for (int i = 0; i < normal.Length; i++)
            {
                cur = normal[i];
                if (cur.Enabled) cur.Update(gameTime);
            }

            for (int i = 0; i < drawable.Length; i++)
            {
                cur = drawable[i];
                if (cur.Enabled) cur.Update(gameTime);
            }
        }

        public void Draw(GameTime gameTime)
        {
            for (int i = 0; i < drawable.Length; i++)
            {
                IDrawableGameComponent cur = drawable[i];
                if (cur.Visible) cur.Draw(gameTime);
            }
        }

        private void AddMultiple<T>(ref T[] array, params T[] items)
            where T : IGameComponent
        {
            for (int i = 0; i < items.Length; i++)
            {
                AddSingle(ref array, items[i]);
            }
        }

        private void AddSingle<T>(ref T[] array, T item)
            where T : IGameComponent
        {
            int i = IncreaseLength(ref array, 1);

            for (int j = 0; j < array.Length - 1; j++)
            {
                T cur = array[j];
                if (cur.UpdateIndex > item.UpdateIndex)
                {
                    array[j] = item;
                    array[i] = cur;
                    return;
                }
            }

            array[i] = item;
        }

        private int IncreaseLength<T>(ref T[] array, int addition)
        {
            int index = array.Length;
            Array.Resize(ref array, index + addition);
            return index;
        }
    }
}
