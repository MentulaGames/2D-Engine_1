namespace Mentula.Engine.Core.Input
{
    using System;

    public class TextInputEventArgs : EventArgs
    {
        public char Character { get; private set; }

        public TextInputEventArgs(char character)
        {
            Character = character;
        }
    }
}
