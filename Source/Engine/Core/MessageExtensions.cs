namespace Mentula.Engine.Core
{
    using System.Windows.Forms;

    internal static class MessageExtensions
    {
        public static int GetPointerId(this Message msg)
        {
            return (short)msg.WParam;
        }

        public static System.Drawing.Point GetPointerLocation(this Message msg)
        {
            int lowword = msg.LParam.ToInt32();

            return new System.Drawing.Point((short)lowword, (short)(lowword >> 16));
        }
    }
}