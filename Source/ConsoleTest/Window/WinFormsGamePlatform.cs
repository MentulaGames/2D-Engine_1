namespace Mentula.Engine.Core.Window
{
    using Mentula.Engine.Core.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    internal class WinFormsGamePlatform : GamePlatform
    {
        private WinFormsGameWindow window;
        private readonly List<Keys> keyState;

        public WinFormsGamePlatform(Game game)
            :base(game)
        {
            keyState = new List<Keys>();
            KeyBoard.keys = keyState;

            window = new WinFormsGameWindow(this);
        }
    }
}
