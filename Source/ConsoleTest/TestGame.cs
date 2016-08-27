using Mentula.Engine;
using Mentula.Engine.Core;
using Mentula.Engine.Core.Components;
using Mentula.Engine.Core.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    public class TestGame : Game
    {
        public TestGame()
        {
            IsMouseVisible = true;
            Initialize += InitializeComponents;
        }

        public void InitializeComponents()
        {
            Components.Add(new TestComp(this));
        }

        private class TestComp : DrawableGameComponent<TestGame>
        {
            public TestComp(TestGame game)
                : base(game)
            { }

            public override void Initialize()
            {
                base.Initialize();
            }

            public override void Update(GameTime gameTime)
            {
                base.Update(gameTime);
            }

            public override void Draw(GameTime gameTime)
            {
                base.Draw(gameTime);
            }
        }
    }
}
