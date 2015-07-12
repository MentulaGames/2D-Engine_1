using Mentula.Engine;
using Mentula.Engine.Core;
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
        private TimeSpan oldTime;

        public TestGame()
        {
            IsMouseVisible = true;

            #region Update
            Update += (time) =>
                {
                    KeyBoardState kState = KeyBoard.GetState();

                    if (kState.GetKey(Keys.Escape)) Exit();
                    if (kState.GetKey(Keys.W))
                    {
                        if ((time.TotalGameTime - oldTime).Milliseconds > 250)
                        {
                            Window.Mode = (int)Window.Mode + 1 > (int)WindowMode.Fullscreen ? 0 : Window.Mode + 1;
                            oldTime = time.TotalGameTime;
                        }
                    }
                };
            #endregion

            #region Draw
            Draw += (time) =>
                {
                    //Console.WriteLine("Fps: {0}             ", Fps);
                    //Console.WriteLine("Mode: {0}            ", Window.Mode);
                    //Console.WriteLine("Mouse: {0}           ", Mouse.GetState());
                    //Console.SetCursorPosition(0, 0);
                };
            #endregion
        }
    }
}
